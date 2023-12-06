using PustokMVC.Areas.Admin.ViewModels.AdminBlogVM;
using PustokMVC.Areas.Admin.ViewModels.AdminProductImageVM;
using PustokMVC.Areas.Admin.ViewModels.AdminProductVM;
using PustokMVC.ViewModels.AuthorVM;
using PustokMVC.ViewModels.CategoryVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Areas.Admin.ViewModels.AdminHomeVM
{
    public class AdminHomeListVM
    {
        public IEnumerable<SliderListVM> Sliders { get; set; }
        public IEnumerable<AdminProductListVM> Products { get; set; }
        public IEnumerable<CategoryListVM> Categories { get; set; }
        public IEnumerable<AdminAuthorListVM> Authors { get; set; }
        public IEnumerable<AdminBlogListVM> Blogs { get; set; }
        public IEnumerable<AdminProductImageListVM> ProductImages { get; set; }
    }
}
