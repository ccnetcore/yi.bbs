using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Model
{
   public class article:baseModel
    {
       public List<article> children { get; set; }
       public string content { get; set; }
        public string name { get; set; }
        public discuss discuss { get; set; }
    }
}
