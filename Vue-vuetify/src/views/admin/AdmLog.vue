<template>
  <v-card>
    <v-data-table
      :headers="headers"
      :items="desserts"
      sort-by="calories"
      class="elevation-1"
      item-key="id"
      show-select
      v-model="selected"
      :search="search"
    >
      <template v-slot:top>
        <!-- 搜索框 -->
        <v-toolbar flat>
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            append-icon="mdi-magnify"
            label="搜索"
            single-line
            hide-details
            class="mx-4"
          ></v-text-field>
        </v-toolbar>
      </template>

      <!-- 表格中的删除和修改 -->
      <template v-slot:item.actions="{ item }">
        <v-icon small class="mr-2" @click="down(item)"> mdi-pencil </v-icon>
      </template>

      <!-- 初始化 -->
      <template v-slot:no-data>
        <v-btn color="primary" @click="initialize"> 刷新 </v-btn>
      </template>
    </v-data-table>
  </v-card>
</template>
<script>
//【2】这里设置对应的API
import fileApi from "@/api/fileApi";
export default {
  data: () => ({
    page: 1,
    selected: [],
    search: "",
    //【3】这里设置对应的模型字段
    headers: [
      { text: "日志名", value: "name", sortable: false },
      { text: "操作", value: "actions", sortable: false },
    ],
    desserts: [],
  }),

  created() {
    this.initialize();
  },

  methods: {
      down(item)
      {
         const baseurl = process.env.VUE_APP_BASE_API;
          const mylink=baseurl+`/File/showLog?filepath=${item.name}`
        var tempwindow=window.open();
      tempwindow.location=mylink;    
      },
    initialize() {
      fileApi.getLogs().then((resp) => {
        const response = resp.data;
        this.desserts = response;
      });
    },
  },
};
</script>