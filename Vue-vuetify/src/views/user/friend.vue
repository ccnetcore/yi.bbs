<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" md="8">
        <v-row>
          <v-col cols="12">
            <p class="title text--secondary">好友列表</p>
          </v-col>
          <v-col cols="12">
            <v-text-field label="好友搜索" prepend-inner-icon="mdi-magnify">
              <v-icon slot="append" color="cyan"> mdi-send </v-icon>
            </v-text-field>
          </v-col>
          <v-col cols="12">
            <p v-show="showFriend" class="text-center text--secondary">
              空空如也，快快添加你的好友吧！
            </p>
          </v-col>
          <v-col
            cols="12"
            sm="6"
            md="4"
            v-for="(item, index) in friendList"
            :key="index"
          >
            <v-hover v-slot="{ hover }">
              <v-card
                class="mx-auto"
                color="cyan"
                dark
                :elevation="hover ? 12 : 2"
              >
                <v-card-title>
                  <v-row>
                    <v-col cols="2">
                      <v-icon large left> mdi-account-circle </v-icon></v-col
                    >

                    <v-col cols="8" class="text-center">
                      <span class="text-h8 font-weight-light">{{
                        item.user.username
                      }}</span></v-col
                    >

                    <v-col cols="2">
                      <v-icon @click="openItem(item.id)"> mdi-close </v-icon>
                    </v-col>
                  </v-row>
                </v-card-title>

                <v-card-text class="text-h7 font-weight-bold">
                  "{{ item.user.user_extra.introduction }}"
                </v-card-text>

                <v-card-actions>
                  <v-list-item class="grow">
                    <v-list-item-avatar color="grey darken-3">
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
                    </v-list-item-avatar>

                    <v-list-item-content>
                      <v-list-item-title>{{
                        item.user.nick
                      }}</v-list-item-title>
                    </v-list-item-content>

                    <v-row align="center" justify="end">
                      <div>
                        <v-icon class="mr-1"> mdi-close </v-icon>
                        <span class="subheading mr-2">聊天</span>
                      </div>

                      <span class="mr-1">·</span>
                      <div>
                        <v-icon class="mr-1"> mdi-close </v-icon>
                        <span class="subheading">赠礼</span>
                      </div>
                    </v-row>
                  </v-list-item>
                </v-card-actions>
              </v-card>
            </v-hover>
          </v-col>
        </v-row>
      </v-col>

      <v-col cols="12" md="4">
        <v-row>
          <v-col cols="12">
            <p class="title text--secondary">请求列表</p>
          </v-col>
          <v-col cols="12" class="text-center">
            <v-btn width="100%" color="cyan" dark @click="openAdd()"
              >添加好友</v-btn
            >
          </v-col>
          <v-col cols="12">
            <p v-show="showNoit" class="text-center text--secondary">
              空空如也，暂无请求！
            </p>
          </v-col>
          <v-col
            cols="12"
            v-for="(item, index) in friendNoticeList"
            :key="index"
          >
            <v-hover v-slot="{ hover }">
              <v-card
                class="mx-auto"
                color="cyan"
                dark
                :elevation="hover ? 12 : 2"
              >
                <v-card-title>
                  <v-row>
                    <v-col cols="2">
                      <v-icon large left> mdi-account-circle </v-icon></v-col
                    >

                    <v-col cols="8" class="text-center">
                      <span class="text-h8 font-weight-light">{{
                        item.user.username
                      }}</span></v-col
                    >
                  </v-row>
                </v-card-title>

                <v-card-text class="text-h7 font-weight-bold">
                  "{{ item.user.user_extra.introduction }}"
                </v-card-text>

                <v-card-actions>
                  <v-list-item class="grow">
                    <v-list-item-avatar color="grey darken-3">
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
                    </v-list-item-avatar>

                    <v-list-item-content>
                      <v-list-item-title>{{
                        item.user.nick
                      }}</v-list-item-title>
                    </v-list-item-content>

                    <v-row align="center" justify="end">
                      <div @click="updateFriend(item.id)">
                        <v-icon class="mr-1">
                          mdi-account-multiple-check
                        </v-icon>
                        <span class="subheading mr-2">同意</span>
                      </div>
                      <span class="mr-1">·</span>
                      <div @click="openItem(item.id)">
                        <v-icon class="mr-1">
                          mdi-account-multiple-remove
                        </v-icon>
                        <span class="subheading">拒绝</span>
                      </div>
                    </v-row>
                  </v-list-item>
                </v-card-actions>
              </v-card>
            </v-hover>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
    <!-- 删除提示框 -->
    <v-dialog v-model="dialog" max-width="500px">
      <v-card>
        <v-card-title class="headline">你确定要删除该项吗？</v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="close">取消</v-btn>
          <v-btn color="blue darken-1" text @click="delFriend()">确定</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- 添加提示框 -->
    <v-dialog v-model="addDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">添加好友</span>
        </v-card-title>

        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field v-model="editName" label="用户名"></v-text-field>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closeAdd"> 取消 </v-btn>
          <v-btn color="blue darken-1" text @click="addFriend"> 添加 </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>
<script>
import friendApi from "@/api/friendApi";
export default {
  data: () => ({
    friendList: [],
    friendNoticeList: [],
    baseurl: "",
    editId: 0,
    dialog: false,
    addDialog: false,
    editName: "",
    showFriend: false,
    showNoit: false,
  }),
  created() {
    this.initializa();
  },
  mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  methods: {
    intoInfo(userId) {
      this.$router.push({
        path: `/userInfo`,
        query: {
          userId: userId,
        },
      });
    },
    initializa() {
      friendApi.GetFriends().then((resp) => {
        if (resp.data.length == 0) {
          this.showFriend = true;
        }
        this.friendList = resp.data;
      });

      friendApi.GetFriendsNotice().then((resp) => {
        if (resp.data.length == 0) {
          this.showNoit = true;
        }
        this.friendNoticeList = resp.data;
      });
    },
    openItem(id) {
      this.editId = id;
      this.dialog = true;
    },
    openAdd() {
      this.addDialog = true;
    },
    close() {
      this.editId = 0;
      this.dialog = false;
    },
    closeAdd() {
      (this.editName = ""), (this.addDialog = false);
    },
    delFriend() {
      friendApi.delFriendList([this.editId]).then((resp) => {
        this.$dialog.notify.error(resp.msg, {
          position: "top-right",
        });
        this.close();
        this.initializa();
      });
    },
    updateFriend(id) {
      friendApi.UpdateFriend(id).then((resp) => {
        this.$dialog.notify.success(resp.msg, {
          position: "top-right",
        });
        this.initializa();
      });
    },
    addFriend() {
      friendApi.AddFriend(this.editName).then((resp) => {
        if (resp.status) {
          this.$dialog.notify.success(resp.msg, {
            position: "top-right",
          });
        } else {
          this.$dialog.notify.error(resp.msg, {
            position: "top-right",
          });
        }
        this.closeAdd();
        this.initializa();
      });
    },
  },
};
</script>