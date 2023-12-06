using PustokMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Areas.Admin.ViewModels.AdminBlogVM
{
    public class AdminBlogCreateVM
    {
        [Required, MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        public int AuthorId { get; set; }
    }
}
