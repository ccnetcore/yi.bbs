<template>
  <v-container fluid>
    <v-row>
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
                  <v-text-field label="聊天室Id"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field label="聊天室密码"></v-text-field>
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

      <v-col cols="2" class="pb-0 d-none d-xl-flex">
        <v-card height="100%" width="100%" class="mx-auto">
          <v-list>
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
                  添加聊天室
                </v-btn>
              </v-list-item-content>
            </v-list-item>

            <v-divider></v-divider>

            <v-list dense nav>
              <v-list-item
                v-for="(myitem, index) in items"
                :key="index"
                link
                @click="myitem.method"
              >
                <v-list-item-content>
                  <v-list-item-title>
                    {{ myitem.name }}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-list>
        </v-card>
      </v-col>

      <v-col cols="12" xl="10" class="elevation-2">
        <v-row>
          <v-col cols="4" md="6">
            <div class="text-h5 title pt-6">
              <v-icon class="mx-2">mdi-menu</v-icon>聊天
            </div>
          </v-col>
          <v-col cols="2" md="2"></v-col>

          <v-col cols="3" md="2">
            <v-btn dark color="blue" @click="con"> 开始连接 </v-btn>
          </v-col>
          <v-col cols="3" md="2">
            <v-btn dark color="red" @click="closehub"> 断开连接 </v-btn>
          </v-col>
        </v-row>

        <v-row justify="center">
          <v-container>
            <v-card>
              <div
                ref="context"
                class="grey lighten-5"
                style="overflow-y: auto; height: 500px"
              >
                <div
                  v-for="(item, index) in form"
                  :key="index"
                 
                >
                  <div  v-show="item.id == myform.id">
                    <v-avatar size="40" class="elevation-2 ml-6 my-4">
                      <img
                        @click="intoInfo(item.id)"
                        :src="
                          baseurl + '/File/ShowNoticeImg?filePath=' + item.icon
                        "
                      />
                    </v-avatar>

                    <v-tooltip bottom>
                      <template v-slot:activator="{ on, attrs }">
                        <span
                          v-bind="attrs"
                          v-on="on"
                          class="pa-3 rounded-lg ml-2 elevation-2 mr-6"
                          :style="{ backgroundColor: 'white' }"
                          v-html="item.context"
                        ></span>
                      </template>
                      <span>发表时间：{{ item.time }}</span>
                    </v-tooltip>
                  </div>

                  <div class="text-right"  v-show="item.id != myform.id">
                    <v-tooltip bottom>
                      <template v-slot:activator="{ on, attrs }">
                        <span
                          v-bind="attrs"
                          v-on="on"
                          class="pa-3 rounded-lg ml-2 elevation-2"
                          :style="{ backgroundColor: 'white' }"
                          v-html="item.context"
                        ></span>
                      </template>
                      <span>发表时间：{{ item.time }}</span>
                    </v-tooltip>

                    <v-avatar size="40" class="elevation-2 ml-2 my-4 mr-6">
                      <img
                        @click="intoInfo(item.id)"
                        :src="
                          baseurl + '/File/ShowNoticeImg?filePath=' + item.icon
                        "
                      />
                    </v-avatar>
                  </div>
                </div>
              </div>

              <v-text-field
                label="输入你的聊天内容"
                v-model="myform.context"
                filled
                dense
                @keyup.enter="send"
              >
                <v-btn
                  slot="append"
                  fab
                  color="cyan"
                  small
                  class="mb-2"
                  @click="send"
                >
                  <v-icon color="white"> mdi-send </v-icon>
                </v-btn>
              </v-text-field>
            </v-card>
          </v-container>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  data() {
    return {
      myform: {
        id: 0,
        context: "",
        icon: "",
        time: "",
      },

      my: false,
      dialog: false,
      items: [
        { name: "#聊天大厅", method: "" },
        { name: "#我的聊天室", method: "" },
        { name: "#工作室", method: "" },
        { name: "#项目团队", method: "" },
        { name: "#其他", method: "" },
      ],
      baseurl: "",
      form: [],
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
    console.log(this.signalr.connectionState);

    this.myform.id = this.$store.state.user.user.id;
    (this.myform.icon = this.$store.state.user.user.icon),
      this.signalr.off("Show");
    var that = this;
    this.signalr.on("Show", (resp) => {
      that.form.push(resp);
      setTimeout(() => {
        that.$refs.context.scrollTop = that.$refs.context.scrollHeight;
      }, 100);
    });
    this.initializa();
  },
  mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  methods: {
    con() {
      console.log(this.signalr);

      this.signalr
        .start()
        .then(() => {
          alert("已连接");
        })
        .catch((err) => {
          alert("连接错误" + err);
        });
    },
    send() {
      this.signalr.invoke("SendMsg", this.myform);
      this.myform.context = "";
    },
    initializa() {
      if (
        this.$route.query.userId == undefined ||
        this.$route.query.userId == this.$store.state.user.user.id
      ) {
        //表示是自己的用户
        this.my = true;
      }
    },
    Add() {
      this.dialog = true;
    },
    close() {
      this.dialog = false;
    },
    closehub() {
      this.signalr.stop();
      alert("已断开连接");
    },
    save() {
      this.close();
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