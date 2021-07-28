
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CC.Yi.BLL
{
    public partial class plateBll : BaseBll<plate>, IplateBll
        {
            public plateBll(IBaseDal<plate> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class discussBll : BaseBll<discuss>, IdiscussBll
        {
            public discussBll(IBaseDal<discuss> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class commentBll : BaseBll<comment>, IcommentBll
        {
            public commentBll(IBaseDal<comment> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class userBll : BaseBll<user>, IuserBll
        {
            public userBll(IBaseDal<user> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class roleBll : BaseBll<role>, IroleBll
        {
            public roleBll(IBaseDal<role> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class actionBll : BaseBll<action>, IactionBll
        {
            public actionBll(IBaseDal<action> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class levelBll : BaseBll<level>, IlevelBll
        {
            public levelBll(IBaseDal<level> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class user_extraBll : BaseBll<user_extra>, Iuser_extraBll
        {
            public user_extraBll(IBaseDal<user_extra> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class labelBll : BaseBll<label>, IlabelBll
        {
            public labelBll(IBaseDal<label> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class collectionBll : BaseBll<collection>, IcollectionBll
        {
            public collectionBll(IBaseDal<collection> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class bannerBll : BaseBll<banner>, IbannerBll
        {
            public bannerBll(IBaseDal<banner> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class agreeBll : BaseBll<agree>, IagreeBll
        {
            public agreeBll(IBaseDal<agree> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class versionBll : BaseBll<version>, IversionBll
        {
            public versionBll(IBaseDal<version> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class shopBll : BaseBll<shop>, IshopBll
        {
            public shopBll(IBaseDal<shop> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class warehouseBll : BaseBll<warehouse>, IwarehouseBll
        {
            public warehouseBll(IBaseDal<warehouse> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class propBll : BaseBll<prop>, IpropBll
        {
            public propBll(IBaseDal<prop> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class friendBll : BaseBll<friend>, IfriendBll
        {
            public friendBll(IBaseDal<friend> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class articleBll : BaseBll<article>, IarticleBll
        {
            public articleBll(IBaseDal<article> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class recordBll : BaseBll<record>, IrecordBll
        {
            public recordBll(IBaseDal<record> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
    public partial class mytypeBll : BaseBll<mytype>, ImytypeBll
        {
            public mytypeBll(IBaseDal<mytype> cd,DataContext _Db):base(cd,_Db)
            {
                CurrentDal = cd;
                Db = _Db;
            }

            public async Task<bool> DelListByUpdateList(List<int> Ids)
            {
                var entitys = await CurrentDal.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
                foreach (var entity in entitys)
                {
                    entity.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                }
                return Db.SaveChanges() > 0;
            }

        }
}