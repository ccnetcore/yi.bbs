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
      <v-col cols="12" >
              <markdownEdit
          @giveData="getHtml"
          :myhtml2="myhtml"
        ></markdownEdit>
      </v-col>

  
    </v-row>
    <v-btn
      class="my-12"
      width="100%"
      color="success"
      large
      @click="send()"
      >确认</v-btn
    >
  </v-container>
</template>
<script>
import labelApi from "@/api/labelApi";
import discussApi from "@/api/discussApi";
import markdownEdit from "@/views/markdown";
import mytypeApi from "@/api/mytypeApi";
export default {
  data() {
    return {
      selectLabel: [], //这里存放的是名称列表
      lableList: [], //这里存放的是id和名字
      itemLabel: [],
      baseurl: "",
      form: {
        id: 0,
        title: "",
        type: "",
        introduction: "",
        content: "",
      },
      items: [],
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
      if (this.$route.query.discussId == undefined) {
        discussApi
          .addDiscuss(this.form, this.$store.state.home.plateId, Ids)
          .then((resp) => {
            this.$store.dispatch("set_discussId", resp.data.id);
            //设置等级
            this.$store.dispatch("setLevel", resp.data.level);
            this.$router.push({ path: "/comment" });
          });
      } else {
        discussApi.updateDiscuss(this.form).then((response) => {
          if (response.status) {
            this.$router.push({ path: "/comment" });
          } else {
            alert("权限不足！");
          }
        });
      }
    },
        getHtml(html) {
      this.myhtml = html;
    },
    initializa() {
      mytypeApi.getMytypes().then((resp) => {
        window.scrollTo(0,0);
        const response = resp.data;
        response.forEach((item) => {
          this.items.push(item.name);
        });
      });

      if (this.$route.query.discussId != undefined) {
        discussApi
          .getDiscussByDiscussId(this.$route.query.discussId)
          .then((response) => {
            const res = response.data;
            this.form.id = res.id;

            this.form.title = res.title;
            this.form.type = res.type;
            this.form.introduction = res.introduction;
            this.myhtml = res.content;
          });

 
      }

      labelApi.getLabelByUserId().then((resp) => {
        this.lableList = resp.data;
        this.itemLabel = resp.data.map((obj) => {
          return obj.name;
        });
      });
    },
  },
  components: {
    markdownEdit,
  },
};
</script>

