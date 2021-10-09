using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace rocky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name ="Display Order")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Display order for category must be grater than zero")]
        public int DisplayOrder { get; set; }

    }
}
