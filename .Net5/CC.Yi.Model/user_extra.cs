using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class user_extra:baseModel
    {
        public int experience { get; set; }
        public int level { get; set; }
    }
}
