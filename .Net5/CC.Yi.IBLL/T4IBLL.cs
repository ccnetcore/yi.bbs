using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
   public partial interface IplateBll : IBaseBll<plate>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IdiscussBll : IBaseBll<discuss>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IcommentBll : IBaseBll<comment>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IuserBll : IBaseBll<user>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IroleBll : IBaseBll<role>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IactionBll : IBaseBll<action>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IlevelBll : IBaseBll<level>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface Iuser_extraBll : IBaseBll<user_extra>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IlabelBll : IBaseBll<label>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IcollectionBll : IBaseBll<collection>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IbannerBll : IBaseBll<banner>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IagreeBll : IBaseBll<agree>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IversionBll : IBaseBll<version>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IshopBll : IBaseBll<shop>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IwarehouseBll : IBaseBll<warehouse>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IpropBll : IBaseBll<prop>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IfriendBll : IBaseBll<friend>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
   public partial interface IarticleBll : IBaseBll<article>
    {
        Task<bool> DelListByUpdateList(List<int> Ids);
    }
}