<template>
  <v-container fluid>
    <v-tabs v-model="tab">
      <v-tab href="#tab-1">
        账户信息
        <v-icon>mdi-phone</v-icon>
      </v-tab>

      <v-tab href="#tab-2">
        额外信息
        <v-icon>mdi-account-box</v-icon>
      </v-tab>
    </v-tabs>
    <v-card>
      <v-tabs-items v-model="tab">
        <v-tab-item :value="'tab-2'">
          <v-card class="mx-auto">
            <v-card-text>
              <div>Extra Information</div>
              <v-row>
                <v-col cols="12">
                  <v-list two-line>
                    <v-list-item>
                      <v-list-item-icon>
                        <v-icon color="indigo"> mdi-phone </v-icon>
                      </v-list-item-icon>

                      <v-list-item-content>
                        <v-list-item-title>等级：</v-list-item-title>
                        <v-list-item-subtitle>{{
                          form.user_extra.level
                        }}</v-list-item-subtitle>
                      </v-list-item-content>

                      <v-list-item-icon>
                        <v-icon>mdi-message-text</v-icon>
                      </v-list-item-icon>
                    </v-list-item>

                    <v-list-item>
                      <v-list-item-action></v-list-item-action>

                      <v-list-item-content>
                        <v-list-item-title>经验：</v-list-item-title>
                        <v-list-item-subtitle>{{
                          form.user_extra.experience
                        }}</v-list-item-subtitle>
                      </v-list-item-content>

                      <v-list-item-icon>
                        <v-icon>mdi-message-text</v-icon>
                      </v-list-item-icon>
                    </v-list-item>

                    <v-divider inset></v-divider>

                    <v-list-item>
                      <v-list-item-icon>
                        <v-icon color="indigo"> mdi-lock </v-icon>
                      </v-list-item-icon>

                      <v-list-item-content>
                        <v-list-item-title>拥有角色：</v-list-item-title>
                        <v-list-item-subtitle>
                          <v-row>
                            <v-col
                              v-for="(item, index) in roleList"
                              :key="index"
                              cols="6"
                              sm="3"
                              md="1"
                              >{{ item.role_name }}</v-col
                            >
                          </v-row>
                        </v-list-item-subtitle>
                      </v-list-item-content>
                    </v-list-item>
                    <v-list-item>
                      <v-list-item-action></v-list-item-action>

                      <v-list-item-content>
                        <v-list-item-title>拥有权限：</v-list-item-title>
                        <v-list-item-subtitle>
                          <v-row>
                            <v-col
                              v-for="(item, index) in actionList"
                              :key="index"
                              cols="6"
                              sm="3"
                              md="1"
                              >{{ item.action_name }}</v-col
                            >
                          </v-row>
                        </v-list-item-subtitle>
                      </v-list-item-content>
                    </v-list-item>

                    <v-divider inset></v-divider>

                    <v-list-item>
                      <v-list-item-icon>
                        <v-icon color="indigo"> mdi-email </v-icon>
                      </v-list-item-icon>

                      <v-list-item-content>
                        <v-list-item-title>发帖总数：</v-list-item-title>
                        <v-list-item-subtitle>{{
                          form.user_extra.num_release
                        }}</v-list-item-subtitle>
                      </v-list-item-content>
                    </v-list-item>

                    <v-list-item>
                      <v-list-item-action></v-list-item-action>

                      <v-list-item-content>
                        <v-list-item-title>评论总数：</v-list-item-title>
                        <v-list-item-subtitle>{{
                          form.user_extra.num_reply
                        }}</v-list-item-subtitle>
                      </v-list-item-content>
                    </v-list-item>

                    <v-list-item>
                      <v-list-item-action></v-list-item-action>
                      <v-list-item-content>
                        <v-btn
                          dark
                          color="cyan"
                          @click="intoMyDiscuss()"
                          class="ml-0"
                          dense
                        >
                          查看主题
                        </v-btn>
                      </v-list-item-content>
                    </v-list-item>
                    <v-divider inset></v-divider>
                  </v-list>
                </v-col>

                <v-col cols="12" md="2">
                  <v-card-actions> </v-card-actions>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-tab-item>

        <v-tab-item :value="'tab-1'">
          <v-card class="mx-auto">
            <v-card-text>
              <div>Basic Information</div>
              <v-row>
                <v-col cols="12" md="2" class="text-center align-center">
                  <v-avatar size="100" class="elevation-4 mt-5">
                    <img :src="imgurl" />
                  </v-avatar>
                  <br />
                  <input
                    type="file"
                    ref="imgFile"
                    @change="uploadImage()"
                    class="d-none"
                  />
                  <v-btn
                    v-if="my"
                    dark
                    color="cyan"
                    @click="choiceImg"
                    class="mt-4 mx-2"
                  >
                    编辑头像
                  </v-btn>

                  <v-btn
                    v-if="my"
                    dark
                    color="cyan"
                    class="mt-4"
                    @click="qqUpdate"
                  >
                    绑定QQ
                  </v-btn>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.username"
                    label="用户名"
                    disabled
                  ></v-text-field>

                  <v-text-field
                    v-model="form.nick"
                    required
                    :counter="10"
                    label="昵称"
                    :disabled="!my"
                  ></v-text-field>

                  <v-text-field
                    v-model="form.email"
                    required
                    label="邮箱"
                    disabled
                  ></v-text-field>

                  <v-text-field
                    v-model="form.user_extra.introduction"
                    required
                    label="简介"
                    :counter="50"
                    :disabled="!my"
                  ></v-text-field>

                  <v-divider class="my-8"></v-divider>
                  <div v-if="my">
                    <p>修改密码</p>
                    <v-text-field
                      v-model="form.password"
                      label="原密码"
                      outlined
                      clearable
                    ></v-text-field>
                    <v-text-field
                      v-model="password_new"
                      required
                      :counter="120"
                      label="新密码"
                      :disabled="en_new"
                    ></v-text-field>
                    <v-card-actions>
                      <v-btn dark color="cyan" class="mr-2" @click="clear()">
                        清除
                      </v-btn>
                      <v-btn dark color="cyan" @click="send()"> 保存 </v-btn>
                    </v-card-actions>
                  </div>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-tab-item>
      </v-tabs-items>
    </v-card>
  </v-container>
</template>
<script>
import userApi from "@/api/userApi";
import myqq from "../../utils/myqq";
import axios from "axios";
export default {
  data() {
    return {
      actionList: [],
      roleList: [],
      my: false, //判定打开的是否是自己的用户
      baseurl: "", ///prod-apis或发展模式的基础前缀
      imgurl: "", ///baseurl+图片名
      en_new: true, //密码框是否能输入
      password_new: "",
      form: {
        username: "",
        password: "",
        icon: "",
        email: "",
        user_extra: {
          experience: 0,
          introduction: "",
          level: 0,
          num_reply: 0,
          num_release: 0,
        },
      },
      tab: null,
      picker: new Date().toISOString().substr(0, 10),
      picker2: null,
    };
  },

  created() {
    this.initializa();
  },
  mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  watch: {
    form: {
      //深度监听，可监听到对象、数组的变化
      handler(val, oldVal) {
        if (val.password == "") {
          this.en_new = true;
          this.form.password_new = "";
        } else {
          this.en_new = false;
        }
      },
      deep: true, //true 深度监听
    },
  },
  methods: {
    //   深刻理解一下文件上传：
    // <input type="file" ref="imgFile" @change="uploadImage()" class="d-none" />
    // <v-btn dark color="cyan" @click="choiceImg" class="mt-4">编辑</v-btn>
    // choiceImg：触发input点击事件，这个要绑定要按钮单机事件上
    // uploadImage：发送图片过去，这个要绑定要@change上，当有东西改变

    // 通过ref获取文件，然后通过axios发送文件给后端，后端返回一个回调地址，前端解析一下换成图片和文件名
    qqUpdate() {
      QC.Login.showPopup(myqq.myqqUpdate);
    },
    choiceImg() {
      this.$refs.imgFile.dispatchEvent(new MouseEvent("click"));
    },
    uploadImage() {
      const file = this.$refs.imgFile.files[0];
      let formData = new FormData();
      formData.append("img", file);
      axios
        .post(this.baseurl + "/File/OnPostUploadImage", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then((resp) => {
          this.imgurl = resp.data.data[0].url.replace("#", this.baseurl);

          const stringList = this.imgurl.split("=");
          this.form.icon = stringList[1];
        });
    },
    intoMyDiscuss() {
      this.$router.push({
        path: `/myDiscuss`,
        query: {
          userId: this.$route.query.userId,
        },
      });
    },
    initializa() {
      if (
        this.$route.query.userId == undefined ||
        this.$route.query.userId ==0||
        this.$route.query.userId == this.$store.state.user.user.id
      ) {
        //表示是自己的用户
        this.my = true;
      }

      userApi.getUserByUserId(this.$route.query.userId).then((resp) => {
        this.form = resp.data;
        this.imgurl =
          this.baseurl + "/File/ShowNoticeImg?filePath=" + this.form.icon;
      });

      userApi.getRoleByuserId(this.$route.query.userId).then((resp) => {
        this.roleList = resp.data;
      });
      userApi.getActionByUserId(this.$route.query.userId).then((resp) => {
        this.actionList = resp.data;
      });
    },
    clear() {
      this.form.username = "";
      this.form.password = "";
      this.password_new = "";
      this.form.user_extra.introduction = "";
    },

    send() {
      this.form.password_new = this.password_new;
      userApi.tryUpdateUser(this.form).then((resp) => {
        if (resp.status) {
          //同时更新一下store

          this.$store.dispatch("setIcon", this.form.icon);

          this.$dialog.notify.success(resp.msg, {
            position: "top-right",
            timeout: 5000,
          });
        } else {
          this.$dialog.notify.error(resp.msg, {
            position: "top-right",
            timeout: 5000,
          });
        }
        this.clear();
        this.initializa();
      });
    },
  },
};
</script>