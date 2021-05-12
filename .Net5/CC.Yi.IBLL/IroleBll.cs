using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial interface IroleBll
    {
        #region
        //设置角色拥有哪些权限
        #endregion
        Task<bool> setAction(int roleId, List<int> actionIds);
    }
}
