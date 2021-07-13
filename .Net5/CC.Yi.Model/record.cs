using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Model
{
    public class record:baseModel
    {
        public DateTime? time { get; set; }

        public user user { get; set; }

        public discuss discuss { get; set; }

        public string describe { get; set; }
    }
}
