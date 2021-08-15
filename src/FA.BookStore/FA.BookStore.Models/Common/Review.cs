using FA.BookStore.Models.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.BookStore.Models.Common
{
    [Table("Reviews", Schema = "common")]
    public class Review : BaseEntity
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
