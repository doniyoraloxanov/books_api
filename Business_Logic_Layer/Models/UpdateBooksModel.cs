using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class UpdateBooksModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title has to be a minimum of 3 characters")]
        [MaxLength(30, ErrorMessage = "Title has to be a maximum of 30 characters")]
        public string Title { get; set; }
        [Required]
        [Range(1000, 2025, ErrorMessage = "PublicationYear has to be between 1000 and 2024")]
        public int PublicationYear { get; set; }
        [Required]

        [MaxLength(20, ErrorMessage = "AuthorName has to be a maximum of 30 charcters")]
        public string AuthorName { get; set; }

    }
}
