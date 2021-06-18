<template>
  <v-container fluid>
    <v-row>
        <v-col cols="12" class="text-center" v-show="is_null">
            你的仓库，空空如也。。。。。。<br>
            请前往商城购买
        </v-col>
      <v-col v-for="(item,index) in form" :key="index" cols="6" sm="6" md="3">
        <v-hover v-slot="{ hover }">
          <v-card :elevation="hover ? 12 : 2" class="pa-4 pb-8">
            <v-icon class="ml-4 mb-2" large :color="item.prop.color">
              {{item.prop.logo}}
            </v-icon>
            {{item.prop.name}}

            <br />
            <v-subheader
              >{{item.prop.introduction}}</v-subheader
            >
            <span class="float-right">数量：{{item.number}}</span>
          </v-card>
        </v-hover>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import warehouseApi from "@/api/warehouseApi";
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
      warehouseApi.GetWarehousesByUserId().then((resp) => {
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