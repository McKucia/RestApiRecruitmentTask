using RestApiRecruitmentTask.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RestApiRecruitmentTask.Api.ViewModels
{
    public class ProducerViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Class must be between 3 and 15 characters.")]
        public string Class { get; set; }
        public List<Tire> Tires { get; set; }
    }
}
