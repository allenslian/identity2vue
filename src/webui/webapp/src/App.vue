<template>
  <el-container style="height: 100%">
    <router-view></router-view>
  </el-container>
</template>

<script>
import { onMounted } from "vue";

export default {
  name: "App",
  setup() {

    onMounted(async () => {
      console.log("webapp App onMounted is invoked!");
    });
  },
};
</script>

<style>
html,
body {
  height: 100%;
  margin: 0;
  padding: 0;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
