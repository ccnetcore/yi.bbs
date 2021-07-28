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
          <!-- 设置特殊权限提示框 -->
          <v-dialog v-model="dialogAction" max-width="500px">
            <v-card>
              <v-card-title>
                <span class="headline">设置特殊权限</span>
              </v-card-title>

              <v-card-text>
                <v-container fluid>
                  <v-col cols="12">
                    <v-combobox
                      v-model="selectAction"
                      :items="itemsAction"
                      label="点击选择特殊权限"
                      multiple
                      chips
                    ></v-combobox>
                  </v-col>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeAction">
                  取消
                </v-btn>
                <v-btn color="blue darken-1" text @click="saveAction">
                  保存
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <!-- 设置角色提示框 -->
          <v-dialog v-model="dialogRole" max-width="500px">
            <v-card>
              <v-card-title>
                <span class="headline">设置角色</span>
              </v-card-title>

              <v-card-text>
                <v-container fluid>
                  <v-col cols="12">
                    <v-combobox
                      v-model="selectRole"
                      :items="itemsRole"
                      label="点击选择角色"
                      multiple
                      chips
                    ></v-combobox>
                  </v-col>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeRole">
                  取消
                </v-btn>
                <v-btn color="blue darken-1" text @click="saveRole">
                  保存
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>

          <v-btn color="cyan" dark class="mb-2" @click="addListRole=true;dialogRole = true">
            设角
          </v-btn>

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
                添加
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
                      <v-text-field
                        v-model="editedItem.username"
                        label="用户名"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.password"
                        label="密码"
                      ></v-text-field>
                    </v-col>

                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.icon"
                        label="头像"
                      ></v-text-field>
                    </v-col>

                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.email"
                        label="邮箱"
                      ></v-text-field>
                    </v-col>

                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.nick"
                        label="昵称"
                      ></v-text-field>
                    </v-col>

                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.openId"
                        label="QQid"
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
            删除
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
        <v-icon small class="mr-2" @click="setRoleItem(item)">
          mdi-gavel
        </v-icon>

        <v-icon small @click="setActionItem(item)"> mdi-lock </v-icon>
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
import userApi from "@/api/userApi";
import roleApi from "@/api/roleApi";
import actionApi from "@/api/actionApi";
export default {
  data: () => ({
    addListRole:false,
    allAction: [],
    selectAction: [],
    itemsAction: [],
    dialogAction: false,
    // -------------------------------
    allRole: [],
    selectRole: [],
    itemsRole: [],
    dialogRole: false,
    // -----------------------------
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
      { text: "用户名", value: "username", sortable: false },
      { text: "密码", value: "password", sortable: false },
      { text: "头像", value: "icon", sortable: false },
      { text: "昵称", value: "nick", sortable: false },
      { text: "邮箱", value: "email", sortable: false },
      { text: "QQId", value: "openid", sortable: false },
      { text: "ip", value: "ip", sortable: false },
      { text: "操作", value: "actions", sortable: false },
    ],
    desserts: [],
    editedIndex: -1,
    //【4】这里设置对应的模型默认字段
    editedItem: {
      user_name: "",
      password: "",
      icon: "",
      nick: "",
      email: "",
      openid: "",
      ip: "",
    },
    defaultItem: {
      user_name: "",
      password: "",
      icon: "",
      nick: "",
      email: "",
      openid: "",
      ip: "",
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
      userApi.getAllUser().then((resp) => {
        const response = resp.data;
        this.desserts = response;
      });

      roleApi.getRoles().then((resp) => {
        const response = resp.data;
        this.allRole = response;
        this.itemsRole = [];

        for (let i = 0; i < response.length; i++) {
          this.itemsRole.push(response[i].role_name);
        }
      });

      actionApi.getActions().then((resp) => {
        const response = resp.data;
        this.allAction = response;
        this.itemsAction = [];

        for (let i = 0; i < response.length; i++) {
          this.itemsAction.push(response[i].action_name);
        }
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
      userApi.delUserList(Ids).then(() => this.initialize());
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
      if (this.editedIndex > -1) {
        userApi.updateUser(this.editedItem).then(() => this.initialize());
      } else {
        userApi.addUser(this.editedItem).then(() => this.initialize());
      }
      this.close();
    },

    // ----------------------------------------------------------------------
    setRoleItem(item) {
      //forEach中this指向已经发生变化
      //获取所有角色

      this.editedIndex = this.desserts.indexOf(item);
      this.editedItem = Object.assign({}, item);
      //获取已经存在的角色
      userApi.getRoleByuserId(this.editedItem.id).then((resp) => {
        const response = resp.data;
        for (let i = 0; i < response.length; i++) {
          this.selectRole.push(response[i].role_name);
        }
      });
      this.dialogRole = true;
    },
    closeRole() {
      this.addListRole=false;
      this.dialogRole = false;
      this.selectRole = [];
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    saveRole() {
      //保存角色(如果点击的是 批量设角色并且选择了目标 那就进行批量保存，否则，只设置一个)
      var Ids = [];
      const myAllRoles = this.allRole;
      for (let i = 0; i < myAllRoles.length; i++) {
        var resule = this.selectRole.find(function (item) {
          return item == myAllRoles[i].role_name;
        });
        if (resule) {
          Ids.push(myAllRoles[i].id);
        }
      }

      //Ids为选中的角色列表

      if (this.addListRole) {
        if (this.selected.length > 0) {
        
 let userIds = this.selected.map(obj => {
   return obj.id; })
 
          userApi.setRoleList(userIds, Ids).then(() => {
            this.closeRole();
          });
        } else {
          this.$dialog.notify.error("请选择至少一个用户进行批量设置", {
            position: "top-right",
            timeout: 5000,
          });
           this.closeRole();
        }
      } else {
        userApi.setRole(this.editedItem.id, Ids).then(() => {
          this.closeRole();
        });
      }
    },
    // -----------------------------------------
    setActionItem(item) {
      //forEach中this指向已经发生变化
      //获取所有权限

      this.editedIndex = this.desserts.indexOf(item);
      this.editedItem = Object.assign({}, item);
      //获取已经存在的特殊权限
      userApi.getSpecialAction(this.editedItem.id).then((resp) => {
        const response = resp.data;
        for (let i = 0; i < response.length; i++) {
          this.selectAction.push(response[i].action_name);
        }
      });
      this.dialogAction = true;
    },
    closeAction() {
      this.dialogAction = false;
      this.selectAction = [];
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    saveAction() {
      var Ids = [];
      const myAllAction = this.allAction;
      for (let i = 0; i < myAllAction.length; i++) {
        var resule = this.selectAction.find(function (item) {
          return item == myAllAction[i].action_name;
        });
        if (resule) {
          Ids.push(myAllAction[i].id);
        }
      }
      userApi.setSpecialAction(this.editedItem.id, Ids).then(() => {
        this.closeAction();
      });
    },
  },
};
</script>