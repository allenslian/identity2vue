<template>
  <el-alert title="Processing callback..." type="info" effect="dark"></el-alert>
</template>

<script>
import { onMounted } from "@vue/runtime-core";
import { useOidcClient } from "../composables/useOidcClient";

export default {
  name: "Callback",
  setup() {
    const { loginCallback } = useOidcClient()
    onMounted(() => {
      console.log('Callback onMounted is invoked!')
      loginCallback();
    });
  }
};
</script>

<style>
</style>