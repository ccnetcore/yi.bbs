<template>
  <div>
    <v-card class="mx-auto my-2" max-width="100%">
      <v-card-text>
        <p class="display-1 text--primary">{{ discussData.title }}</p>
        <v-divider class="my-2"></v-divider>
        <v-avatar size="60px" @click="intoInfo(discussData.user.id)">
          <img
            v-if="discussData.user != undefined"
            alt="Avatar"
            :src="
              baseurl + '/File/ShowNoticeImg?filePath=' + discussData.user.icon
            "
          />
        </v-avatar>
        <p v-if="discussData.user != undefined">
          作者：{{ discussData.user.username }}
        </p>

        <p>时间：{{ discussData.time }}</p>
        <v-divider class="my-4"></v-divider>
        <div class="text--primary" v-html="discussData.content"></div>
      </v-card-text>
      <v-card-actions>
        <v-btn text color="teal accent-4" @click="reveal = true">
          更多信息
        </v-btn>
        <v-btn @click="agrees" class="mx-2" dark outlined color="cyan">
          <v-icon large dark> mdi-menu-up-outline </v-icon>
          {{ discussData.agree_num }}
          赞同
        </v-btn>

        <v-menu offset-y>
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              v-bind="attrs"
              v-on="on"
              class="mx-2"
              fab
              dark
              small
              color="cyan"
            >
              <v-icon dark> mdi-credit-card-settings </v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item
              v-for="(item, index) in myPorp"
              :key="index"
              link
              @click="openCard(item.method)"
            >
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-btn class="mx-2" fab dark small color="cyan">
          <v-icon dark @click="Collection(discussData.id)"> mdi-star </v-icon>
        </v-btn>
      </v-card-actions>

      <v-expand-transition>
        <v-card
          v-if="reveal"
          class="transition-fast-in-fast-out v-card--reveal"
          style="height: 100%"
        >
          <v-card-text class="pb-0">
            <p class="display-1 text--primary">简介</p>
            <p>{{ discussData.introduction }}</p>
          </v-card-text>
          <v-card-actions class="pt-0">
            <v-btn text color="teal accent-4" @click="reveal = false">
              关闭
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-expand-transition>
    </v-card>
    <v-card class="my-2 mb-6">
      <v-list>
        <v-subheader>评论</v-subheader>
        <v-text-field
          class="mx-6"
          ref="com"
          v-model="form.content"
          label="发表评论"
          required
          :rules="commentRules"
        >
          <v-icon slot="append" color="blue" @click="sentComment()">
            mdi-send
          </v-icon></v-text-field
        >

        <v-list-item-group v-model="selectedItem" color="blue">
          <v-list-item v-for="(item, i) in commentList" :key="i">
            <v-list-item-icon>
              <v-avatar size="36px" @click="intoInfo(item.user.id)">
                <img
                  alt="Avatar"
                  :src="
                    baseurl + '/File/ShowNoticeImg?filePath=' + item.user.icon
                  "
                />
              </v-avatar>
            </v-list-item-icon>
            <v-list-item-content>
              <p class="light-ayan--text">
                {{ i + 1 }}楼:<br />
                作者：{{ item.user.username
                }}<v-divider class="mx-2" vertical></v-divider>发表时间：
                {{ item.time }}
              </p>

              <v-list-item-title v-text="item.content"></v-list-item-title>
              <v-divider></v-divider>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
      <div class="text-center mt-4 mb-12">
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
    </v-card>

    <v-dialog v-model="dialog" max-width="600px">
      <v-card class="px-6 py-4">
        <v-card-title class="headline" v-show="openIndex!=1">你确定要使用此道具吗？</v-card-title>
      
            <v-text-field
            v-show="openIndex==1"
              v-model="mycolor"
              label="请输入颜色"
            ></v-text-field>
      

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="close">取消</v-btn>
          <v-btn color="blue darken-1" text @click="useCard">确定</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>
<script>
import discussApi from "@/api/discussApi";
import commentApi from "@/api/commentApi";
import collectionApi from "@/api/collectionApi";
import agreesApi from "@/api/agreesApi";
export default {
  data: () => ({
    dialog: false,
    mycolor: "#212121",
    myPorp: [
      { title: "置顶卡", method: "0" },
      { title: "色彩卡", method: "1" },
      { title: "匿名卡", method: "2" },
      { title: "加密卡", method: "3" },
    ],
    commentRules: [
      (v) => !!v || "评论不能为空",
      (v) => (v && v.length <= 200) || "评论必须小于200个字符",
    ],
    openIndex: -1,
    pageIndex: 1,
    pageSize: 10,
    total: 100,
    discussData: {},
    commentList: [],
    reveal: false,
    selectedItem: "",
    form: {
      content: "",
    },
  }),
  created() {
    this.initializa();
  },
  watch: {
    pageIndex: {
      handler() {
        this.initializa();
      },
    },
  },
  computed: {
    lenData() {
      return Math.floor(this.total / this.pageSize) + 1;
    },
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
    Collection(Id) {
      collectionApi.addCollection(Id).then((resp) => {
        if (resp.status) {
          this.$dialog.message.success(resp.msg, {
            position: "top-right",
          });
        } else {
          this.$dialog.message.error(resp.msg, {
            position: "top-right",
          });
        }
      });
    },
    initializa() {
      discussApi
        .getDiscussByDiscussId(this.$store.state.home.discussId)
        .then((resp) => {
          this.discussData = resp.data;
        });
      commentApi
        .getCommentsByDiscussId(
          this.$store.state.home.discussId,
          this.pageIndex
        )
        .then((resp) => {
          this.commentList = resp.data.pageData;
          this.pageSize = resp.data.pageSize;
          this.total = resp.data.total;
        });
    },
    agrees() {
      agreesApi.getAgrees(this.$store.state.home.discussId).then((resp) => {
        if (resp.data > this.discussData.agree_num) {
          this.$dialog.notify.success(resp.msg, {
            position: "top-right",
          });
        } else {
          this.$dialog.notify.error(resp.msg, {
            position: "top-right",
          });
        }
        this.discussData.agree_num = resp.data;
      });
    },

    sentComment() {
      if (this.$refs.com.validate()) {
        commentApi
          .addComment(this.form, this.$store.state.home.discussId)
          .then((resp) => {
            //设置等级
            this.$store.dispatch("setLevel", resp.data.level);

            this.form.content = "";
            this.initializa();
          });
      } else {
        this.$dialog.notify.error("请合理输入数据", {
          position: "top-right",
          timeout: 5000,
        });
      }
    },
    openCard(index) {
      this.openIndex = index;
      this.dialog = true;
    },
    close() {
      this.openIndex = -1;
      this.dialog = false;
      this.mycolor="#212121";
    },
    useCard() {
      
      if (this.openIndex == -1) {
        return 0;
      }
      discussApi
        .UpdatePorp(this.discussData.id, this.openIndex, this.mycolor)
        .then((resp) => {
          if (resp.status) {
            this.$dialog.notify.success("使用道具成功", {
              position: "top-right",
              timeout: 5000,
            });
          }

          this.close();
        });
    },
  },
};
</script>
<style scoped>
.v-card--reveal {
  bottom: 0;
  opacity: 1 !important;
  position: absolute;
  width: 100%;
}
</style>