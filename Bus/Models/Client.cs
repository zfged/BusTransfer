using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bus.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Дата")]
        [MaxLength(20)]
        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        [Index("IX_SecondAndSecond", 1, IsUnique = true)]
        public string Date { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string FIO { get; set; }
        [Required]
        [DisplayName("Тел")]
       
        public string Phone { get; set; }
        [Required]
        [MaxLength(20)]
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        [Index("IX_SecondAndSecond", 2, IsUnique = true)]
        [DisplayName("Место в Автобусе")]
        public string PlaceBus { get; set; }
        [Required]
        [DisplayName("Тип Поездки")]
        [Index("IX_FirstAndSecond", 3, IsUnique = true)]
        [Index("IX_SecondAndSecond", 3, IsUnique = true)]
        public bool RoadType { get; set; }
        [Required]
        [DisplayName("Откуда Едем")]

        public string CoordsFrom { get; set; }
        [Required]
        [DisplayName("Куда Едем")]

        public string CoordsTo { get; set; }



        [DisplayName("Координаты Откуда Едем")]
        
        public string CoordsFromR { get; set; }

        [DisplayName("Координаты Куда Едем")]
       
        public string CoordsToR { get; set; }


        [Index("IX_FirstAndSecond", 4, IsUnique = true)]
        public int? RouteId { get; set; }
        public Route Route { get; set; }
        
    }
}