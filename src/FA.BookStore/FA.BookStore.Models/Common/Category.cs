using FA.BookStore.Models.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.BookStore.Models.Common
{
    [Table("Categories", Schema = "common")]
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        public IList<Book> Books { get; set; }
    }
}
