import { getToken, setToken, getUser, setUser, removeToken } from '../../utils/usertoken'
import accountApi from "@/api/accountApi"
//再导入axion请求
const state = { //状态
    token: getToken(),
    user: getUser()
}

const mutations = { //变化//载荷
    SET_TOKEN(state, token) {
        state.token = token
        setToken(token)
    },
    SET_USER(state, user) {
        state.user = user
        setUser(user)
    }
}



//在action中可以配合axios进行权限判断
const actions = { //动作
    setIcon({ commit, state }, icon) {
        state.user.icon = icon
        commit('SET_USER', state.user)
    },


    setLevel({ commit, state }, level) {

        state.user.icon = level;

        commit('SET_USER', state.user)
    },

    Login({ commit }, form) {
        return new Promise((resolv, reject) => {
            accountApi.login(form.username.trim(), form.password.trim()).then(resp => {
                commit('SET_TOKEN', resp.data.token)
                commit('SET_USER', resp.data.user)
                resolv(resp)
            }).catch(error => {
                reject(error)
            })
        })
    },
    Register({ commit }, form) {
        return new Promise((resolv, reject) => {
            accountApi.register(form.username.trim(), form.password.trim()).then(resp => {
                resolv(resp)
            }).catch(error => {
                reject(error)
            })
        })
    },
    Logged({ commit }) {
        return new Promise((resolv, reject) => {
            accountApi.logged().then(resp => {
                resolv(resp)
            }).catch(error => {
                reject(error)
            })
        })
    },

    // GetUserInfo({ commit, state }) {
    //     return new Promise((resolv, reject) => {
    //         // getUserInfo(state.token).then(response => {
    //         //     commit('SET_USER', response.data)
    //         // resolve(response)
    //         // }).catch(error=>{
    //         //     reject(error)
    //         // })
    //     })
    // },
    Logout({ commit, state }) {
        return new Promise((resolv, reject) => {
            accountApi.logout().then(response => {
                commit('SET_TOKEN', '')
                commit('SET_USER', null)
                removeToken()
                resolv(response)
            }).catch(error => {
                reject(error)
            })
        })
    }

}


export default { state, mutations, actions }