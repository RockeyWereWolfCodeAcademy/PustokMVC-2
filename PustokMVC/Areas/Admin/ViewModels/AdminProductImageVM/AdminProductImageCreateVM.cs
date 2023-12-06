using PustokMVC.Models;

namespace PustokMVC.Areas.Admin.ViewModels.AdminProductImageVM
{
    public class AdminProductImageCreateVM
    {
		public IFormFile ImageFile { get; set; }
		public int ProductId { get; set; }
		public bool IsActive { get; set; }
	}
}
