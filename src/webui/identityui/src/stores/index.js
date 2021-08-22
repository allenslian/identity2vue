import { createStore } from 'vuex'
import { authentication } from './auth'

const store = createStore({
    modules: {
        authentication
    }
})

export default store