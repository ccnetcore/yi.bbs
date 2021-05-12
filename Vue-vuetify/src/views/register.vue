<template>
  <v-app >
    <v-container >
      <v-row style="margin-top: 200px"  >

        <v-col cols="12" sm="6" md="4" offset-sm="3" offset-md="4">
<h1 class="text-center">加入Jift-CC</h1>
          <v-form ref="form" v-model="valid" lazy-validation>
            <v-text-field
              v-model="user_name"
              :counter="20"
              :rules="user_nameRules"
              label="用户名"
              required
            ></v-text-field>

            <v-text-field
              v-model="password"
              :rules="passwordRules"
              label="密码"
              required
              :type="'password'"
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
              <v-col cols="6" class="text-right pt-8"><router-link to='login'>返回</router-link></v-col>
            </v-row>
            <v-btn
              :disabled="!valid"
           dark
              class="mr-4 deep-purple"
              @click="myRegister"
              style="width: 100%"
            >
              注册
            </v-btn>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>
<script>
import accountApi from "@/api/accountApi"
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
      (v) => (v && v.length <= 120) || "密码必须小于20个字符",
    ],
    select: null,
    checkbox: true,
  }),

  methods: {
    myRegister() {
      this.$refs.form.validate(
        accountApi.register(this.user_name, this.password).then((resp) => {
          if (resp.flag) {
              alert(resp.message)
            this.$router.push("/login");
          }
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