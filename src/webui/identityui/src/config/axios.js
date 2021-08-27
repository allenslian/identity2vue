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
        if (error.response) {
            console.log('error.response interceptor error=>')
            console.error(error.response)
            store.dispatch('notifyError', {
                code: error.response.status,
                message: error.response.data,
                metadata: {
                    method: error.response.config.method,
                }
            })
        } else if (error.request) {
            console.log('error.request interceptor error=>')
            console.error(error.request)
            store.dispatch('notifyError', '请检查网络是否断开，或重新刷新网页!')
        } else {
            console.log('error interceptor error=>')
            console.error(error)
            store.dispatch('notifyError', error.message)
        }
    })

    return instance
}