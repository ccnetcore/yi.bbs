using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial interface IrecordBll 
    {
        //添加编辑信息
        bool Add(int discussId,string describe,int userId);
    }
}
