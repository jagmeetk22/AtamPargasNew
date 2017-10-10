using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtamPargas.Models
{
    public class AuthorDto
    {
        public AuthorDto()
        {
            this.Books = new HashSet<BookDto>();
        }

        public int AuthorId { get; set; }
        [Required]
        public string AuthorCode { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public string AuthorNamePunjabi { get; set; }
        [StringLength(10,MinimumLength =10,ErrorMessage ="Number must be of 10 digits.")]
        public string AuthorContactNumber { get; set; }
        public string AuthorAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime AddedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }

        public virtual ICollection<BookDto> Books { get; set; }
    }
}
