import { createRouter, createWebHashHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Callback from '../views/Callback.vue'

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
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
      name: 'workspace',
      path: '/workspace',
      component: () => import(/* webpackChunkName: "workspace" */ '../views/Workspace.vue')
    },
    {
      path: '/sign-in-callback',
      component: Callback
    },
    {
      path: '/sign-out-callback',
      component: Callback
    }
  ],
})

// router.beforeEach()

export default router