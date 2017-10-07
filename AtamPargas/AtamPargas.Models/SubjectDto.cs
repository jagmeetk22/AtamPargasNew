using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtamPargas.Models
{
    public class SubjectDto
    {
        public SubjectDto()
        {
            this.Books = new HashSet<BookDto>();
        }

        public int SubjectId { get; set; }
        [Required]
        [Display(Name = "Subject Code")]
        public string SubjectCode { get; set; }
        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        [Display(Name = "Subject Name Punjabi")]
        public string SubjectNamePunjabi { get; set; }
        [Required]
        [Display(Name = "Subject Description")]
        public string SubjectDescription { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        [Display(Name ="Is Sub-Subject")]
        public bool IsSubSubject { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime AddedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ParentSubjectId { get; set; }

        public virtual ICollection<BookDto> Books { get; set; }
    }
}
