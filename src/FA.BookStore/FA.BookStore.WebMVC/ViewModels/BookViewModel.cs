using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Title { get; set; }

        [StringLength(1024, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public Guid PublisherId { get; set; }

        public bool Published { get; set; }

        public IEnumerable<Guid> SelectedAuthorIds { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}