using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Areas.Admin.ViewModels.AdminAuthorVM
{
    public class AdminAuthorUpdateVM
    {
        [Required, MaxLength(16)]
        public string Name { get; set; }
        [Required, MaxLength(16)]
        public string Surname { get; set; }
    }
}
