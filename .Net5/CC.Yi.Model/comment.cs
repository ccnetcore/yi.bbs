using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class comment:baseModel
    {
        public DateTime? time { get; set; }
        public string content { get; set; }

        public user user { get; set; }
        public discuss discuss { get; set; }
    }
}
