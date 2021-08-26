<template>
  <el-container direction="vertical">
    <el-row>
      <el-col>
        <el-button style="float: left">新建产品</el-button>
        <el-button type="primary" @click="getProducts">
          <el-icon style="vertical-align: middle">
            <search />
          </el-icon>
        </el-button>
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
        </el-table>
      </el-col>
    </el-row>

    <!--
    <el-dialog title="Shipping address" v-model="dialogFormVisible">
      <el-form :model="form">
        <el-form-item label="Promotion name" :label-width="formLabelWidth">
          <el-input v-model="form.name" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="Zones" :label-width="formLabelWidth">
          <el-select v-model="form.region" placeholder="Please select a zone">
            <el-option label="Zone No.1" value="shanghai"></el-option>
            <el-option label="Zone No.2" value="beijing"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogFormVisible = false">Cancel</el-button>
          <el-button type="primary" @click="dialogFormVisible = false"
            >Confirm</el-button
          >
        </span>
      </template>
    </el-dialog>
    -->
  </el-container>
</template>

<script>
import { defineComponent, getCurrentInstance, onMounted, ref } from "vue";

export default defineComponent({
  name: "ProductList",
  setup() {
    const products = ref([]);
    const { proxy } = getCurrentInstance();
    const getProducts = () => {
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
    };

    onMounted(() => {
      console.log("ProductList onMounted is invoked!");
      getProducts();
    });

    return {
      products,
      getProducts,
    };
  },
});
</script>

<style>
</style>