using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(3 , ErrorMessage = "Length Not Valid")]
        public string Name { get; set; }
    }
}
