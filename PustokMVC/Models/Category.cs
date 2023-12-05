using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
