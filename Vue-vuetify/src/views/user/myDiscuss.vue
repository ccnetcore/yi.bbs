<template>
  <v-container fluid>
    <v-row>
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

  
      <!-- 添加提示框 -->
      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">添加标签</span>
          </v-card-title>

          <v-card-text>
            <v-container>
              <!-- 【1】这里设置对应的编辑框名 -->
              <v-row>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field
                    v-model="form.name"
                    label="标签名"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field
                    v-model="form.color"
                    label="文字颜色"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field
                    v-model="form.color_bg"
                    label="背景颜色"
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

      <v-col  class="pb-0">
        <v-card height="100%" class="mx-auto">
          <v-navigation-drawer right  app v-model="drawer"
 >
            <v-list-item>
              <v-list-item-content>
                <v-btn
             v-if="my"
                  color="cyan"
                  elevation="2"
                  @click="Add"
                  large
                  dark
                >
                  添加标签
                </v-btn>
              </v-list-item-content>
            </v-list-item>

            <v-divider></v-divider>

            <v-list dense nav>
              <v-list-item link @click="initializa()">
                <v-list-item-content>
                  <v-list-item-title class="text-center">
                   <v-chip>    <v-icon> mdi-star </v-icon> 全部</v-chip>
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-list-item
                v-for="myitem in items"
                :key="myitem.id"
                link
        
              >
        <v-list-item-content>
                  <v-list-item-title class="text-center">
                    <v-chip

                    @click="selctDiscuss(myitem.id)"
               
                      :style="{
                        backgroundColor: myitem.color_bg,
                        color: myitem.color,
                      }"
                      >{{ myitem.name }}
                      <v-icon v-if="my" @click="deleteItem(myitem.id)">mdi-close-circle</v-icon>
   
                      </v-chip
                    >
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-navigation-drawer>
        </v-card>
      </v-col>

      <v-col cols="12"  class="elevation-2 mt-3">
        <v-row>
          <v-col cols="4" md="6">
            <div class="text-h5 title pt-6"><v-icon class="mx-2"  @click="drawer = !drawer">mdi-menu</v-icon>我的</div>
          </v-col>
          <v-col cols="8" md="6">
            <v-text-field label="搜索">
              <v-icon slot="append" color="red">
                mdi-plus
              </v-icon></v-text-field
            >
          </v-col>
        </v-row>

        <v-row justify="center">
          <v-container>
            <v-expansion-panels inset focusable>
              <v-expansion-panel
                class="p-1"
                v-for="(item, i) in discussList"
                :key="i"
              >
                <v-expansion-panel-header>
                  <v-row align="center" class="spacer" no-gutters>
                                    <v-col cols="2" sm="1" md="1">
                      <v-avatar size="36px " @click="intoInfo(item.user.id)">
                        <img
                          alt="Avatar"
                          :src="
                            baseurl +
                            '/File/ShowNoticeImg?filePath=' +
                            item.user.icon
                          "
                        />
                      </v-avatar>
                      <br>
                  <span class="ml-2">     {{ item.user.username }}</span>
                    </v-col>
                    <v-col cols="6" sm="5" md="8" :style="{'color':item.color}">
                      [{{ item.type }}] {{ item.title }}
                    </v-col>

                    <v-col cols="4" sm="5" md="3">
                      <v-subheader>
                  {{ item.time }} 发布    <br />
              {{item.see_num}}阅览 | {{item.agree_num}}点赞         </v-subheader
                      >
                    </v-col>
                  </v-row>
                </v-expansion-panel-header>

                <v-expansion-panel-content>
                  {{ item.introduction }}
                  <div class="mt-2">
                    <v-btn
                      class="mr-2"
                      fab
                      small
                      dark
                      color="blue"
                      @click="intoComment(item.id, item.plate.id)"
                    >
                      <v-icon dark > mdi-login </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark> mdi-format-list-bulleted-square </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark > mdi-pencil </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark > mdi-android </v-icon>
                    </v-btn>
                  </div>
                </v-expansion-panel-content>
              </v-expansion-panel>
            </v-expansion-panels>
          </v-container>
        </v-row>
      </v-col>
    </v-row>

    <div class="text-center mt-4 mb-10">
      <v-pagination
        v-model="pageIndex"
        :length="lenData"
        :total-visible="7"
        prev-icon="mdi-menu-left"
        next-icon="mdi-menu-right"
        circle
        color="cyan"
      ></v-pagination>
    </div>
  </v-container>
</template>
<script>
import discussApi from "@/api/discussApi";
import labelApi from "@/api/labelApi";
export default {
  data() {
    return {
      my:false,
      drawer:"true",
      dialogDelete: false,
      delId: 0,
      dialog: false,
      form: {
        color: "",
        color_bg: "",
        name: "",
      },
      defaultForm: {
        color: "",
        color_bg: "",
        name: "",
      },
      items: [],
      plateId: "",
      baseurl: "",
      pageIndex: 1,
      pageSize: 10,
      total: 100,
      discussList: [],
    };
  },
  watch: {
    pageIndex: {
      handler() {
        this.initializa();
      },
    },
  },
  created() {
    this.initializa();
  },
  mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  computed: {
    lenData() {
      return Math.floor(this.total / this.pageSize) + 1;
    },
  },
  methods: {
    initializa() {

      if (this.$route.query.userId==undefined||this.$route.query.userId == this.$store.state.user.user.id) {
        //表示是自己的用户
        this.my = true;

      }

      labelApi.getLabelByUserId(this.$route.query.userId).then((resp) => {
        this.items = resp.data;
      });

      //初始化创建
      discussApi.getDiscussByUserId(this.$route.query.userId,this.pageIndex).then((resp) => {
        this.discussList = resp.data.pageData;
        this.total = resp.data.total;
        this.pageSize = resp.data.pageSize;
      });
    },
    selctDiscuss(id) {
      labelApi.getDiscussByLabelId(this.$route.query.userId,this.pageIndex, id).then((resp) => {
        this.discussList = resp.data.pageData;
        this.total = resp.data.total;
        this.pageSize = resp.data.pageSize;
      });
    },

    deleteItem(id) {
      this.delId = id;
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      labelApi.delLabelList([this.delId]).then(() => this.initializa());
      this.closeDelete();
    },
    closeDelete() {
      this.dialogDelete = false;
    },

    Add() {
      this.dialog = true;
    },
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.form = Object.assign({}, this.defaultForm);
      });
    },
    save() {
      labelApi.addLabelByUserId(this.form).then(() => this.initializa());
      this.close();
    },
    intoComment(discussId, plateId) {
      this.$store.dispatch("set_plateId", plateId);
      this.$store.dispatch("set_discussId", discussId);
      this.$router.push({ path: "/comment" });
    },
        intoInfo(userId) {
      this.$router.push({
        path: `/userInfo`,
        query: {
          userId: userId,
        },
      });
    },
  },
};
</script>