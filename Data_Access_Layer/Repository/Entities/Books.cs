using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Entities
{
    public partial class Book
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }

        [Column("publication_year")]
        public int PublicationYear { get; set; }

        [Column("author_name")]
        [StringLength(100)]
        public string AuthorName { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("view_count")]
        public int ViewCount { get; set; }
    }
}
