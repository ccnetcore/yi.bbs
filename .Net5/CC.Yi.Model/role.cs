using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public  class role:baseModel
    {


        public string role_name { get; set; }

        public  ICollection<action> actions { get; set; }

        public  ICollection<user> users { get; set; }
    }
}
