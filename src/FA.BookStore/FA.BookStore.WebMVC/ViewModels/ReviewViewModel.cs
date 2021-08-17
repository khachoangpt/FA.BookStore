using System;
using System.ComponentModel.DataAnnotations;

namespace FA.BookStore.WebMVC.ViewModels
{
    public class ReviewViewModel : BaseViewModel
    {
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}