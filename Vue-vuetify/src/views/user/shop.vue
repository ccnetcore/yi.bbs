<template>
  <v-container>
    <v-row>
        <v-col cols="12" class="text-center" v-show="is_null">
            商城货物已被购空。。。。。。<br>
            请等待新货物上架
        </v-col>
      <v-col v-for="(item,index) in form" :key="index" cols="6" sm="6" md="3">
        <v-hover v-slot="{ hover }">
          <v-card :elevation="hover ? 12 : 2" class="pa-4 ">
            <v-icon class="ml-4 mb-2" large :color="item.prop.color">
              {{item.prop.logo}}
            </v-icon>
            {{item.prop.name}}

            <br />
            <v-subheader
              >{{item.prop.introduction}}</v-subheader
            >
<v-divider ></v-divider>
            
            <p class="mt-2" >原价：{{item.price}}</p>
            
            <p>折扣：{{item.discount}}</p>
            <p>折后价格：{{item.real_price}}</p>
                        <span class="float-right ">剩余数量：{{item.number}}</span>
                        <v-divider ></v-divider>
                        <v-btn class="mt-2" dark color="cyan">购买</v-btn>
          </v-card>
        </v-hover>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import shopApi from "@/api/shopApi";
export default {
  data() {
    return {
        is_null:false,
      form: [],
    };
  },
  created() {
    this.initializa();
  },
  methods: {
    initializa() {
      shopApi.getShops().then((resp) => {
          if(resp.data.length==0)
          {
              this.is_null=true;
          }
        this.form = resp.data;
      });
    },
  },
};
</script>