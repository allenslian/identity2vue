/**
 * parse jwt payload part.
 * @param {string} token - jwt token string
 * @returns {object} an object which describes jwt payload part.
 */
export const parseJwt = (token) => {
    try {
        var base64Url = token.split('.')[1]
        var base64 = base64Url.replace('-', '+').replace('_', '/')
        return JSON.parse(window.atob(base64))
    } catch (error) {
        return {}
    }
}

/**
 * whether the token has expired.
 * @param {string} token - jwt token string, such as access token/id token/refresh token
 * @returns {boolean} - true means the token has expired, false means the token has not expired.
 */
export const isTokenExpired = (token) => {
    if (token) {
        const parsed = parseJwt(token)
        const exp = parsed.exp ? parsed.exp * 1000 : null
        if (exp) {
            return exp < new Date().getTime()
        }
    }
    return false
}