import { createApp } from 'vue'
import ElementPlus from 'element-plus'

import 'element-plus/theme-chalk/index.css'

import store from './stores/index'
import router from './routes/index'
import App from './App.vue'

const app = createApp(App)
app.use(ElementPlus)
    .use(router)
    .use(store)
    .mount('#app')
