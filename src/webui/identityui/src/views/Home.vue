<template>
  <el-container>
    <el-header>
      <current-user></current-user>
    </el-header>
    <!--
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
          <el-menu class="nav" mode="horizontal" v-else>
            <el-submenu index="1">
              <template #title>{{ profile ? profile.name : "NONE" }}</template>
              <el-menu-item index="1-1" @click="logout">LOGOUT</el-menu-item>
            </el-submenu>
          </el-menu>
        </el-col>
      </el-row>
    </el-header>
    -->
    <el-main> Some data are shown!!! </el-main>
  </el-container>
</template>

<script>
import { onMounted } from "@vue/runtime-core";
import CurrentUser from "../components/CurrentUser.vue";

export default {
  name: "Home",
  components: {
    "current-user": CurrentUser,
  },
  setup() {
    onMounted(() => {
      console.log("Home onMounted is invoked!");
    });
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