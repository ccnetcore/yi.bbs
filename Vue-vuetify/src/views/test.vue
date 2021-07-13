<template>
  <v-container fluid>
    <v-row>
      <v-col cols="10">
        <!-- <mavon-editor
          v-model="content"
          ref="md"
          @imgAdd="$imgAdd"
          @change="change"
          style="min-height: 800px"
        /> -->
      </v-col>
      <v-col cols="2">
        <v-btn v-for="(item, index) in selection" :key="index">{{
          item
        }}</v-btn>
        <v-treeview
          activatable
          open-on-click
          color="blue"
          selectable
          selection-type="independent"
          :items="items2"
          v-model="selection"
          single-select="true"
        ></v-treeview>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
// 该组件中注释掉的代码为局部注册的方式。
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
import axios from "axios";
import articleApi from "@/api/articleApi";
import collectionVue from "./user/collection.vue";

export default {
  created() {
    articleApi.getArticlesByDiscussId(49).then((response) => {
      const resp = response.data;
      this.items2 = resp;

      this.articleList = this.arrOfOneDimension(this.items2);
      console.log(this.newArr);
    });
  },
  data: () => {
    return {
      newArr: [],
      articleList: [],
      selection: [],
      items2: [
        {
          id: 1,
          name: "Applications :",
          children: [
            { id: 2, name: "Calendar : app" },
            { id: 3, name: "Chrome : app" },
            { id: 4, name: "Webstorm : app" },
          ],
        },
        {
          id: 5,
          name: "Documents :",
          children: [
            {
              id: 6,
              name: "vuetify :",
              children: [
                {
                  id: 7,
                  name: "src :",
                  children: [
                    { id: 8, name: "index : ts" },
                    { id: 9, name: "bootstrap : ts" },
                  ],
                },
              ],
            },
            {
              id: 10,
              name: "material2 :",
              children: [
                {
                  id: 11,
                  name: "src :",
                  children: [
                    { id: 12, name: "v-btn : ts" },
                    { id: 13, name: "v-card : ts" },
                    { id: 14, name: "v-window : ts" },
                  ],
                },
              ],
            },
          ],
        },
        {
          id: 15,
          name: "Downloads :",
          children: [
            { id: 16, name: "October : pdf" },
            { id: 17, name: "November : pdf" },
            { id: 18, name: "Tutorial : html" },
          ],
        },
        {
          id: 19,
          name: "Videos :",
          children: [
            {
              id: 20,
              name: "Tutorials :",
              children: [
                { id: 21, name: "Basic layouts : mp4" },
                { id: 22, name: "Advanced techniques : mp4" },
                { id: 23, name: "All about app : dir" },
              ],
            },
            { id: 24, name: "Intro : mov" },
            { id: 25, name: "Conference introduction : avi" },
          ],
        },
      ],
      content: "",
      html: "",
      baseurl: "",
      configs: {},
    };
  },
  components: {
    mavonEditor,
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
        url: this.baseurl + "/File/OnPostUploadImage",
        method: "post",
        data: formdata,
        headers: { "Content-Type": "multipart/form-data" },
      }).then((resp) => {
        var myurl = resp.data.data[0].url;

        this.$refs.md.$img2Url(pos, myurl.replace("#", this.baseurl));
      });
    },
    change(value, render) {
      // render 为 markdown 解析后的结果
      this.html = render;
      this.$emit("giveData", render);
    },

    arrOfOneDimension(arr) {
      for (var i = 0; i < arr.length; i++) {
        if (
          arr[i].children == null ||
          arr[i].children.length == 0
        ) {
          var obj = { id: arr[i].id, name: arr[i].name };
          this.newArr.push(obj);
        } else {
          this.arrOfOneDimension(arr[i]);
          // var obj2 = { id: arr[i].id, name: arr[i].name };
          // this.newArr.push(obj2);
        }
      }
    },
  },
};
</script>