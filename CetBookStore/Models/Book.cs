using System.ComponentModel.DataAnnotations;

namespace CetBookStore.Models
{
    public class Book : OrderableItem
    {
           
        [Required]
        [DataType(DataType.Html)]
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Author { get; set; }
        [StringLength(100,MinimumLength =3)]
        public string Publisher { get; set; }
        [Range(1, 10000)]
        public int PageCount { get; set; }
        
        
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
