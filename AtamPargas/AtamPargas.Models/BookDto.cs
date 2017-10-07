using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtamPargas.Models
{
    public class BookDto
    {
        public BookDto()
        {
            this.Subjects = new HashSet<SubjectDto>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public int BookLanguage { get; set; }
        public int NumberOfPages { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime AddedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string BookReferenceNumber { get; set; }
        public string BookCoverPagePath { get; set; }
        public string BookPdfPath { get; set; }
        public Nullable<int> AuthorId { get; set; }

        public virtual AuthorDto Author { get; set; }
        public virtual ICollection<SubjectDto> Subjects { get; set; }
    }
}
