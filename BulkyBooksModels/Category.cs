using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name="Catergory Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
