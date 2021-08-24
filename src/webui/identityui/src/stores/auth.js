export const authentication = {
    state: () => ({
        user: {},
        tokenType: 'Bearer',
        accessToken: '',
        accessTokenExpiry: 0,
        refreshToken: '',
        idToken: ''
    }),
    getters: {
        isAuthenticated(state) {
            if (state.idToken) {
                return true
            }
            return false
        },

        currentUser(state) {
            return state.user
        },

        tokenType(state) {
            return state.tokenType
        },

        accessToken(state) {
            return state.accessToken
        }
    },
    mutations: {
        saveTokens(state, payload) {
            console.log(payload)
            // localStorage.setItem('oidc.user.id', payload.profile.sub)
            // localStorage.setItem('oidc.user.name', payload.profile.name)
            // localStorage.setItem('oidc.user.tokenType', payload.token_type)
            // localStorage.setItem('oidc.user.accessToken', payload.access_token ? payload.access_token : '')
            // localStorage.setItem('oidc.user.accessTokenExpiry', payload.expires_at)
            // localStorage.setItem('oidc.user.refreshToken', payload.refresh_token ? payload.refresh_token : '')
            // localStorage.setItem('oidc.user.idToken', payload.id_token)

            state.user.id = payload.profile.sub
            state.user.name = payload.profile.name
            state.tokenType = payload.token_type
            state.accessToken = payload.access_token ? payload.access_token : ''
            state.accessTokenExpiry = payload.expires_at
            state.idToken = payload.id_token ? payload.id_token : ''
            state.refreshToken = payload.refresh_token ? payload.refresh_token : ''
        },

        clearTokens(state) {
            state.user = {}
            state.accessToken = ''
            state.accessTokenExpiry = 0
            state.idToken = ''
            state.refreshToken = ''
        }
    },
    actions: {
        authenticate(state, user) {
            state.commit('saveTokens', user)
        }
    }
}