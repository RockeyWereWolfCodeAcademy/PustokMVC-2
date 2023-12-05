using PustokMVC.Models;

namespace PustokMVC.ViewModels.CategoryVM
{
    public class CategoryListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
