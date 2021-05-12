<template>
  <div>
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
      <v-col cols="12">
        <v-text-field label="简介" v-model="form.introduction"></v-text-field>
      </v-col>
    </v-row>

    <div ref="editorDiv" style="text-align: left; z-index: -1"></div>

    <div class="text-center mt-5">
      <v-btn class="mr-6" color="blue lighten-2" dark large @click="claer()"
        >清空</v-btn
      >
      <v-btn color="success" large @click="send()">发布</v-btn>
    </div>
  </div>
</template>
<script>
import E from "wangeditor";
import discussApi from "@/api/discussApi";
export default {
  data() {
    return {
      baseurl: "",
      form: {
        title: "",
        type: "",
        introduction: "",
        content: "",
      },
      items: ["闲聊", "原创", "转载", "正文"],
      myeditor: {},
      mytext: "",
    };
  },
  mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
    this.initializa();
  },
  methods: {
    send() {
      this.form.content = this.myeditor.txt.html();
      discussApi
        .addDiscuss(this.form, this.$store.state.home.plateId)
        .then((resp) => {
          this.$store.dispatch("set_discussId", resp.data.id);
          //设置等级
          this.$store.dispatch("setLevel", resp.data.level);
          this.$router.push({ path: "/comment" });
        });
    },
    initializa() {
      //初始化创建
      var my = this;
      const editor = new E(this.$refs.editorDiv);
      this.myeditor = editor;
      editor.config.height = 400; //设置高度
      editor.config.placeholder = "输入你的内容"; //修改提示文字
      editor.config.zIndex = 1;
      editor.config.uploadImgServer = this.baseurl + "/File/OnPostUploadImage"; //配置图片接口
      editor.config.uploadVideoServer =
        this.baseurl + "/File/OnPostUploadVideo"; //配置视频接口
      editor.config.uploadImgHooks = {
        customInsert: function (insertImgFn, result) {
          // result 即服务端返回的接口
          // insertImgFn 可把图片插入到编辑器，传入图片 src ，执行函数即可
          result.data.forEach((item) => {
            insertImgFn(item.url.replace("#", my.baseurl));
          });
        },
      };
      editor.config.uploadVideoHooks = {
        customInsert: function (insertVideoFn, result) {
          // result 即服务端返回的接口
          insertVideoFn(result.data.url.replace("#", my.baseurl));

          // insertVideoFn 可把视频插入到编辑器，传入视频 src ，执行函数即可
        },
      };
      editor.create();
    },
  },
};
</script>

