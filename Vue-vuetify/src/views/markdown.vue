<template>
  <v-container fluid>
      <mavon-editor
        v-model="content"
        ref="md"
        @imgAdd="$imgAdd"
        @change="change"
        style="min-height: 800px"
      />
  </v-container>
</template>
<script>
// 该组件中注释掉的代码为局部注册的方式。
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
import axios from "axios";
export default {
  data: function() {
    return {
      content: "",
      html: "",
      configs: {}
    };
  },
  components: {
    mavonEditor
  },
  methods: {
    // 将图片上传到服务器，返回地址替换到md中
    $imgAdd(pos, $file) {
      var formdata = new FormData();
      formdata.append("file", $file);
      //将下面上传接口替换为你自己的服务器接口
      axios({
        url: "/common/upload",
        method: "post",
        data: formdata,
        headers: { "Content-Type": "multipart/form-data" }
      }).then(url => {
        this.$refs.md.$img2Url(pos, url);
      });
    },
    change(value, render) {
      // render 为 markdown 解析后的结果
      this.html = render;
      this.$emit('giveData',render)
    }
  }
};
</script>