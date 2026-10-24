using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        // Yahh jo required Likha hai isko dataNotations bolte hai ok 
        [Required]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
        [DisplayName("Display Name")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
