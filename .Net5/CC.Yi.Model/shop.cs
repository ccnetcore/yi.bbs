using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Yi.Model
{
    public class shop : baseModel
    {

        public prop prop { get; set; }

        public int price { get; set; }

        public int number { get; set; }

        public float discount { get; set; }
    }
}
