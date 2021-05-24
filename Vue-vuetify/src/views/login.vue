<template>
  <v-card
    class="px-6 py-4 mx-auto elevation-4 rounded-md"
    style="margin-top: 100px; height: 600px; width: 500px"
  >
    <div>
      <h1 class="title my-2">Jift-CC</h1>
      <v-subheader>登入你的用户</v-subheader>
    </div>

    <v-form ref="form" v-model="valid" lazy-validation>
      <v-text-field
        v-model="user_name"
        :rules="user_nameRules"
        label="用户名"
        outlined
        clearable
        required
        :counter="20"
      ></v-text-field>

      <v-text-field
        v-model="password"
        :rules="passwordRules"
        label="密码"
        outlined
        clearable
        required
        type="password"
      ></v-text-field>
      <v-row>
        <v-col cols="6">
          <v-checkbox
            v-model="checkbox"
            :rules="[(v) => !!v || '同意后才可进入']"
            label="你同意协议吗？"
            required
          ></v-checkbox
        ></v-col>
        <v-col cols="6" class="text-right pt-8"
          ><router-link to="register">前往注册</router-link></v-col
        >
      </v-row>
    </v-form>

    <v-btn
      dark
      class="my-2 light-blue"
      @click="login"
      large
      style="width: 100%"
    >
      登入
    </v-btn>
    <p class="my-2">或使用登录</p>
    <v-btn dark class="my-2 cyan" @click="qqlogin" large style="width: 100%">
      <v-icon class="mx-2"> mdi-qqchat </v-icon>
      QQ
    </v-btn>

    <v-btn dark class="cyan" @click="login" large style="width: 100%">
      <v-icon class="mx-2"> mdi-message-text </v-icon>
      微信
    </v-btn>
  </v-card>
</template>
<script>
import myqq from "../utils/myqq";
export default {
  data: () => ({
    valid: true,
    user_name: "",
    user_nameRules: [
      (v) => !!v || "用户名不能为空",
      (v) => (v && v.length <= 20) || "用户名必须小于20个字符",
    ],
    password: "",
    passwordRules: [
      (v) => !!v || "密码不能为空",
      (v) => (v && v.length <= 120) || "密码必须小于20个字符",
    ],
    select: null,
    checkbox: true,
  }),
  methods: {
    qqlogin() {
      QC.Login.showPopup(myqq.myqqLogin);
      // window.close();
    },
    login() {
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch("Login", {
            username: this.user_name,
            password: this.password,
          })
          .then((resp) => {
            if (resp.status) {
              this.$router.push("/");
            } else {
              this.$dialog.notify.error(resp.msg, {
                position: "top-right",
                timeout: 5000,
              });
            }
          });
      } else {
        this.$dialog.notify.error("请合理输入数据", {
          position: "top-right",
          timeout: 5000,
        });
      }
    },
    reset() {
      this.$refs.form.reset();
    },
    resetValidation() {
      this.$refs.form.resetValidation();
    },
  },
};
</script>
