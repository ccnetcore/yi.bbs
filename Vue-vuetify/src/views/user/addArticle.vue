<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12">
        <v-text-field label="目录名" v-model="form.name"></v-text-field>
      </v-col>
    </v-row>

    <v-card color="#FAFAFA" class="pb-15">
      <v-row class="text-center">
        <v-col cols="12">
          <p class="text-h6 text--secondary text-left ml-2">
            选择主题内容编辑方式：
          </p>
        </v-col>
        <v-col cols="12" md="6">
          <v-btn
            fab
            @click="
              is_html = true;
              dialog = true;
              selectIndex = 1;
            "
            width="200"
            height="200"
            color="cyan"
            dark
            :disabled="is_mark"
            >markdown</v-btn
          >
        </v-col>
        <v-col cols="12" md="6">
          <v-btn
            fab
            @click="
              is_mark = true;
              dialog = true;
              selectIndex = 2;
            "
            width="200"
            height="200"
            color="blue"
            dark
            :disabled="is_html"
            >html</v-btn
          >
        </v-col>
      </v-row>
    </v-card>
    <v-btn
      class="my-12"
      width="100%"
      color="success"
      :disabled="is_send"
      large
      @click="send()"
      >确认</v-btn
    >

    <v-dialog
      v-model="dialog"
      fullscreen
      hide-overlay
      transition="dialog-bottom-transition"
    >
      <v-card class="text-center align-center">
        <v-toolbar dark color="cyan">
          <v-btn
            icon
            dark
            @click="
              dialog = false;
              is_send = false;
            "
          >
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <v-toolbar-title>主题内容</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn
              dark
              text
              @click="
                dialog = false;
                is_send = false;
              "
            >
              保存
            </v-btn>
          </v-toolbar-items>
        </v-toolbar>

        <htmlEdit
          v-show="selectIndex == 2"
          @giveData="getHtml"
          :myhtml2="myhtml"
        ></htmlEdit>
        <markdownEdit
          v-show="selectIndex == 1"
          @giveData="getHtml"
        ></markdownEdit>
      </v-card>
    </v-dialog>
  </v-container>
</template>
<script>
import articleApi from "@/api/articleApi";
import htmlEdit from "@/views/html";
import markdownEdit from "@/views/markdown";
export default {
  data() {
    return {
      is_mark: false,
      is_html: false,
      is_send: true,
      dialog: false,
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
  mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  created() {
    this.initializa();
  },
  methods: {
    send() {
      this.form.content = this.myhtml;

      if (this.$route.query.articleId == undefined) {
        articleApi.addArticle(this.form,this.$store.state.home.discussId).then((response) => {
          if(response.status)
          {
this.$router.push({ path: "/comment" });
          }
          else
          {
            alert("权限不足！")
          }
          
        });
      } else {
        articleApi.updateArticle(this.form,this.$store.state.home.discussId).then((response) => {
          if (response.status) {
            this.$router.push({ path: "/comment" });
          } else {
            alert("权限不足！");
          }
        });
      }
    },
    initializa() {
      if (this.$route.query.articleId != undefined) {
        articleApi
          .getArticleById(this.$route.query.articleId)
          .then((response) => {
            const resp = response.data;
            this.form.id = resp.id;
            this.form.name = resp.name;
            this.myhtml = resp.content;
          });

        this.is_mark = true;
      }
    },
    getHtml(html) {
      this.myhtml = html;
    },
  },
  components: {
    htmlEdit,
    markdownEdit,
  },
};
</script>

