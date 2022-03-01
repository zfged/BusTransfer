using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bus.Models
{
    public class Route
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Водитель")]
        public string NameDriver { get; set; }
        [Required]
        [DisplayName("Маршрут")]
        [MaxLength(20)]
        [Index("IX_FirstAndSecondRoute", 1, IsUnique = true)]
        public string NameRoute { get; set; }
        public IEnumerable<Client> Clients { get; set; }
       
        [Required]
        [DisplayName("Направление")]
        [Index("IX_FirstAndSecondRoute", 2, IsUnique = true)]
        public bool RoadTypeRoute { get; set; }

        [Required]
        [DisplayName("Дата")]
        [MaxLength(20)]
        [Index("IX_FirstAndSecondRoute", 3, IsUnique = true)]
        public string DateRoute { get; set; }


    }
}