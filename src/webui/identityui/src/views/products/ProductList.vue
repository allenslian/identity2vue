<template>
  <el-container direction="vertical">
    <el-row>
      <el-col>
        <el-button @click="addProduct">新建产品</el-button>
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
          <el-table-column prop="description" label="备注"> </el-table-column>
          <el-table-column fixed="right" label="操作" width="120">
            <template #default="scope">
              <el-button
                type="text"
                size="small"
                @click="editProduct(scope.row.id)"
                >编辑</el-button
              >
            </template>
          </el-table-column>
        </el-table>
      </el-col>
    </el-row>

    <el-drawer
      :title="drawer.title"
      v-model="drawer.isVisiable"
      size="30%"
      custom-class="cdrawer"
    >
      <div class="cdrawer__content">
        <el-form :model="form">
          <el-form-item label="产品名称" label-width="80px">
            <el-input v-model="form.name" autocomplete="off"></el-input>
          </el-form-item>
          <el-form-item label="产品价格" label-width="80px">
            <el-input v-model="form.price" autocomplete="off"></el-input>
          </el-form-item>
          <el-form-item label="描述" label-width="80px">
            <el-input
              type="textarea"
              v-model="form.description"
              autocomplete="off"
            ></el-input>
          </el-form-item>
        </el-form>
        <div class="cdrawer__footer">
          <el-button @click="close">取消</el-button>
          <el-button type="primary" @click="save" :loading="drawer.loading">{{
            drawer.loading ? "保存..." : "保存"
          }}</el-button>
        </div>
      </div>
    </el-drawer>
  </el-container>
</template>

<script>
import {
  defineComponent,
  getCurrentInstance,
  onMounted,
  reactive,
  ref,
} from "vue";
import debounce from "lodash.debounce";

export default defineComponent({
  name: "ProductList",
  setup() {
    const products = ref([]);
    const drawer = reactive({
      title: "",
      isVisiable: false,
      loading: false,
    });

    const form = reactive({
      id: "",
      name: "",
      description: "",
      price: 0.0,
    });

    const { proxy } = getCurrentInstance();
    const getProducts = () => {
      debounce(function () {
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
      }, 300)();
    };
    const getProduct = (id) => {
      debounce(function () {
        proxy.$http
          .get(`/products/${id}`)
          .then((data) => {
            reset();
            if (data) {
              form.id = data.id;
              form.name = data.name;
              form.description = data.description;
              form.price = data.salesPrice;
            }
          })
          .catch((err) => {
            console.error(err);
          });
      }, 300)();
    };

    const addProduct = () => {
      drawer.title = "新建产品";
      reset();
      drawer.isVisiable = true;
    };

    const editProduct = (id) => {
      drawer.title = "编辑产品";
      getProduct(id);
      drawer.isVisiable = true;
    };

    const reset = () => {
      form.id = "";
      form.name = "";
      form.description = "";
      form.price = 0.0;
    };

    const save = () => {
      debounce(function () {
        let result =
          form.id == ""
            ? proxy.$http.post("/products", {
                name: form.name,
                description: form.description,
                salesPrice: parseFloat(form.price),
              })
            : proxy.$http.put(`/products/${form.id}`, {
                name: form.name,
                description: form.description,
                salesPrice: parseFloat(form.price),
              });
        result
          .then(() => {
            drawer.isVisiable = false;
            getProducts();
          })
          .catch((err) => {
            console.error(err);
          });
      }, 300)();
    };

    const close = () => {
      reset();
      drawer.isVisiable = false;
    };

    onMounted(() => {
      console.log("ProductList onMounted is invoked!");
      getProducts();
    });

    return {
      products,
      drawer,
      form,
      getProducts,
      addProduct,
      editProduct,
      save,
      close,
    };
  },
});
</script>

<style>
.cdrawer {
  padding: 20px;
}
</style>