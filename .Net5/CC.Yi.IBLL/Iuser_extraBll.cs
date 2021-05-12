using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial interface Iuser_extraBll
    {
        Task<int> UpdateExperience(int userId, int experienceNum, bool is_set = true);
    }
}
