<template>
  <v-form v-model="valid">
    <v-container>
      <v-card class="pa-4">
        <h2>设置</h2>
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="form.title"
              :rules="myRules"
              :counter="10"
              label="站点标题"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-divider class="my-6"></v-divider>
        <h3>分页设置</h3>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="form.discussPage"
              :counter="10"
              label="主题每页数"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="form.commentPage"
              label="评论每页数"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-divider class="my-6"></v-divider>
        <h3>经验设置</h3>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="form.discussExperience"
              :counter="10"
              label="发布主题经验"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="form.commentExperience"
              label="发布评论经验"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-divider class="my-6"></v-divider>
        <v-btn dark color="cyan" @click="save">保存</v-btn>
      </v-card>
    </v-container>
  </v-form>
</template>
<script>
import settingApi from "@/api/settingApi";
export default {
  data: () => ({
    form: {},
    valid: false,
    firstname: "",
    lastname: "",
    myRules: [
      (v) => !!v || "不能为空",
      (v) => v.length <= 10 || "请输入小于10个字符",
    ],
  }),
  created() {
    this.initialize();
  },
  methods: {
    save() {
      settingApi.UpdateSetting(this.form).then((resp) => {
        this.initialize();
        this.$dialog.notify.success(resp.msg, {
          position: "top-right",
          timeout: 5000,
        });
      });
    },
    initialize() {
      settingApi.getSetting().then((resp) => {
        this.form = resp.data;
      });
    },
  },
};
</script>