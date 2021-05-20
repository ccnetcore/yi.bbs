using Autofac;
using Autofac.Extras.DynamicProxy;
using CC.Yi.BLL;
using CC.Yi.Common.Castle;
using CC.Yi.DAL;
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using System;


namespace CC.Yi.API
{
    public partial class Startup
    {
        //动态 面向AOP思想的依赖注入 Autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(CustomAutofacAop));
            builder.RegisterGeneric(typeof(BaseDal<>)).As(typeof(IBaseDal<>));
            builder.RegisterType<plateBll>().As<IplateBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<discussBll>().As<IdiscussBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<commentBll>().As<IcommentBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<userBll>().As<IuserBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<roleBll>().As<IroleBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<actionBll>().As<IactionBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<levelBll>().As<IlevelBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<user_extraBll>().As<Iuser_extraBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<labelBll>().As<IlabelBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<collectionBll>().As<IcollectionBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
            builder.RegisterType<bannerBll>().As<IbannerBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle，AOP
        }
    }
}