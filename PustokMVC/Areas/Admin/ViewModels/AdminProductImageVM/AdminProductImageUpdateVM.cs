namespace PustokMVC.Areas.Admin.ViewModels.AdminProductImageVM
{
	public class AdminProductImageUpdateVM
	{
		public IFormFile? ImageFile { get; set; }
        public int ProductId { get; set; }
		public bool IsActive { get; set; }
	}
}
