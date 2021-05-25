using CC.Yi.Common;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial interface IuserBll
    {
        #region
        //通过用户id来获取用户所有的权限（正常权限+特殊权限）
        #endregion
        Task<List<actionJwt>> getActionByUserId(int id);

        #region
        //给用户设置角色
        #endregion
        Task<bool> setRole(int userId, List<int> roleIds);

        #region
        //给用户设置特殊权限
        #endregion
        Task<bool> setSpecialAction(int userId, List<int> actionIds);

        #region
        //登录的通用方法
        #endregion
        Task<Result> login(user data);

        #region
        //检测邮箱是否存在
        #endregion
        Task<bool> mail_exist(string mail);
    }
}
