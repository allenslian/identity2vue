import { UserManager } from 'oidc-client'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import { oidcSettings } from '../config'

const mgr = new UserManager(oidcSettings)

export function useSignInClient() {
    mgr.events.addUserLoaded(user => {
        console.log('User loaded')
        console.log(user)
    })

    mgr.events.addUserUnloaded(() => {
        console.log('User logged out locally')
    })

    mgr.events.addAccessTokenExpiring(() => {
        console.log("Access token expiring...")
    })
    
    mgr.events.addAccessTokenExpired(() => {

    })

    mgr.events.addSilentRenewError(err => {
        console.log("Silent renew error: " + err.message)
    })

    mgr.events.addUserSignedIn((e) => {
        console.log("user logged in to the token server")
        console.log(e)
    })

    mgr.events.addUserSignedOut(() => {
        console.log("User signed out of OP")
    })
    
    const login = () => {
        console.log("User signs in redirectly!")
        mgr.signinRedirect()
    };

    const logout = () => {
        console.log("User signs out redirectly!")
        mgr.signoutRedirect()
    };

    const renewToken = () => {
        console.log("User renews token!")
        mgr.signinSilent()
    };

    const revokeToken = () => {
        console.log("User revokes token!")
        mgr.revokeAccessToken().then(() => {
            console.log("User revoked token!")
        }).catch(err => {
            console.log(err)
        })
    }

    return {
        login,
        logout,
        renewToken,
        revokeToken
    }
}

export function useSignInCallbackClient() {
    const router = useRouter()
    const store = useStore()

    const signInCallback = () => {
        console.log('signInCallback invoked!')
        mgr.signinRedirectCallback().then(user => {
            store.commit('saveTokens', user)
            router.replace({ path: '/workspace' })
        })
    }

    return {
        signInCallback
    }
}
