
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIExample.Models.Data
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
    }
}