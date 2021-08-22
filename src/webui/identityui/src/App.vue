<template>
  <div>
    <el-container v-if="isAuthenticated">
      <router-view></router-view>
    </el-container>
    <el-container v-else>
      <el-button @click="login">Login</el-button>
    </el-container>
  </div>
</template>

<script>
import { ref, computed, onMounted } from "@vue/runtime-core"
import { useStore } from 'vuex'
import { useSignInClient } from './composables/useOidcClient'

export default {
  name: "App",
  setup() {
    const {login, renewToken, revokeToken } = useSignInClient()
    const store = useStore()
    let username = ref('')

    onMounted(() => {
      console.log("onMounted is invoked!")
    })

    return {
      isAuthenticated: computed(() => store.getters.isAuthenticated),
      username,
      login,
      renewToken,
      revokeToken
    }
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /*
  margin-top: 60px;
  */
}
</style>
