import Vue from 'vue'
import VueRouter from 'vue-router'

// import Layout from '@/components/LayoutLogin'
// import LayoutLogin from '@/components/LayoutLogin'

// import Index from '@/views/user/Index'
// import discuss from '@/views/user/discuss'
// import comment from '@/views/user/comment'
// import addDiscuss from '@/views/user/addDiscuss'
// import my from '@/views/user/my'
// import userInfo from '@/views/user/userInfo'
// import myDiscuss from '@/views/user/myDiscuss'
// import collection from '@/views/user/collection'
// import theme from '@/views/user/theme'
// import version from '@/views/user/version'
// import warehouse from '@/views/user/warehouse'
// import shop from '@/views/user/shop'
// import friend from '@/views/user/friend'

// import AdmUser from '@/views/admin/AdmUser'
// import AdmRole from '@/views/admin/AdmRole'
// import AdmAction from '@/views/admin/AdmAction'
// import AdmDiscuss from '@/views/admin/AdmDiscuss'
// import AdmPlate from '@/views/admin/AdmPlate'
// import AdmLevel from '@/views/admin/AdmLevel'
// import AdmSetting from '@/views/admin/AdmSetting'
// import AdmBanner from '@/views/admin/AdmBanner'
// import AdmVersion from '@/views/admin/AdmVersion'
// import AdmProp from '@/views/admin/AdmProp'
// import AdmShop from '@/views/admin/AdmShop'

// import qq from '@/views/qq'
// import login from '@/views/login'
// import register from '@/views/register'

//采用懒加载,优化一下，我们将用户和管理界面分离

const Index = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/Index");
const discuss = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/discuss");
const comment = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/comment");
const addDiscuss = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/addDiscuss");
const my = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/my");
const userInfo = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/userInfo");
const myDiscuss = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/myDiscuss");
const collection = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/collection");
const theme = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/theme");
const version = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/version");
const warehouse = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/warehouse");
const shop = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/shop");
const friend = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/user/friend");

const AdmUser = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmUser");
const AdmRole = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmRole");
const AdmAction = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmAction");
const AdmDiscuss = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmDiscuss");
const AdmPlate = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmPlate");
const AdmLevel = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmLevel");
const AdmSetting = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmSetting");
const AdmBanner = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmBanner");
const AdmVersion = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmVersion");
const AdmProp = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmProp");
const AdmShop = () =>
    import ( /* webpackChunkName: 'Admin' */ "@/views/admin/AdmShop");

const Layout = () =>
    import ( /* webpackChunkName: 'User' */ "@/components/Layout");
const LayoutLogin = () =>
    import ( /* webpackChunkName: 'User' */ "@/components/LayoutLogin");
const qq = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/qq");
const login = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/login");
const register = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/register");
const chathub = () =>
    import ( /* webpackChunkName: 'User' */ "@/views/chathub");




Vue.use(VueRouter)

const routes = [{
        path: '/layoutLogin',
        name: 'layoutLogin',
        component: LayoutLogin,
        redirect: "/login",
        children: [{
                path: "/login",
                name: "login",
                component: login
            },
            {
                path: '/register',
                name: 'register',
                component: register
            },
            {
                path: '/qq',
                name: 'qq',
                component: qq
            }
        ]
    },
    {
        path: '/',
        name: 'Layout',
        component: Layout,
        redirect: "/index",
        children: [{
                path: "/index",
                name: "Index",
                component: Index
            },
            {
                path: '/AdmUser',
                name: 'AdmUser',
                component: AdmUser
            },
            {
                path: '/AdmRole',
                name: 'AdmRole',
                component: AdmRole
            },
            {
                path: '/AdmAction',
                name: 'AdmAction',
                component: AdmAction
            },
            {
                path: '/my/:mylink',
                name: 'my',
                component: my
            },
            {
                path: '/discuss',
                name: 'discuss',
                component: discuss
            },
            {
                path: '/comment',
                name: 'comment',
                component: comment
            },
            {
                path: '/addDiscuss',
                name: 'addDiscuss',
                component: addDiscuss
            },
            {
                path: '/AdmPlate',
                name: 'AdmPlate',
                component: AdmPlate
            },
            {
                path: '/AdmBanner',
                name: 'AdmBanner',
                component: AdmBanner
            },

            {
                path: '/AdmLevel',
                name: 'AdmLevel',
                component: AdmLevel
            },
            {
                path: '/AdmDiscuss',
                name: 'AdmDiscuss',
                component: AdmDiscuss
            },
            {
                path: '/AdmVersion',
                name: 'AdmVersion',
                component: AdmVersion
            },
            {
                path: '/AdmSetting',
                name: 'AdmSetting',
                component: AdmSetting
            },
            {
                path: '/AdmProp',
                name: 'AdmProp',
                component: AdmProp
            },
            {
                path: '/AdmShop',
                name: 'AdmShop',
                component: AdmShop
            },
            {
                path: '/userInfo',
                name: 'userInfo',
                component: userInfo
            },
            {
                path: '/myDiscuss',
                name: 'myDiscuss',
                component: myDiscuss
            },
            {
                path: '/collection',
                name: 'collection',
                component: collection
            },
            {
                path: '/theme',
                name: 'theme',
                component: theme
            },
            {
                path: '/version',
                name: 'version',
                component: version
            },
            {
                path: '/warehouse',
                name: 'warehouse',
                component: warehouse
            },
            {
                path: '/shop',
                name: 'shop',
                component: shop
            },
            {
                path: '/friend',
                name: 'friend',
                component: friend
            },
            {
                path: '/chathub',
                name: 'chathub',
                component: chathub
            }
        ]
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router