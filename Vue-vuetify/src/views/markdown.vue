<template>

      <mavon-editor
 previewBackground="#ffffff"
        v-model="myhtml2"
        ref="md"
        @imgAdd="$imgAdd"
        @change="change"
         :ishljs="true"
    
      codeStyle="atom-one-dark"
        style="height: 800px;z-index:1"
      />

</template>
<script>
// 该组件中注释掉的代码为局部注册的方式。
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
import axios from "axios";
export default {
  props: ["myhtml2"],
  data: function() {
    return {
      content: this.myhtml2,
      html: "",
      baseurl:"",
      configs: {}
    };
  },
  components: {
    mavonEditor
  },
    mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  methods: {
    // 将图片上传到服务器，返回地址替换到md中
    $imgAdd(pos, $file) {
      var formdata = new FormData();
      formdata.append("file", $file);
      //将下面上传接口替换为你自己的服务器接口
      axios({
        url: this.baseurl+"/File/OnPostUploadImage",
        method: "post",
        data: formdata,
        headers: { "Content-Type": "multipart/form-data" }
      }).then(resp => {
        var myurl=resp.data.data[0].url;


        this.$refs.md.$img2Url(pos, myurl.replace("#", this.baseurl));
      });
    },
    change(value, render) {
      // render 为 markdown 解析后的结果
      this.myhtml2 =value;
      this.$emit('giveData',value)
    }
  }
};
</script>