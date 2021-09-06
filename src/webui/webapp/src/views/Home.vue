<template>
  <el-container>
    <el-header>
      <el-row align="middle">
        <el-col :span="12">
          <h3 class="title">World App</h3>
        </el-col>
        <el-col :span="12">
          <el-button
            type="primary"
            plain
            style="float: right"
            @click="login"
            v-if="!isAuthenticated"
            >LOGIN</el-button
          >
          <el-menu mode="horizontal" style="float: right; width: 250px" v-else>
            <el-sub-menu index="1">
              <template #title>{{ profile ? profile.name : "NONE" }}</template>
              <el-menu-item index="1-1" @click="goToHomeApp">Go To Home App</el-menu-item>
              <el-menu-item index="1-2" @click="logout">LOGOUT</el-menu-item>
            </el-sub-menu>
          </el-menu>
        </el-col>
      </el-row>
    </el-header>
    <el-container v-if="isAuthenticated">
      <el-main>
        <el-container direction="vertical">
          <el-row>
            <el-col>
              <el-button
                type="primary"
                icon="el-icon-refresh"
                @click="getProducts"
              ></el-button>
            </el-col>
          </el-row>
          <el-row>
            <el-col>
              <el-table :data="products">
                <el-table-column prop="name" label="名称" width="280">
                </el-table-column>
                <el-table-column prop="salesPrice" label="销售价格" width="180">
                </el-table-column>
                <el-table-column prop="description" label="备注">
                </el-table-column>
              </el-table>
            </el-col>
          </el-row>
        </el-container>
      </el-main>
    </el-container>
    <el-container v-else>
      <p>Please sign in at first!</p>
    </el-container>
  </el-container>
</template>

<script>
import { onMounted, computed, ref, getCurrentInstance } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useOidcClient } from "@/composables/useOidcClient";

export default {
  name: "Home",
  setup() {
    const router = useRouter();
    const store = useStore();
    const mgr = useOidcClient(router, store);

    let isAuthenticated = computed(() => store.getters.isAuthenticated);
    const products = ref([]);

    const login = () => {
      mgr.login();
    };

    const logout = () => {
      mgr.logout();
    };

    const goToHomeApp = () => {
      window.location.href = process.env.VUE_APP_HOME_APP_BASE;
    };

    const { proxy } = getCurrentInstance();
    const getProducts = () => {
      proxy.$http
        .get("/products")
        .then((data) => {
          products.value = [];
          if (data) {
            data.forEach((p) => {
              products.value.push(p);
            });
          }
        })
        .catch((err) => {
          console.error(err);
        });
    };

    onMounted(async () => {
      console.log("webapp App onMounted is invoked!");
      if (isAuthenticated.value) {
        console.log("login successfully!");
        getProducts();
      }
      // else {
      //   console.log("relogin successfully!");
      //   mgr.login();
      // }
    });

    return {
      isAuthenticated,
      profile: computed(() => store.getters.profile),
      products,
      getProducts,
      login,
      logout,
      goToHomeApp
    };
  },
};
</script>

<style>
</style>