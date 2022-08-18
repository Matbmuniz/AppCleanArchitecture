using System.ComponentModel.DataAnnotations;

namespace AppCleanArchitecture.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatorio")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
