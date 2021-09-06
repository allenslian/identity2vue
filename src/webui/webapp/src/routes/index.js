import {
  createRouter,
  createWebHashHistory
} from 'vue-router'
import Home from '@/views/Home.vue'
import SSO from '@/views/SSO.vue'
import Callback from '@/views/Callback.vue'

const router = createRouter({
  history: createWebHashHistory(),
  routes: [{
      name: 'home',
      path: '/',
      component: Home
    },
    {
      path: '/home',
      redirect: {
        name: 'home'
      },
    },
    {
      name: 'sso',
      path: '/sso',
      component: SSO
    },
    {
      path: '/sign-in-callback',
      component: Callback
    }
  ],
})

// router.beforeEach()

export default router