using PustokMVC.Models;

namespace PustokMVC.Areas.Admin.ViewModels.AdminProductImageVM
{
    public class AdminProductImageListVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; }
    }
}
