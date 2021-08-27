import axios from 'axios'

export default function (store) {
    const instance = axios.create({
        baseURL: 'https://localhost:6001',
    })

    instance.interceptors.request.use(config => {
        if (store.getters.isAuthenticated) {
            config.headers['Authorization'] =
                `${store.getters.tokens.tokenType} ${store.getters.tokens.accessToken}`
        }

        if (!config.headers['Content-Type']) {
            config.headers['Content-Type'] = 'application/json; charset=utf-8'
        }
        return config
    }, error => {
        console.error('request interceptor error=>' + JSON.stringify(error))
        return Promise.reject(error)
    })

    instance.interceptors.response.use(res => {
        return Promise.resolve(res.data)
    }, error => {
        console.log('response interceptor error=>')
        if (error.response) {
            console.error(error.response)
            store.dispatch('notifyError', {
                code: error.response.status,
                message: error.response.data,
                metadata: {
                    method: error.response.config.method,
                }
            })
        } else if (error.request) {
            console.error(error.request)
            store.dispatch('notifyError', 'The request failed!')
        } else {
            console.error(error)
            store.dispatch('notifyError', error.message)
        }
    })

    return instance
}