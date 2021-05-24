using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class user:baseModel
    {

        public DateTime? time { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string icon { get; set; }
        public string nick { get; set; }
        public string openid { get; set; }

        public user_extra user_extra { get; set; }

        public ICollection<plate> plates { get; set; }
        public ICollection<discuss> discusses { get; set; }
        public ICollection<comment> comments { get; set; }

        public ICollection<action> actions { get; set; }
        public ICollection<role> roles { get; set; }

        public virtual ICollection<label> labels { get; set; }
    }
}
