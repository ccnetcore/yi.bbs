using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial interface IdiscussBll 
    {
        //使用道具
        Task<bool> setProp(int disucssId, int propId, string color);
    }
}
