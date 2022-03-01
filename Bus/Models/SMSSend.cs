using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus.Models
{
    public class SMSSend
    {
        public string NumberUser { get; set; }
        public string FIO { get; set; }
        public bool Verify { get; set; }
    }
}