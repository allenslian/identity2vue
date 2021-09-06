<template>
  <p>Start to logining...</p>
</template>

<script>
import { onMounted } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useOidcClient } from "@/composables/useOidcClient";

export default {
  name: "SSO",
  setup() {
    const router = useRouter();
    const store = useStore();
    const mgr = useOidcClient(router, store);

    onMounted(async () => {
      console.log("home SSO onMounted is invoked!");
      console.log("relogin successfully!");
      mgr.login();
    });
  },
};
</script>

<style>
</style>