<template>
    <div v-if="$auth.$state.loggedIn">
        <p>Hello, {{ $auth.$state.user }}</p>
    </div>
    <div v-else>
        <h2 class="text-center">Login</h2>
        <hr />
        <el-button type="primary" @click="login">login</el-button>
    </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
    middleware: ["auth"],
    methods: {
        login() {
            this.$auth.loginWith("oidc");
        },
    },
});
</script>
