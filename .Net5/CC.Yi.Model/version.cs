using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class version:baseModel
    {
        public string ver { get; set; }
        public DateTime? time { get; set; }
        public string color { get; set; }
        public string context { get; set; }
    }
}
