using PustokMVC.ViewModels.CategoryVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Areas.Admin.ViewModels.AdminHomeVM
{
    public class AdminHomeListVM
    {
        public IEnumerable<SliderListVM> Sliders { get; set; }
        public IEnumerable<AdminProductListVM> Products { get; set; }
        public IEnumerable<CategoryListVM> Categories { get; set; }
    }
}
