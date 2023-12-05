using PustokMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ViewModels.ProductVM
{
    public class ProductCreateVM
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }
        public decimal ExTax { get; set; }
        [MaxLength(64)]
        public string Brand { get; set; }
        public uint RewardPoints { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public decimal ActualPrice { get; set; }
        [Required]
        public decimal SellPrice { get; set; }
        [Range(0, 100)]
        public float Discount { get; set; }
        [Required, MaxLength(128)]
        public string About { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        public uint Quantity { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
