import { UserManager, WebStorageStateStore } from 'oidc-client'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'

export function useSignInClient() {
    const authority = 'https://localhost:5001'
    const settings = {
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      authority: authority,
      client_id: 'webclient.vuejs',
      redirect_uri: window.location.origin + '/#/sign-in-callback',
      post_logout_redirect_uri: window.location.origin + '/#/sign-out-callback',
      front_channel_logout_uri: window.location.origin + '/#/sign-out-callback',
      
      response_type: 'code',
      scope: 'openid profile platform.api',

      silent_redirect_uri: window.location.origin + '/#/silent-callback',
      automaticSilentRenew: true,
      
      revokeAccessTokenOnSignout: true,
      filterProtocolClaims: false
    }
    const mgr = new UserManager(settings)
    mgr.events.addUserLoaded(user => {
        console.log('User loaded')
        console.log(user)
    })
    mgr.events.addUserUnloaded(() => {
        console.log('User logged out locally')
    });
    mgr.events.addAccessTokenExpiring(() => {
        console.log("Access token expiring...")
    });
    mgr.events.addSilentRenewError(err => {
        console.log("Silent renew error: " + err.message)
    });
    mgr.events.addUserSignedIn((e) => {
        console.log("user logged in to the token server")
        console.log(e)
    });
    mgr.events.addUserSignedOut(() => {
        console.log("User signed out of OP")
    });
    
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
    const settings = {
        loadUserInfo: true,
        filterProtocolClaims: true,
        response_mode: 'query'
    }
    const mgr = new UserManager(settings)
    const router = useRouter()
    const store = useStore()

    const signInCallback = () => {
        console.log('signInCallback invoked!')
        mgr.signinRedirectCallback().then(user => {
            store.commit('saveToken', user)
            router.replace({ path: '/' })
        })
    }

    return {
        signInCallback
    }
}
