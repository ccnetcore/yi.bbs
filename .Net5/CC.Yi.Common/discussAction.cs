using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Common
{
   public static class discussAction
    {
        public static bool is_allow(user _user,int discussId)
        {
            bool exit = false;
            foreach (var k in _user.actions)
            {
                if (k.action_name == ("d-" + discussId) || k.action_name == "d-admin")
                {
                    exit = true;
                    break;
                }
            }
            return exit;
        }
    }
}
