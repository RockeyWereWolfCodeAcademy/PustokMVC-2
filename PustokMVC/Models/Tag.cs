using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required, MaxLength(16)]
        public string Title { get; set; }
        public List<BlogTag>? BlogTags { get; set; }
    }
}
