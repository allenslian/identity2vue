import { createApp } from 'vue'
import ElementPlus from 'element-plus'


import 'element-plus/lib/theme-chalk/index.css'

import store from './stores/index'
import router from './routes/index'
import App from './App.vue'
import axios from './config/axios'

const app = createApp(App)
app.config.globalProperties.$http = axios(store)
app.use(ElementPlus)
    .use(router)
    .use(store)
    .mount('#app')

