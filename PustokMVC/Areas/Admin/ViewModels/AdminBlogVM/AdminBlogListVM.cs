using PustokMVC.Models;

namespace PustokMVC.Areas.Admin.ViewModels.AdminBlogVM
{
    public class AdminBlogListVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
}
