<template>
  <el-container>
    <el-header>
      <current-user></current-user>
    </el-header>
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