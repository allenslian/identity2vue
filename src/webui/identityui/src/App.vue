<template>
  <div>
    <el-container>
      <el-header>
        <el-row>
          <el-col :span="12">
            <el-space :fill="true">
              <span>Hello Header</span>
            </el-space>
          </el-col>
          <el-col :span="12">
            <el-space alignment="flex-end">
               <el-button @click="login" v-if="!isAuthenticated">Login</el-button>
                <el-menu mode="horizontal" v-else>
                  <template #title>
                    <span>{{currentUser ? currentUser.name : 'None'}}</span>
                  </template>
                  <el-submenu index="1">
                    <el-menu-item index="1" @click="logout">Logout</el-menu-item>
                  </el-submenu>
                </el-menu>
            </el-space>
          </el-col>
        </el-row>
      </el-header>
      <el-container>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { useStore } from "vuex"
import { useSignInClient } from "./composables/useOidcClient"
import { oidcSettings } from './config/index'

export default {
  name: "App",

  setup() {
    const { login, logout, renewToken, revokeToken } = useSignInClient(oidcSettings);
    const store = useStore();

    onMounted(() => {
      
    });

    return {
      isAuthenticated: computed(() => store.getters.isAuthenticated),
      currentUser: computed(() => store.getters.currentUser),
      login,
      logout,
      renewToken,
      revokeToken,
    };
  },
};
</script>

<style>

body {
  margin: 0;
  padding: 0;
}

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
