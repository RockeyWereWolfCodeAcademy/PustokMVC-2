using PustokMVC.Models;

namespace PustokMVC.ViewModels.AuthorVM
{
    public class AuthorListVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
