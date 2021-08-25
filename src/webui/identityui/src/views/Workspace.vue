<template>
  <el-container>
    <el-header>
      <el-row align="middle">
        <el-col :span="12">
          <h3 class="title">Hello</h3>
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
              <el-menu-item index="1-1" @click="logout">LOGOUT</el-menu-item>
            </el-submenu>
          </el-menu>
        </el-col>
      </el-row>
    </el-header>
    <el-container>
      <el-aside>
        <el-menu class="left-navbar" mode="vertical">
          <el-submenu index="1">
            <template #title>Product Management</template>
            <el-menu-item index="1-1">Product List</el-menu-item>
          </el-submenu>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
import { onMounted, computed } from "@vue/runtime-core";
import { useStore } from "vuex";

export default {
  name: "Workspace",
  setup() {
    const store = useStore();

    onMounted(() => {
      console.log("Workspace onMounted is invoked!");
    });

    return {
      isAuthenticated: computed(() => store.getters.isAuthenticated),
      profile: computed(() => store.getters.profile),
    };
  },
};
</script>

<style>
.title {
    float: left;
}

.profile-button {
    float: right;
    width: 240px;
}

.left-navbar {
  width: 240px;
}
</style>