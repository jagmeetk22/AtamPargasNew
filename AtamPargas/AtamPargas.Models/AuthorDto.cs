using System;
using System.Collections.Generic;
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
        public string AuthorCode { get; set; }
        public string AuthorName { get; set; }
        public string AuthorNamePunjabi { get; set; }
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
