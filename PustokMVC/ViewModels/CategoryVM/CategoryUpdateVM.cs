using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ViewModels.CategoryVM
{
    public class CategoryUpdateVM
    {
        [Required, MaxLength(16)]
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
