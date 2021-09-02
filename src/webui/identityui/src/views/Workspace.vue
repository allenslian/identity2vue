<template>
  <el-container style="height:100%">
    <el-header>
      <current-user></current-user>
    </el-header>
    <el-container>
      <el-aside>
        <el-menu class="left-navbar" mode="vertical">
          <el-submenu index="1">
            <template #title>Product Management</template>
            <el-menu-item index="1-1" @click="navigateToProductList"
              >Product List</el-menu-item>
          </el-submenu>
          <el-menu-item index="2" @click="forwardToWebapp">
            <i class="el-icon-menu"></i>
            <span>Forward To WebApp</span>
          </el-menu-item>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
import { onMounted } from "@vue/runtime-core";
import { useRouter } from "vue-router";
import CurrentUser from "../components/CurrentUser.vue";

export default {
  name: "Workspace",
  components: {
    "current-user": CurrentUser,
  },
  setup() {
    const router = useRouter();
    const navigateToProductList = () => {
      router.push({ path: "/products" });
    };

    const forwardToWebapp = () => {
      window.location.href = "http://localhost:8081";
    };

    onMounted(() => {
      console.log("Workspace onMounted is invoked!");
    });

    return {
      navigateToProductList,
      forwardToWebapp,
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