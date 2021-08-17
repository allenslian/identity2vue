import { createApp } from 'vue'
import App from './App.vue'
import router from '@/routes'

const app = createApp(App)

app.use(router)
app.use(ElementPlus)

app.mount('#app')
