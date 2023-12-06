using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required, MaxLength(16)]
        public string Name { get; set; }
        [Required, MaxLength(16)]
        public string Surname { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
