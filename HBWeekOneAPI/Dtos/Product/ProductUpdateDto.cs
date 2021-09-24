using System.ComponentModel.DataAnnotations;

namespace HBWeekOneAPI.Dtos.Product
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name cannot be less than 2"),MaxLength(255,ErrorMessage = "Name cannot be greater than 255")]
        public string Name { get; set; }
    }
}