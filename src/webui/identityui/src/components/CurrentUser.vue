<template>
  <el-row align="middle">
    <el-col :span="12">
      <h3 class="title">Home App</h3>
    </el-col>
    <el-col :span="12">
      <el-button
        type="primary"
        plain
        class="nav"
        @click="login"
        v-if="!isAuthenticated"
        >LOGIN</el-button
      >
      <el-menu class="profile-button" mode="horizontal" v-else>
        <el-submenu index="1">
          <template #title>{{ profile ? profile.name : "NONE" }}</template>
          <el-menu-item index="1-1" @click="goToWorldApp">Go To World App</el-menu-item>
          <el-menu-item index="1-2" @click="logout">LOGOUT</el-menu-item>
        </el-submenu>
      </el-menu>
    </el-col>
  </el-row>
</template>

<script>
import { onMounted, computed } from "@vue/runtime-core";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useOidcClient } from "../composables/useOidcClient";

export default {
  name: "CurrentUser",
  setup() {
    const router = useRouter();
    const store = useStore();
    const mgr = useOidcClient(router, store);

    const login = () => {
      mgr.login();
    };

    const logout = () => {
      mgr.logout();
    };

    const goToWorldApp = () => {
      window.location.href = process.env.VUE_APP_WORLD_APP_BASE;
    };

    onMounted(() => {
      console.log("CurrentUser onMounted is invoked!");
    });

    return {
      isAuthenticated: computed(() => store.getters.isAuthenticated),
      profile: computed(() => store.getters.profile),
      login,
      logout,
      goToWorldApp
    };
  },
};
</script>

<style scoped>
.title {
  text-align: left;
}

.nav {
  float: right;
}
</style>