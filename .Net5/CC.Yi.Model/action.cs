using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class action:baseModel
    {

        public int sort { get; set; }

        public string action_name { get; set; }


        public string router { get; set; }

        public string icon { get; set; }

        public  ICollection<role> roles { get; set; }

        public  ICollection<user> users { get; set; }
    }
}
