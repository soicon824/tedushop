using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Model
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        public string ID { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
