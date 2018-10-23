using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIExample.Models.Data
{
    [Table("Purchase")]
    public class Purchase
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("productId")]
        public long ProductId { get; set; }

        [Column("customerId")]
        public long CustomerId { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        [Column("datePurchase")]
        public DateTime DatePurchase { get; set; }

        [Column("pricePerUnity")]
        public double PricePerUnity { get; set; }
    }
}