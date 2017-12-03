using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.DataBase.ProjectAttribute;
using WebApplication1.Models.ClientData;
using WebApplication1.Utility;

namespace WebApplication1.DataBase
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreContext") //call decryption when instance context obj
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += new ObjectMaterializedEventHandler(ObjectMaterialized); //delegate
        }

        public override int SaveChanges()
        {
            var contextAdapter = ((IObjectContextAdapter)this);
            contextAdapter.ObjectContext.DetectChanges(); //force this. Sometimes entity state needs a handle jiggle
            var pendingEntities = contextAdapter.ObjectContext.ObjectStateManager
                                  .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                                  .Where(en => !en.IsRelationship).ToList();

            foreach (var entry in pendingEntities) //Encrypt all pending changes
                EncryptEntity(entry.Entity);

            int result = base.SaveChanges();

            foreach (var entry in pendingEntities) //Decrypt updated entities for continued use
                DecryptEntity(entry.Entity);

            return result;
        }

        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            var contextAdapter = ((IObjectContextAdapter)this);
            contextAdapter.ObjectContext.DetectChanges(); //force this. Sometimes entity state needs a handle jiggle
            var pendingEntities = contextAdapter.ObjectContext.ObjectStateManager
                                  .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                                  .Where(en => !en.IsRelationship).ToList();

            foreach (var entry in pendingEntities) //Encrypt all pending changes
                EncryptEntity(entry.Entity);

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entry in pendingEntities) //Decrypt updated entities for continued use
                DecryptEntity(entry.Entity);

            return result;
        }

        //Avoid the table name become plural
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        public void ObjectMaterialized(object sender, ObjectMaterializedEventArgs e) //delegated method for ObjectMaterializedEvent
        {
            DecryptEntity(e.Entity);
        }

        //DbSet
        public DbSet<Customer> Customers
        {
            get;
            set;
        }

        public DbSet<Order> Orders
        {
            get;
            set;
        }

        public DbSet<Product> Products
        {
            get;
            set;
        }

        private void EncryptEntity(object entity)
        {
            //Get all the properties that are encryptable and encrypt them
            var encryptedProperties = entity.GetType().GetProperties()
                                      .Where(p => p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(String)));

            foreach (var property in encryptedProperties)
            {
                string value = property.GetValue(entity) as string;

                if (!String.IsNullOrEmpty(value))
                {
                    string encryptedValue = EncryptionUtility.Encryption(value);
                    property.SetValue(entity, encryptedValue);
                }
            }
        }

        private void DecryptEntity(object entity)
        {
            //Get all the properties that are encryptable and decyrpt them
            var encryptedProperties = entity.GetType().GetProperties()
                                      .Where(p => p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(String)));

            foreach (var property in encryptedProperties)
            {
                string encryptedValue = property.GetValue(entity) as string;

                if (!String.IsNullOrEmpty(encryptedValue))
                {
                    string value = EncryptionUtility.Decryption(encryptedValue);
                    Entry(entity).Property(property.Name).OriginalValue = value;
                    Entry(entity).Property(property.Name).IsModified = false;
                }
            }
        }
    }
}