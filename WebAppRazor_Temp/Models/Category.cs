using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название категории не введено")]
        [MaxLength(30, ErrorMessage = "Превышена максимальная длина")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Введённое значение должно быть от 1 до 100")]
        public int DisplayOrder { get; set; }

    }
}
