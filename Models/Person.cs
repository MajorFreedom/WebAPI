using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI.Models
{
    [Table("People")]
    public class Person
    {
      
        [Column("PersonID")]
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("Sex")]
        [StringLength(10)]
        public string Sex { get; set; }

        [Column("Age")]
        public int Age { get; set; }

       
    }
}
