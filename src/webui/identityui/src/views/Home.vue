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
          <el-menu class="nav" mode="horizontal" v-else>
            <el-submenu index="1">
              <template #title>{{ profile ? profile.name : "NONE" }}</template>
              <el-menu-item index="1-1" @click="logout">LOGOUT</el-menu-item>
            </el-submenu>
          </el-menu>
        </el-col>
      </el-row>
    </el-header>
    <el-main> Some data are shown!!! </el-main>
  </el-container>
</template>

<script>
import { onMounted, computed } from "@vue/runtime-core";
import { useStore } from "vuex";
import { oidcSettings } from "../config/index";
import { useSignInClient } from "../composables/useOidcClient";

export default {
  name: "Home",

  setup() {
    const store = useStore();
    const mgr = useSignInClient(oidcSettings);
    // const products = ref([])
    // const getProducts = () => {
    //   axios.get('https://localhost:6001/products', {
    //     headers: {
    //       'Authorization': store.getters.tokenType + ' ' + store.getters.accessToken
    //     }
    //   }).then(res => {
    //     console.log(res)
    //     res.data.forEach(item => {
    //       products.value.push(item)
    //     })
    //   })
    // }

    // const addProduct = () => {
    //   axios.post('https://localhost:6001/products', {
    //     name: 'product a',
    //     description: 'a product called a',
    //     salesPrice: 11.00
    //   }, {
    //     headers: {
    //       'Authorization': store.getters.tokenType + ' ' + store.getters.accessToken
    //     }
    //   }).then(res => {
    //     console.log(res)
    //   }).catch(err => {
    //     if (err.response) {
    //       const res = err.response

    //       console.log(res.data)
    //       console.log(res.status)
    //       console.log(res.headers)
    //     }
    //   })
    // }

    const login = () => {
      mgr.login();
    };

    const logout = () => {
      mgr.logout();
    };

    onMounted(() => {
      console.log("Home onMounted is invoked!");
    });

    return {
      isAuthenticated: computed(() => store.getters.isAuthenticated),
      profile: computed(() => store.getters.profile),
      login,
      logout,
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