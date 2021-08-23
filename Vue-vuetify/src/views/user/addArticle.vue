<template>
  <v-container fluid>
    <v-row>
      <v-card>
        <v-navigation-drawer
          right
          app
          v-model="drawer"
          :style="{ width: articleWidth + 'px' }"
        >
          <!-- <v-list-item>
            <v-list-item-content>
              <v-btn
                color="blue"
                elevation="2"
                @click="drawer = !drawer"
                large
                dark
              >
                关闭目录
              </v-btn>
            </v-list-item-content>
          </v-list-item> -->

          <v-list-item>
            <v-list-item-content>
              <v-btn color="blue" elevation="2" @click="updateWidth" large dark>
                扩展目录
              </v-btn>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-btn color="cyan" elevation="2" @click="getCache" large dark>
                拉取备份
              </v-btn>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-content>
              <v-btn color="cyan" elevation="2" @click="intoAdd2" large dark>
                添加目录
              </v-btn>
            </v-list-item-content>
          </v-list-item>

          <v-divider></v-divider>
          <v-list nav>
            <v-list-item link>
              <v-list-item-content>
                <v-list-item-title
                  >根目录

                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-icon
                        v-bind="attrs"
                        color="cyan"
                        v-on="on"
                        @click="updataDiscuss()"
                      >
                        mdi-book-edit
                      </v-icon>
                    </template>
                    <span>编辑根目录</span>
                  </v-tooltip>
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list>
          <v-list dense nav>
            <v-list-item>
              <v-treeview
                :items="articleList"
                style="width: 100%"
                open-on-click
                activatable
                return-object
                :active.sync="selectArt"
              >
                <template v-slot:append="{ item }">
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-icon
                        v-bind="attrs"
                        v-on="on"
                        color="cyan"
                        @click="intoAddArticle(item.id)"
                        >mdi-plus-box-multiple</v-icon
                      >
                    </template>
                    <span>添加下一级子目录</span>
                  </v-tooltip>

                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-icon
                        v-bind="attrs"
                        v-on="on"
                        color="cyan"
                        @click="intoArticle(item.id)"
                        >mdi-book-edit-outline</v-icon
                      >
                    </template>
                    <span>编辑</span>
                  </v-tooltip>

                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-icon
                        v-bind="attrs"
                        v-on="on"
                        color="cyan"
                        @click="delArticle(item.id)"
                        >mdi-close-circle</v-icon
                      >
                    </template>
                    <span>删除</span>
                  </v-tooltip>
                </template>
              </v-treeview>
            </v-list-item>
          </v-list>
        </v-navigation-drawer>
      </v-card>

      <v-col cols="12">
        <v-text-field label="目录名" v-model="form.name"></v-text-field>
      </v-col>

      <v-col cols="12">
        <markdownEdit @giveData="getHtml" :myhtml2="myhtml"></markdownEdit>
      </v-col>
      <v-col cols="12">
        <v-btn class="my-12" width="100%" large color="success" @click="send()"
          >确认</v-btn
        >
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import articleApi from "@/api/articleApi";
import markdownEdit from "@/views/markdown";
export default {
  data() {
    return {
      mytimer: null,
      selectArt: [],
      articleWidth: "260",
      drawer: true,
      articleList: [],
      baseurl: "",
      form: {
        id: 0,
        name: "",
        content: "",
      },
      myhtml: "",
      selectIndex: 0,
    };
  },
  // watch: {
  //   selectArt: {
  //     handler(new1, old2) {
  //       // console.log(new1)
  //       // this.intoArticle(new1[0].id);
  //     },
  //   },
  // },
  mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  created() {
    this.initializa();
  },
  methods: {
    getCache() {
      articleApi.getArticleByCache(this.$route.query.articleId).then((resp) => {
        if (resp.status) {
          this.myhtml = resp.data;
          this.$dialog.notify.success("已在云端发现备份", {
            position: "top-right",
            timeout: 5000,
          });
        } else {
          this.$dialog.notify.error("未在云端发现备份", {
            position: "top-right",
            timeout: 5000,
          });
        }
      });
    },

    setCache() {
      articleApi
        .setArticleByCache(this.$route.query.articleId, this.myhtml)
        .then((resp) => {
          this.$dialog.notify.success("已成功自动备份云端", {
            position: "top-right",
            timeout: 2000,
          });
        });
    },

    intoAdd2() {
      this.$router.push({ path: "/addArticle" });
      this.form.name = "";
      this.form.content = "";
      this.myhtml = "";
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
    updataDiscuss() {
      this.$router.push({
        path: `/AddDiscuss`,
        query: {
          discussId: this.$store.state.home.discussId,
        },
      });
      this.initializa();
    },
    updateWidth() {
      if (this.articleWidth == "260") {
        this.articleWidth = "500";
      } else {
        this.articleWidth = "260";
      }
    },
    intoAddArticle(parentId) {
      this.$router.push({
        path: `/addArticle`,
        query: {
          parentId: parentId,
        },
      });
      this.form.name = "";
      this.form.content = "";
      this.myhtml = "";
    },
    send() {
      this.form.content = this.myhtml;

      if (
        this.$route.query.articleId == undefined &&
        this.$route.query.parentId == undefined
      ) {
        //没有传任何值，代表是添加
        articleApi
          .addArticle(this.form, this.$store.state.home.discussId)
          .then((response) => {
            if (response.status) {
              this.$dialog.notify.success("目录添加成功", {
                position: "top-right",
                timeout: 5000,
              });
              this.initializa();
            } else {
              alert("权限不足！");
            }
          });
      } else if (
        this.$route.query.articleId != undefined &&
        this.$route.query.parentId == undefined
      ) {
        //只传了文章id，代表更新
        articleApi
          .updateArticle(this.form, this.$store.state.home.discussId)
          .then((response) => {
            if (response.status) {
              this.$dialog.notify.success("文章更新成功", {
                position: "top-right",
                timeout: 5000,
              });
              this.initializa();
            } else {
              alert("权限不足！");
            }
          });
      } else if (
        this.$route.query.articleId == undefined &&
        this.$route.query.parentId != undefined
      ) {
        //只传了父级id，代表添加子目录

        articleApi
          .addChildrenArticle(
            this.form,
            this.$route.query.parentId,
            this.$store.state.home.discussId
          )
          .then((response) => {
            if (response.status) {
              this.$dialog.notify.success("添加子目录成功", {
                position: "top-right",
                timeout: 5000,
              });
              this.initializa();
            } else {
              alert("权限不足！");
            }
          });
      }
    },

    initializa() {
      window.clearInterval(this.mytimer);
      setTimeout(function () {
        window.scrollTo(0, 0);
      }, 500);

      articleApi
        .getArticlesByDiscussId(this.$store.state.home.discussId)
        .then((resp) => {
          this.articleList = resp.data;
        });

      if (this.$route.query.articleId != undefined) {
        //下面是进行更新(没有传文章id或者父级id)
        //每30秒轮询保存
        this.mytimer = window.setInterval(() => {
          setTimeout(this.setCache, 0);
        }, 1000 * 60 * 1);
        articleApi
          .getArticleById(this.$route.query.articleId)
          .then((response) => {
            const resp = response.data;
            this.form.id = resp.id;
            this.form.name = resp.name;
            this.myhtml = resp.content;
          });
      }
    },
    intoArticle(articleId) {
      this.$router.push({
        path: `/addArticle`,
        query: {
          articleId: articleId,
        },
      });
      articleApi.getArticleById(articleId).then((response) => {
        const resp = response.data;
        this.form.id = resp.id;
        this.form.name = resp.name;
        this.myhtml = resp.content;
      });
    },
    getHtml(html) {
      this.myhtml = html;
    },
  },
  destroyed() {
    window.clearInterval(this.mytimer);
  },
  components: {
    markdownEdit,
  },
};
</script>

