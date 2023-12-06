using PustokMVC.Models;

namespace PustokMVC.ViewModels.BlogVM
{
    public class BlogListVM
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Author Author { get; set; }
    }
}
