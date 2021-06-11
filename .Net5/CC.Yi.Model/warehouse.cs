using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class warehouse : baseModel
    {
        public user user { get; set; }
        public prop prop { get; set; }
        public int number { get; set; }
      
    }
}
