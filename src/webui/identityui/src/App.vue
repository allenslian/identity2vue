<template>
  <div>
    <router-view></router-view>
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isVisible"
      width="20%"
      :show-close="false"
    >
      <p>{{ dialog.message }}</p>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialog.isVisible = false">取消</el-button>
          <el-button type="primary" @click="closeDialog">确认</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { onMounted, onUnmounted, reactive } from "vue"
import { useRouter } from "vue-router"
import { useStore } from "vuex"
//import debounce from "lodash.debounce"

export default {
  name: "App",
  setup() {
    const dialog = reactive({
      title: "",
      isVisible: false,
      message: "",
    });

    const store = useStore();
    const router = useRouter();
    const closeDialog = () => {
      dialog.isVisible = false;
      if (store.getters.error.needRelogin) {
        router.replace({ path: "/" });
      }
    };

    const watcher = store.watch(
      (_state, getters) => getters.error,
      (_ov, nv) => {
        if (nv == null) {
          return
        }
        console.log(nv)
        // debounce(() => {
        //   dialog.title = nv ? "错误提示" : "提示";
        //   dialog.message = nv ? nv.message : "";
        //   dialog.isVisible = true;
        // }, 500)

        dialog.title = nv ? "错误提示" : "提示"
        dialog.message = nv ? nv.message : ""
        dialog.isVisible = true
      }
    );

    onMounted(() => {
      console.log("App onMounted is invoked!");
    });

    onUnmounted(() => {
      console.log("App onUnmounted is invoked!");
      watcher();
    });

    return {
      dialog,
      closeDialog,
    };
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /*
  margin-top: 60px;
  */
}
</style>
