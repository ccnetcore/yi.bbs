<template>
  <v-container fluid>
   
    <div ref="editorDiv" style="text-align: left; z-index: -1">
    </div>
  </v-container>
</template>
<script>
import E from "wangeditor";
export default {
  props: ["myhtml2"],
  data() {
    return {
      myhtml: this.myhtml2,
    };
  },
  mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
    this.initializa();
  },
  methods: {
    initializa() {
      //初始化创建
      var my = this;
      const editor = new E(this.$refs.editorDiv);
      this.myeditor = editor;
      editor.config.height = 750; //设置高度
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

      editor.config.onchange = function (html) {
        // 第二步，监控变化，同步更新到 textarea
        my.myhtml = html;
        my.$emit("giveData", html);
      };

      
      editor.create();
   editor.txt.html(this.myhtml);
    },
  },
};
</script>
