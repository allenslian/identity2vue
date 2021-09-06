import { createRouter, createWebHashHistory } from 'vue-router'
import Home from '../views/Home.vue'
import SSO from '@/views/SSO.vue'
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
      name: 'sso',
      path: '/sso',
      component: SSO
    },
    {
      name: 'workspace',
      path: '/workspace',
      component: () => import(/* webpackChunkName: "workspace" */ '../views/Workspace.vue'),
      children: [
        {
          path: '/products',
          name: 'productList',
          component: () => import(/* webpackChunkName: "workspace" */ '../views/products/ProductList.vue'),
          meta: {
            requireAuth: true
          }
        }
      ]
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