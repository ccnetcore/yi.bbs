<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" sm="1">
        <v-select
          :items="items"
          filled
          label="类别"
          required
          v-model="form.type"
          dense
        ></v-select>
      </v-col>
      <v-col cols="12" sm="11">
        <v-text-field label="标题" v-model="form.title"></v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="4">
        <v-combobox
          v-model="selectLabel"
          :items="itemLabel"
          label="点击选择标签"
          multiple
          dense
          chips
        ></v-combobox>
      </v-col>
      <v-col cols="12" sm="6" md="8">
        <v-text-field label="简介" v-model="form.introduction"></v-text-field>
      </v-col>
    </v-row>

    <!-- <div ref="editorDiv" style="text-align: left; z-index: -1"></div> -->

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
            @click="is_html=true;dialog = true;selectIndex=1;"
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
            @click="is_mark=true;dialog = true;selectIndex=2;"
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
    <v-btn class="my-12" width="100%" color="success" :disabled="is_send" large @click="send()"
      >确认发布</v-btn
    >

    <v-dialog
      v-model="dialog"
      fullscreen
      hide-overlay
      transition="dialog-bottom-transition"
    >
      <v-card class="text-center align-center">
        <v-toolbar dark color="cyan">
          <v-btn icon dark @click="dialog = false;is_send=false;">
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <v-toolbar-title>主题内容</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark text @click="dialog = false;is_send=false;"> 保存 </v-btn>
          </v-toolbar-items>
        </v-toolbar>

        <htmlEdit v-show="selectIndex==2" @giveData="getHtml"></htmlEdit>
        <markdownEdit v-show="selectIndex==1" @giveData="getHtml"></markdownEdit>
      </v-card>
    </v-dialog>
  </v-container>
</template>
<script>
import labelApi from "@/api/labelApi";
import discussApi from "@/api/discussApi";
import htmlEdit from "@/views/html";
import markdownEdit from "@/views/markdown";
export default {
  data() {
    return {
      is_mark:false,
      is_html:false,
      is_send:true,
      dialog: false,
      selectLabel: [], //这里存放的是名称列表
      lableList: [], //这里存放的是id和名字
      itemLabel: [],
      baseurl: "",
      form: {
        title: "",
        type: "",
        introduction: "",
        content: "",
      },
      items: ["闲聊", "原创", "转载", "正文"],
      myhtml: "",
      selectIndex:0
    };
  },
  mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
    this.initializa1();
  },
  created() {
    this.initializa2();
  },
  methods: {
    send() {
      var Ids = [];
      for (var i = 0; i < this.selectLabel.length; i++) {
        for (var j = 0; j < this.lableList.length; j++) {
          if (this.selectLabel[i] == this.lableList[j].name) {
            Ids.push(this.lableList[j].id);
          }
        }
      }
      if (this.form.type == "") {
        this.form.type = "闲聊";
      }
      this.form.content = this.myhtml;
      discussApi
        .addDiscuss(this.form, this.$store.state.home.plateId, Ids)
        .then((resp) => {
          this.$store.dispatch("set_discussId", resp.data.id);
          //设置等级
          this.$store.dispatch("setLevel", resp.data.level);
          this.$router.push({ path: "/comment" });
        });
    },
    initializa2() {
      labelApi.getLabelByUserId().then((resp) => {
        this.lableList = resp.data;
        this.itemLabel = resp.data.map((obj) => {
          return obj.name;
        });
      });
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

