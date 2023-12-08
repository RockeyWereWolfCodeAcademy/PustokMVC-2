using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace PustokMVC.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required, MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public bool IsDeleted { get; set; }
        public List<BlogTag>? BlogTags {  get; set; }

    }
}
