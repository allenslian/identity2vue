export const authentication = {
    state: () => ({
        profile: null,
        tokens: null
    }),

    getters: {
        isAuthenticated(state) {
            if (state.tokens && state.tokens.idToken) {
                return true
            }
            return false
        },

        profile(state) {
            return state.profile
        },

        tokens(state) {
            return state.tokens
        }
    },

    mutations: {
        saveTokens(state, payload) {
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
            state.user = null
            state.tokens = null
        }
    },
    actions: {
        authenticate(state, user) {
            state.commit('saveTokens', user)
        }
    }
}