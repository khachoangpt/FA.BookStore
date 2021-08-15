using FA.BookStore.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.BookStore.Models.Common
{
    [Table("Books", Schema = "common")]
    public class Book : BaseEntity
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

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool Published { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Publisher")]
        public Guid PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public IList<Author> Authors { get; set; }

        public IList<Review> Reviews { get; set; }

    }
}
