export const authentication = {
    state: () => ({
        isAuthenticated: false,
        profile: null,
        tokens: null,
        error: null
    }),

    getters: {
        isAuthenticated(state) {
            return state.isAuthenticated
        },

        profile(state) {
            return state.profile
        },

        tokens(state) {
            return state.tokens
        },

        error(state) {
            return state.error
        }
    },

    mutations: {
        saveTokens(state, payload) {
            if (payload === null) {
                return
            }
            state.isAuthenticated = true
            state.profile = {
                id: payload.profile.sub,
                name: payload.profile.name
            }
            state.tokens = {
                tokenType: payload.token_type,
                idToken: payload.id_token ? payload.id_token : '',
                accessToken: payload.access_token ? payload.access_token : '',
                accessTokenExpiry: payload.expires_at,
                refreshToken: payload.refresh_token ? payload.refresh_token : ''
            }
        },

        clearTokens(state) {
            state.isAuthenticated = false
            state.user = null
            state.tokens = null
            state.error = null
        },

        notifyError(state, payload) {
            if (typeof payload === 'object') {
                // object
                switch (payload.code) {
                    case 401:
                        state.error = {
                            needRelogin: true,
                            message: payload.message ? payload.message : '验证失败，或者是访问令牌过期，请重新登录!'
                        }
                        break;
                    case 403:
                        state.error = {
                            needRelogin: false,
                            message: payload.message ? payload.message : '暂时不能使用此功能，如果需要请您联系管理员!'
                        }
                        break;
                    case 405:
                        state.error = {
                            needRelogin: false,
                            message: payload.message ? payload.message : `服务器暂时不支持HTTP请求的[${payload.metadata.method}]方法`
                        }
                        break;
                    case 500:
                        state.error = {
                            needRelogin: false,
                            message: payload.message ? payload.message : '服务器发生错误，请稍后重试!'
                        }
                        break;
                    case 502:
                        state.error = {
                            needRelogin: false,
                            message: '暂时不能连接到服务器，请稍后重试！'
                        }
                        break;
                    case 503:
                        state.error = {
                            needRelogin: false,
                            message: payload.message ? payload.message : '服务器发生错误，请稍后重试!'
                        }
                        break;
                    default:
                        state.error = {
                            needRelogin: false,
                            message: payload.message ? payload.message : '发生未知错误!'
                        }
                        break;
                }
            } else {
                // string
                state.error = {
                    needRelogin: false,
                    message: payload
                }
            }
        }
    },

    actions: {
        notifyError({
            commit
        }, message) {
            if (message === null) {
                return
            }
            commit('notifyError', message)
        },

        saveTokens({
            commit
        }, user) {
            if (user === null) {
                return
            }

            return new Promise((resolve, reject) => {
                try {
                    commit('saveTokens', user)
                    resolve()
                } catch (error) {
                    reject(error)
                }
            })
        },

        clearTokens({
            commit
        }) {
            return new Promise((resolve, reject) => {
                try {
                    commit('clearTokens')
                    resolve()
                } catch (error) {
                    reject(error)
                }
            })
        }
    }
}