using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public decimal ExTax { get; set; }
        [MaxLength(64)]
        public string Brand { get; set; }
        public uint RewardPoints { get; set; }
        public bool IsAvailable { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal SellPrice { get; set; }
        public float Discount { get; set; }
        [MaxLength(128)]
        public string About { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public uint Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
