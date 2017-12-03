using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.ClientData
{
    public class Product
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int Price
        {
            get;
            set;
        }

        // Navigation Properties
        public virtual ICollection<Order> Orders
        {
            get;
            set;
        }
    }
}