using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.BLL
{
    public partial class warehouseBll : BaseBll<warehouse>, IwarehouseBll
    {
        //仓库扣除道具
        public async Task<bool> setWarehouse(int userId, int propId)
        {
            var myWarehouser = await CurrentDal.GetEntities(u => u.is_delete == (short)ViewModel.Enum.DelFlagEnum.Normal && u.user.id == userId && u.prop.id == propId).FirstOrDefaultAsync();

            if (myWarehouser == null)
            {
                return false;
            }
            if (myWarehouser.number == 1)
            {
                myWarehouser.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
            }
            myWarehouser.number -= 1;

            CurrentDal.Update(myWarehouser);

            return Db.SaveChanges() > 0;
        }
    }
}
