using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Model
{
    public class label:baseModel
    {
        public string name { get; set; }
        public string color { get; set; }
        public string color_bg { get; set; }

        public  ICollection<discuss>  discusses { get; set; }
        public  user user { get; set; }
    }
}
