<template>
  <el-container>
    <el-aside width="200px">
      <el-menu :default-openeds="['1']">
        <el-submenu index="1">
          <template #title><i class="el-icon-message"></i>Home</template>
          <el-menu-item-group>
            <template #title>Group 1</template>
            <el-menu-item index="1-1">Option 1</el-menu-item>
            <el-menu-item index="1-2">Option 2</el-menu-item>
          </el-menu-item-group>
        </el-submenu>
      </el-menu>
    </el-aside>
    <el-main>
      <el-button @click="getProducts">load products</el-button>
      <el-button @click="addProduct">add product</el-button>
    </el-main>
  </el-container>
</template>

<script>
import { ref, onMounted } from "@vue/runtime-core"
import { useStore } from 'vuex'
import axios from "axios"

export default {
  name: "Home",
  setup() {
    const store = useStore()
    const products = ref([])
    const getProducts = () => {
      axios.get('https://localhost:6001/products', {
        headers: {
          'Authorization': store.getters.tokenType + ' ' + store.getters.accessToken
        }
      }).then(res => {
        console.log(res)
        res.data.forEach(item => {
          products.value.push(item)
        })
      })
    }

    const addProduct = () => {
      axios.post('https://localhost:6001/products', {
        name: 'product a',
        description: 'a product called a',
        salesPrice: 11.00
      }, {
        headers: {
          'Authorization': store.getters.tokenType + ' ' + store.getters.accessToken
        }
      }).then(res => {
        console.log(res)
      }).catch(err => {
        if (err.response) {
          const res = err.response
          
          console.log(res.data)
          console.log(res.status)
          console.log(res.headers)
        }
      })
    }

    onMounted(() => {
      console.log("Home onMounted is invoked!");
    });

    return {
      getProducts,
      addProduct,
      products
    };
  },
};
</script>

<style>
</style>