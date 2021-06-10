using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class prop : baseModel
    {
        public string name { get; set; }
        public string logo { get; set; }
        public string introduction { get; set; }
    }
}
