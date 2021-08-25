import {
    WebStorageStateStore
} from 'oidc-client'

const authority = 'https://localhost:5001'

export const oidcSettings = {
    userStore: new WebStorageStateStore({
        store: window.localStorage
    }),
    authority: authority,
    client_id: 'webclient.vuejs',
    redirect_uri: window.location.origin + '/#/sign-in-callback',
    post_logout_redirect_uri: window.location.origin + '/#/sign-out-callback',
    front_channel_logout_uri: window.location.origin + '/#/sign-out-callback',

    response_type: 'code',
    scope: 'openid profile platform.api',

    silent_redirect_uri: window.location.origin + '/silent-callback.html',
    automaticSilentRenew: true,
    accessTokenExpiringNotificationTime: 10,

    revokeAccessTokenOnSignout: true,

    loadUserInfo: true,
    filterProtocolClaims: true,
    response_mode: 'query'
}