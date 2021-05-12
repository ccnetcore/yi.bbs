using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class level:baseModel
    {


        public int num { get; set; }
        public string name { get; set; }
        public int experience { get; set; }

        public int max_char { get; set; }


    }
}
