<template>
  <v-app>
    <v-container fluid class="grey lighten-5" v-bind:style="{ height: allDiv }">
      <v-row v-bind:style="{ height: allDiv }">
        <!-- -------------------------------公告----------------------------------- -->
        <v-col
          cols="0"
          sm="4"
          md="3"
          class="white d-none d-sm-flex text-center"
        >
          <div style="width:100%">
            <h1 class="light-blue--text mt-8">服装交流平台</h1>
                         <p class="my-4 title">clothing communication platform</p>
            <p class="my-4 title">欢迎！在这里，我们无所不谈！</p>

            <v-img
              src="../../public/image/login.svg"
              contain
              v-bind:style="{ marginTop:imgDiv }"
              style="width:100%;"
            ></v-img>
          </div>
        </v-col>
        <!-- -------------------------------------------------------------------------- -->

        <!-- ------------------------------表单-------------------------------- -->
        <v-col cols="12" sm="8" md="9">
          <v-container class="text-center align-center">
            <v-card
              class="px-6 py-4 mx-auto elevation-4 rounded-md"
              style="margin-top: 120px; height: 600px; width: 500px"
            >
              <div>
                <h1  class="title my-2">Jift-CC</h1>
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
                    ><router-link to="register"
                      >没有账号？前往注册</router-link
                    ></v-col
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
              <p>或使用登录</p>
              <v-btn
                dark
                class="my-2 cyan"
                @click="login"
                large
                style="width: 100%"
              >
                <v-icon class="mx-2"> mdi-message-text </v-icon>
                QQ
              </v-btn>

              <v-btn dark class="cyan" @click="login" large style="width: 100%">
                <v-icon class="mx-2"> mdi-message-text </v-icon>
                微信
              </v-btn>
            </v-card>
          </v-container>
        </v-col>
        <!-- --------------------------------------------------------------- -->
      </v-row>
    </v-container>
  </v-app>
</template>
<script>
export default {
  data: () => ({
    allDiv: document.documentElement.clientHeight + "px",
    imgDiv:document.documentElement.clientHeight-490 + "px",
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
      (v) => (v && v.length <= 120) || "密码必须小于20个字符",
    ],
    select: null,
    checkbox: true,
  }),
  mounted() {
    // 注：window.onresize只能在项目内触发1次
    window.onresize = function windowResize() {
      // 通过捕获系统的onresize事件触发我们需要执行的事件
      this.allDiv = document.documentElement.clientHeight + "px";
    };
  },
  methods: {
    login() {
      this.$refs.form.validate(
        this.$store
          .dispatch("Login", {
            username: this.user_name,
            password: this.password,
          })
          .then((resp) => {
            this.$router.push("/");
          })
      );
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
