using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial  interface IwarehouseBll
    {
        #region
        //仓库使用道具，如果返回false则表示仓库没有
        #endregion
        Task<bool> setWarehouse(int userId, int propId);
    }
}
