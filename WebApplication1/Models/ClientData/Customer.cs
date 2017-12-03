using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.DataBase.ProjectAttribute;

namespace WebApplication1.Models.ClientData
{
    public class Customer
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        public string FacebookId
        {
            get;
            set;
        }

        [Required]
        [StringLength(50, ErrorMessage = "Too much characters")]
        [MinLength(10, ErrorMessage = "At least 10 characters")]
        public string Name
        {
            get;
            set;
        }

        [Required]
        [Encrypted]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "At least 10 characters")]
        public string Password
        {
            get;
            set;
        }

        [Required]
        [Encrypted]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "At least 10 characters")]
        [PasswordCompare("Password", ErrorMessage = "Password not match!")]
        public string PasswordConfirmation
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get;
            set;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegisterOn
        {
            get;
            set;
        }

        public DateTime? EditOn
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