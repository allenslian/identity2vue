<template>
  <div>
    <el-container>
      <el-header>
        <el-row>
          <el-col :span="12"> </el-col>
          <el-col :span="12" :offset="6">
            <el-menu mode="horizontal">
              <el-submenu index="1" :title="username">
                <el-menu-item index="1" @click="logout">Logout</el-menu-item>
              </el-submenu>
            </el-menu>
          </el-col>
        </el-row>
      </el-header>
      <el-container>
        <el-aside width="200px">
          <el-menu :default-openeds="['1']">
            <el-submenu index="1">
              <template #title><i class="el-icon-message"></i>Home</template>
              <el-menu-item-group>
                <template #title>Group 1</template>
                <el-menu-item index="1-1">Option 1</el-menu-item>
                <el-menu-item index="1-2">Option 2</el-menu-item>
              </el-menu-item-group>
            </el-submenu>
          </el-menu>
        </el-aside>
        <el-main>
          <router-view name="mainContent"></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import { onMounted } from "@vue/runtime-core"
import { useSignInClient } from '../composables/useOidcClient'

export default {
  name: "Home",
  setup() {
    const { logout } = useSignInClient()

    onMounted(() => {
      console.log("onMounted is invoked!")
    })

    return {
      logout
    }
  },
};
</script>

<style>
</style>