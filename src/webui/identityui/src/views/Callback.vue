<template>
  <el-alert title="Processing callback..." type="info" effect="dark"></el-alert>
</template>

<script>
import { onMounted } from "@vue/runtime-core";
import { useOidcClient } from "../composables/useOidcClient";
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';

export default {
  name: "Callback",
  setup() {
    const router = useRouter()
    const store = useStore()
    const { loginCallback } = useOidcClient(router, store)
    onMounted(() => {
      console.log('Callback onMounted is invoked!')
      loginCallback();
    });
  }
};
</script>

<style>
</style>