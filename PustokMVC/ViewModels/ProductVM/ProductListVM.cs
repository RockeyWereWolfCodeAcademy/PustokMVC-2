﻿using PustokMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ViewModels.ProductVM
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ExTax { get; set; }
        public string Brand { get; set; }
        public uint RewardPoints { get; set; }
        public bool IsAvailable { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal SellPrice { get; set; }
        public float Discount { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
        public uint Quantity { get; set; }
        public string ImageUrl { get; set; }
        public Category? Category { get; set; }
    }
}
