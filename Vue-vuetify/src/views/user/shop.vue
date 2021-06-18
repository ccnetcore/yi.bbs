<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" class="text-center" v-show="is_null">
        商城货物已被购空。。。。。。<br />
        请等待新货物上架
      </v-col>
      <v-col v-for="(item, index) in form" :key="index" cols="6" sm="6" md="3">
        <v-hover v-slot="{ hover }">
          <v-card :elevation="hover ? 12 : 2" class="pa-4">
            <v-icon class="ml-4 mb-2" large :color="item.prop.color">
              {{ item.prop.logo }}
            </v-icon>
            {{ item.prop.name }}

            <br />
            <v-subheader>{{ item.prop.introduction }}</v-subheader>
            <v-divider></v-divider>

            <p class="mt-2">原价：{{ item.price }}</p>

            <p>折扣：{{ item.discount }}</p>
            <p>折后价格：{{ item.real_price }}</p>
            <span class="float-right">剩余数量：{{ item.number }}</span>
            <v-divider></v-divider>
            <v-btn class="mt-2" dark @click="openBuy(item.id)" color="cyan"
              >购买</v-btn
            >
          </v-card>
        </v-hover>
      </v-col>
    </v-row>

    <!-- 删除提示框 -->
    <v-dialog v-model="dialog" max-width="500px">
      <v-card>
        <v-card-title class="headline">你确定要购买该道具吗？</v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="close">取消</v-btn>
          <v-btn color="blue darken-1" text @click="buy">确定</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>
<script>
import shopApi from "@/api/shopApi";

export default {
  data() {
    return {
      dialog: false,
      shopId: 0,
      is_null: false,
      form: [],
    };
  },
  created() {
    this.initializa();
  },
  methods: {
    openBuy(id) {
      this.shopId = id;
      this.dialog = true;
    },
    close() {
      this.shopId = 0;
      this.dialog = false;
    },
    buy() {
      shopApi.BuyShop(this.shopId).then((resp) => {
        if (resp.status) {
          this.$dialog.notify.success(resp.msg, {
            position: "top-right",
          });
        } else {
          this.$dialog.notify.error(resp.msg, {
            position: "top-right",
          });
        }
        this.close();
      this.initializa();
      });

      
    },
    initializa() {
      shopApi.getShops().then((resp) => {
        if (resp.data.length == 0) {
          this.is_null = true;
        }
        this.form = resp.data;
      });
    },
  },
};
</script>