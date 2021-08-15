using FA.BookStore.Models.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.BookStore.Models.Common
{
    [Table("Publishers", Schema = "common")]
    public class Publisher : BaseEntity
    {
        [StringLength(255, MinimumLength = 2, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Publisher Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        public IList<Book> Books { get; set; }
    }
}
