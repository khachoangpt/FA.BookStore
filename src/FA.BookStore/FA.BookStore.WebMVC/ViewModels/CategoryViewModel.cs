using System.ComponentModel.DataAnnotations;

namespace FA.BookStore.WebMVC.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }
    }
}