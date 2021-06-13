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

          <!-- 添加提示框 -->
          <v-dialog v-model="dialog" max-width="500px">
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                color="primary"
                dark
                class="mb-2 mx-2"
                v-bind="attrs"
                v-on="on"
              >
                添加新项
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <!-- 【1】这里设置对应的编辑框名 -->
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-select
                        :items="items"
                        filled
                        label="选择道具"
                        v-model="propItem"
                      ></v-select>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.price"
                        label="定价"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.number"
                        label="数量"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.discount"
                        label="折扣"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="close"> 取消 </v-btn>
                <v-btn color="blue darken-1" text @click="save"> 保存 </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>

          <v-btn color="red" dark class="mb-2" @click="deleteItem(null)">
            删除所选
          </v-btn>

          <!-- 删除提示框 -->
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="headline"
                >你确定要删除此条记录吗？</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeDelete"
                  >取消</v-btn
                >
                <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                  >确定</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>

      <!-- 表格中的删除和修改 -->
      <template v-slot:item.actions="{ item }">
        <v-icon small class="mr-2" @click="editItem(item)"> mdi-pencil </v-icon>
        <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
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
import shopApi from "@/api/shopApi";
export default {
  data: () => ({
    items: ["置顶卡", "色彩卡", "匿名卡", "加密卡"],
    page: 1,
    selected: [],
    search: "",
    dialog: false,
    dialogDelete: false,
    //【3】这里设置对应的模型字段
    headers: [
      {
        text: "编号",
        align: "start",
        value: "id",
      },
      { text: "道具", value: "prop.name", sortable: false },
      { text: "定价", value: "price", sortable: false },
      { text: "数量", value: "number", sortable: false },
      { text: "折扣", value: "discount", sortable: false },

      { text: "操作", value: "actions", sortable: false },
    ],
    desserts: [],
    editedIndex: -1,
    propItem:"",
    //【4】这里设置对应的模型默认字段
    editedItem: {
      price: "",
      number: "1",
      discount: "1.00",
    },
    defaultItem: {
      price: "",
      number: "1",
      discount: "1.00",
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "添加数据" : "编辑数据";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  created() {
    this.initialize();
  },

  methods: {
    initialize() {
      //【5】这里获取全部字段的API
      shopApi.getShops().then((resp) => {
        const response = resp.data;
        this.desserts = response;
      });
    },

    editItem(item) {
      this.editedIndex = this.desserts.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.desserts.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      var Ids = [];
      if (this.editedIndex > -1) {
        Ids.push(this.editedItem.id);
      } else {
        this.selected.forEach(function (item) {
          Ids.push(item.id);
        });
      }
      //【6】这里多条删除的API
      shopApi.delShopList(Ids).then(() => this.initialize());
      this.closeDelete();
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    save() {
      //【7】这里编辑和添加的API
              var propId=0;
        switch(this.propItem){
            case "置顶卡":propId=1;break;
            case "色彩卡":propId=2;break;
            case "匿名卡":propId=3;break;
            case "加密卡":propId=4;break;

        }
      if (this.editedIndex > -1) {
        shopApi.updateShop(this.editedItem,propId).then(() => this.initialize());
      } else {

        shopApi.addShop(this.editedItem,propId).then(() => this.initialize());
      }
      this.close();
    },
  },
};
</script>