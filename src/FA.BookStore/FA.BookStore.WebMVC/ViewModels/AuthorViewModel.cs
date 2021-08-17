using System.ComponentModel.DataAnnotations;

namespace FA.BookStore.WebMVC.ViewModels
{
    public class AuthorViewModel : BaseViewModel
    {
        [StringLength(255, MinimumLength = 2, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Author Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }
    }
}