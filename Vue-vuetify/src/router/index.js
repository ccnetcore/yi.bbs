import Vue from 'vue'
import VueRouter from 'vue-router'
import Layout from '@/components/Layout'
import LayoutLogin from '@/components/LayoutLogin'

import Index from '@/views/user/Index'
import discuss from '@/views/user/discuss'
import comment from '@/views/user/comment'
import addDiscuss from '@/views/user/addDiscuss'
import my from '@/views/user/my'
import userInfo from '@/views/user/userInfo'
import myDiscuss from '@/views/user/myDiscuss'
import collection from '@/views/user/collection'

import AdmUser from '@/views/admin/AdmUser'
import AdmRole from '@/views/admin/AdmRole'
import AdmAction from '@/views/admin/AdmAction'
import AdmDiscuss from '@/views/admin/AdmDiscuss'
import AdmPlate from '@/views/admin/AdmPlate'
import AdmLevel from '@/views/admin/AdmLevel'


import login from '@/views/login'
import register from '@/views/register'



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
            }
        ]
    }
]

const router = new VueRouter({
    // mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router