import {
    UserManager
} from 'oidc-client'
import {
    oidcSettings
} from '../config'

let mgr = null

export function useOidcClient(router, store) {
    if (mgr == null) {
        mgr = new UserManager(oidcSettings)
        mgr.events.addUserLoaded(user => {
            console.log('User loaded!' + JSON.stringify(user))
            store.commit('saveTokens', user)
        })

        mgr.events.addUserUnloaded(() => {
            console.log('User unloaded!')
            store.commit('clearTokens')
        })

        mgr.events.addAccessTokenExpiring(() => {
            console.log("Access token expiring.")
        })

        mgr.events.addAccessTokenExpired(() => {
            console.log("Access token expired.")
            store.commit('clearTokens')
        })

        mgr.events.addSilentRenewError(err => {
            console.error("Silent renew error=>" + JSON.stringify(err))
            store.dispatch('notifyError', {
                code: 401,
                message: '重新获取access token失败，请重新登录!',
                metadata: {
                    method: 'get',
                }
            })
        })

        mgr.events.addUserSignedIn(() => {
            console.log("The User signed in to identity server!")
        })

        mgr.events.addUserSignedOut(() => {
            console.log("User signed out from identity server!")
        })
    }

    const login = () => {
        console.log("User signs in redirectly!")
        mgr.signinRedirect()
    }

    const logout = () => {
        console.log("User signs out redirectly!")
        mgr.signoutRedirect()
    }

    const loginCallback = () => {
        console.log("User signs in callback!")
        mgr.signinRedirectCallback().then(() => {
            mgr.getUser().then(user => {
                if (user !== null) {
                    store.commit('saveTokens', user)
                    router.replace({
                        path: '/workspace'
                    })
                } else {
                    router.replace({
                        path: '/'
                    })
                }
            })
        })
    }

    return {
        login,
        logout,
        loginCallback
    }
}