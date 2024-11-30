using System.ComponentModel.DataAnnotations;

namespace RestApiRecruitmentTask.Api.ViewModels
{
    public class TireViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}/\d{2}R\d{2,3}$", ErrorMessage = "Size must be in tire size format.")] // for example 205/55R16
        public string Size { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Size must be between 3 and 20 characters.")]
        public string TreadName { get; set; }
        [Required]
        public int ProducerId { get; set; }
    }
}
