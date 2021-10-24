<template>
    <div>
        <p>Hello World!!</p>
        <el-button type="primary" @click="login">login</el-button>
    </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
    middleware: "auth",
    methods: {
        login() {
          this.$auth.loginWith("oidc");
        },
    },
});
</script>
