using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIExample.Models.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        public string Name { get; set; }

        public double PricePerUnit { get; set; }
    }
}