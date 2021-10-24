<template>
    <div>
        <p>logged: {{ $auth.$state.loggedIn }}</p>
        <p>Hello, {{ $auth.$state.user }}</p>
    </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
    middleware: ["auth"],
});
</script>
