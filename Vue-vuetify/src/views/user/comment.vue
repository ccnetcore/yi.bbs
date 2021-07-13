<template>
  <v-container fluid>
    <v-row>
      <v-col class="pb-0">
        <v-card height="100%" class="mx-auto">
          <v-navigation-drawer right app v-model="drawer">
            <v-list-item>
              <v-list-item-content>
                <v-btn color="cyan" elevation="2" @click="intoAdd2" large dark>
                  添加目录
                </v-btn>
              </v-list-item-content>
            </v-list-item>

            <v-divider></v-divider>
            <v-list nav>
              <v-list-item link @click="initializa()">
                <v-list-item-content>
                  <v-list-item-title>根目录</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
            <v-list dense nav>
              <v-list-item
                v-for="myitem in articleList"
                :key="myitem.id"
                link
                @click="showArticle(myitem.id)"
              >
                <v-list-item-content>
                  <v-list-item-title>
                    <span>{{ myitem.name }}</span>
                   
                    <v-icon class="ml-2" color="cyan" @click="intoArticle(myitem.id)"
                      >mdi-book-edit-outline</v-icon
                    >
                    <v-icon color="cyan" @click="delArticle(myitem.id)"
                      >mdi-close-circle</v-icon
                    >
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-navigation-drawer>
        </v-card>
      </v-col>

      <v-col cols="12">
        <v-card class="mx-auto my-2" max-width="100%">
          <v-card-text>
            <p class="display-1 text--primary">
              <v-icon class="mx-2" @click="drawer = !drawer">mdi-menu</v-icon>
              {{ discussData.title }}
            </p>

            <v-divider class="my-2"></v-divider>
            <v-avatar size="60px" @click="intoInfo(discussData.user.id)">
              <img
                v-if="discussData.user != undefined"
                alt="Avatar"
                :src="
                  baseurl +
                  '/File/ShowNoticeImg?filePath=' +
                  discussData.user.icon
                "
              />
            </v-avatar>

            <p v-if="discussData.user != undefined">
              作者：{{ discussData.user.username }}
            </p>

            <p>时间：{{ discussData.time }}</p>
            <p>文章ID：{{ discussData.id }}</p>
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
              <v-icon dark @click="Collection(discussData.id)">
                mdi-star
              </v-icon>
            </v-btn>

            <v-btn class="mx-2" fab dark small color="cyan">
              <v-icon dark @click="updataDiscuss(discussData.id)">
                mdi-book-edit
              </v-icon>
            </v-btn>

            <v-btn class="mx-2" fab dark small color="cyan">
              <v-icon dark @click="intoRecords">mdi-update</v-icon>
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
              @keyup.enter="sentComment()"
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
                        baseurl +
                        '/File/ShowNoticeImg?filePath=' +
                        item.user.icon
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
            <v-card-title class="headline" v-show="openIndex != 2"
              >你确定要使用此道具吗？</v-card-title
            >

            <v-text-field
              v-show="openIndex == 2"
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

        <v-dialog v-model="dialog2" width="800">
          <v-card>
            <v-card-title class="text-h5 grey lighten-2">
              协同编辑记录
            </v-card-title>

            <v-card-text style="overflow-y: scroll; max-height: 600px">
              <v-timeline>
                <v-timeline-item
                  v-for="(item, index) in recordList"
                  :key="index"
                  color="cyan lighten-1"
                  fill-dot
                  small
                >
                  <v-card>
                    <v-card-title class="cyan lighten-1 justify-end">
                      <v-subheader class="white--text">{{
                        item.time
                      }}</v-subheader>

                      <v-avatar size="42" @click="intoInfo(item.user.id)">
                        <img
                          alt="Avatar"
                          :src="
                            baseurl +
                            '/File/ShowNoticeImg?filePath=' +
                            item.user.icon
                          "
                        />
                      </v-avatar>

                    <span class="white--text ml-2">  {{item.user.username}}</span>
                    </v-card-title>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          {{ item.describe }}
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card>
                </v-timeline-item>
              </v-timeline>
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="cyan" text @click="dialog2 = false"> 关闭 </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import discussApi from "@/api/discussApi";
import commentApi from "@/api/commentApi";
import collectionApi from "@/api/collectionApi";
import agreesApi from "@/api/agreesApi";
import articleApi from "@/api/articleApi";
import recordApi from "@/api/recordApi";
export default {
  data: () => ({
    recordList: [],
    dialog2: false,
    articleList: [],
    drawer: true,
    dialog: false,
    mycolor: "#212121",
    myPorp: [
      { title: "置顶卡", method: "1" },
      { title: "色彩卡", method: "2" },
      { title: "匿名卡", method: "3" },
      { title: "加密卡", method: "4" },
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
    intoRecords() {
      recordApi
        .getRecordsByDiscussId(this.$store.state.home.discussId)
        .then((resp) => {
          this.recordList = resp.data;
        });
      this.dialog2 = true;
    },
    delArticle(acticleId) {
      this.$dialog
        .confirm({
          text: "你确定要删除该项吗？",
          title: "Warning",
        })
        .then((resp) => {
          if (resp) {
            articleApi
              .delArticleList([acticleId], this.$store.state.home.discussId)
              .then((resp) => {
                if (resp.status) {
                  this.initializa();
                } else {
                  alert("权限不足！");
                }
              });
          }
        });
    },
    updataDiscuss(discussId) {
      this.$router.push({
        path: `/AddDiscuss`,
        query: {
          discussId: discussId,
        },
      });
    },
    intoArticle(articleId) {
      this.$router.push({
        path: `/addArticle`,
        query: {
          articleId: articleId,
        },
      });
    },
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
    intoAdd() {
      this.$router.push({ path: "/addDisucss" });
    },
    intoAdd2() {
      this.$router.push({ path: "/addArticle" });
    },
    showArticle(id) {
      articleApi.getArticleById(id).then((resp) => {
        this.discussData.content = resp.data.content;
      });
    },

    initializa() {
      articleApi
        .getArticlesByDiscussId(this.$store.state.home.discussId)
        .then((resp) => {
          this.articleList = resp.data;
        });

      discussApi
        .getDiscussByDiscussId(this.$store.state.home.discussId)
        .then((resp) => {
          this.discussData = resp.data;

          this.discussData.content = this.discussData.content
            .replace(/&lt;/g, "<")
            .replace(/&gt;/g, ">");
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
      this.mycolor = "#212121";
    },
    useCard() {
      if (this.openIndex == -1) {
        return 0;
      }
      discussApi
        .UpdatePorp(this.discussData.id, this.openIndex, this.mycolor)
        .then((resp) => {
          if (resp.status) {
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
