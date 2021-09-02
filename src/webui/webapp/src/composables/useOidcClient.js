import {
    UserManager
} from 'oidc-client'
import {
    oidcSettings
} from '../config/oidc'


let mgr = null
export function useOidcClient({replace}, {dispatch}) {
    if (mgr == null) {
        mgr = new UserManager(oidcSettings)
        mgr.events.addUserLoaded(user => {
            console.log('User loaded!' + JSON.stringify(user))
            dispatch('saveTokens', user)
        })

        mgr.events.addUserUnloaded(() => {
            console.log('User unloaded!')
            dispatch('clearTokens')
        })

        mgr.events.addAccessTokenExpiring(() => {
            console.log("Access token expiring.")
        })

        mgr.events.addAccessTokenExpired(() => {
            console.log("Access token expired.")
        })

        mgr.events.addSilentRenewError(err => {
            console.error("Silent renew error=>" + JSON.stringify(err))
            dispatch('notifyError', {
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
        mgr.signinRedirectCallback().then(() => {
            mgr.getUser().then(user => {
                if (user !== null) {
                    console.log("User signs in callback!")
                    dispatch('saveTokens', user).then(() => {
                        replace({
                            path: '/workspace'
                        })
                    })
                } else {
                    replace({
                        path: '/'
                    })
                }
            })
        }).catch(err => {
            console.error('signinRedirectCallback=>' + JSON.stringify(err))
            replace({
                path: '/'
            })
        })
    }

    const getUser = () => {
        return mgr.getUser()
    }

    return {
        login,
        logout,
        loginCallback,
        getUser
    }
}