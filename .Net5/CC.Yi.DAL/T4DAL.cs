using CC.Yi.IDAL;
using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CC.Yi.DAL
{
    public partial class plateDal : BaseDal<plate>, IplateDal
    {
        public plateDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class discussDal : BaseDal<discuss>, IdiscussDal
    {
        public discussDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class commentDal : BaseDal<comment>, IcommentDal
    {
        public commentDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class userDal : BaseDal<user>, IuserDal
    {
        public userDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class roleDal : BaseDal<role>, IroleDal
    {
        public roleDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class actionDal : BaseDal<action>, IactionDal
    {
        public actionDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class levelDal : BaseDal<level>, IlevelDal
    {
        public levelDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class user_extraDal : BaseDal<user_extra>, Iuser_extraDal
    {
        public user_extraDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class labelDal : BaseDal<label>, IlabelDal
    {
        public labelDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class collectionDal : BaseDal<collection>, IcollectionDal
    {
        public collectionDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class bannerDal : BaseDal<banner>, IbannerDal
    {
        public bannerDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
    public partial class agreeDal : BaseDal<agree>, IagreeDal
    {
        public agreeDal(DataContext _Db):base(_Db)
        {
            Db = _Db;
        }
    }
}