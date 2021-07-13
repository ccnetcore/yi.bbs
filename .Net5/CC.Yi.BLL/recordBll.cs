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
    public partial class recordBll : BaseBll<record>, IrecordBll
    {
        public bool Add(int discussId, string describe, int userId)
        {
            var myUser = Db.Set<user>().Find(userId);
            var myDiscuss = Db.Set<discuss>().Find(discussId);
            CurrentDal.Add(new record { describe = describe, time = DateTime.Now, discuss = myDiscuss, user = myUser, is_delete = (short)ViewModel.Enum.DelFlagEnum.Normal });
            return Db.SaveChanges() > 0;
        }
    }
}
