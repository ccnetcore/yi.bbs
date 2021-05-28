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
            <h1 class="light-blue--text mt-8">江西服装学院
              <br>论坛BBS</h1>
                         <p class="my-4 title">Jift BBS</p>
            <p class="my-4 title">欢迎！在这里，我们无所不谈！</p>

            <v-img
              src="../assets/login.svg"
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
                <router-view></router-view>
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
    //   email: '',
    //   emailRules: [
    //     v => !!v || 'E-mail is required',
    //     v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
    //   ],
  }),
  mounted() {
    // 注：window.onresize只能在项目内触发1次
    window.onresize = function windowResize() {
      // 通过捕获系统的onresize事件触发我们需要执行的事件
      this.allDiv = document.documentElement.clientHeight + "px";
    };
  }
};
</script>
