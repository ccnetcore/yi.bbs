using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class discuss:baseModel
    {
        public string title { get; set; }
        public string type { get; set; }
        public string introduction { get; set; }
        public DateTime? time { get; set; }
        public int agree_num { get; set; }
        public int see_num { get; set; }

        public string content { get; set; }


        public string color { get; set; }
        public int is_top { get; set; }
        public int is_anonymous { get; set; }
        public int is_encryption { get; set; }

        public ICollection<label> labels { get; set; }
        public ICollection<comment> comments { get; set; }
        public user user { get; set; }
        public plate plate { get; set; }
        public ICollection<article> articles { get; set; }
    }
}
