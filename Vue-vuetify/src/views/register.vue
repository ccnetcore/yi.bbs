<template>
  <v-card
    class="px-6 py-4 mx-auto elevation-4 rounded-md"
    style="margin-top: 120px; height: 600px; width: 500px"
  >
    <div>
      <h1 class="title my-2">Jift-CC</h1>
      <v-subheader>注册加入我们</v-subheader>
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
        v-model="user_name"
        :rules="user_nameRules"
        label="邮箱"
        outlined
        clearable
        required
        :counter="20"
      ></v-text-field>

      <v-text-field
        v-model="user_name"
        :rules="user_nameRules"
        label="验证码"
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
          ><router-link to="login">返回</router-link></v-col
        >
      </v-row>
    </v-form>

    <v-btn
      dark
      class="my-2 light-blue"
      @click="register"
      large
      style="width: 100%"
    >
      注册
    </v-btn>
    <!-- <p>或使用登录</p>
    <v-btn dark class="my-2 cyan" @click="login" large style="width: 100%">
      <v-icon class="mx-2"> mdi-message-text </v-icon>
      QQ
    </v-btn>

    <v-btn dark class="cyan" @click="login" large style="width: 100%">
      <v-icon class="mx-2"> mdi-message-text </v-icon>
      微信
    </v-btn> -->
  </v-card>
</template>
<script>
export default {
  data: () => ({
    valid: true,
    user_name: "",
    user_nameRules: [
      (v) => !!v || "用户名不能为空",
      (v) => (v && v.length <= 20) || "用户名必须小于20个字符",
    ],
    //   email: '',
    //   emailRules: [
    //     v => !!v || 'E-mail is required',
    //     v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
    //   ],
    password: "",
    passwordRules: [
      (v) => !!v || "密码不能为空",
      (v) => (v && v.length <= 120) || "密码必须小于120个字符",
           (v) => (v && v.length >= 7) || "密码必须大于等于7个字符",
    ],
    select: null,
    checkbox: true,
  }),
  methods: {
    register() {
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch("Register", {
            username: this.user_name,
            password: this.password,
          })
          .then((resp) => {
            if (resp.status) {
              this.$dialog.notify.success(resp.msg, {
                position: "top-right",
                timeout: 5000,
              });
              this.$router.push("/login");
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
