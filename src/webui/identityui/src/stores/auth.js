
const authentication = {
    state: () => ({
        userId: '',
        username: '',
        tokenType: 'Bearer',
        accessToken: '',
        accessTokenExpiry: 0,
        refreshToken: '',
        idToken: ''
    }),
    getters: {
        isAuthenticated(state) {
            state.accessToken = localStorage.getItem('oidc.user.accessToken')
            state.accessTokenExpiry = parseInt(localStorage.getItem('oidc.user.accessTokenExpiry'))
            return state.accessToken !== '' && state.accessTokenExpiry < new Date().getTime() / 1000
        },

        currentUser(state) {
            state.userId = localStorage.getItem('oidc.user.id')
            state.username = localStorage.getItem('oidc.user.name')
            return {
                id: state.userId,
                name: state.username
            }
        },

        accessToken(state) {
            return state.accessToken
        }
    },
    mutations: {
        saveToken(_state, payload) {
            console.log(payload)
            localStorage.setItem('oidc.user.id', payload.profile.sub)
            localStorage.setItem('oidc.user.name', payload.profile.name)
            localStorage.setItem('oidc.user.tokenType', payload.token_type)
            localStorage.setItem('oidc.user.accessToken', payload.access_token ? payload.access_token : '')
            localStorage.setItem('oidc.user.accessTokenExpiry', payload.expires_at)
            localStorage.setItem('oidc.user.refreshToken', payload.refresh_token ? payload.refresh_token : '')
            localStorage.setItem('oidc.user.idToken', payload.id_token)
        }
    },
}

export {
    authentication
}