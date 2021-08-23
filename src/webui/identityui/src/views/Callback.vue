<template>
  <el-alert title="Processing callback..." type="info" effect="dark">
  </el-alert>
</template>

<script>
import { onMounted } from "@vue/runtime-core";
import { useSignInCallbackClient } from "../composables/useOidcClient";

export default {
  name: "Callback",
  setup() {
    const { signInCallback } = useSignInCallbackClient();
    onMounted(() => {
      console.log('callback -> on mounted');
      signInCallback();
    });
  },
};
</script>

<style>
</style>