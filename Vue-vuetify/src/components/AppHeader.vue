<template>
  <v-row class="mt-2">
    <v-col cols="5" offset="0" class="mt-2" offset-md="4" md="5">
      <v-text-field
        label="全站搜索"
        placeholder="输入"
        dense
        prepend-inner-icon="mdi-magnify"
      >
        <v-icon slot="append" color="blue"> mdi-send </v-icon>
      </v-text-field>
    </v-col>
    
    <v-col cols="7" md="3" class="mb-2 text-right">
         <router-link :to="{ path: '/shop' }">
      <v-btn icon medium>
        <v-icon color="blue" dark> mdi-content-paste </v-icon>
      </v-btn>
            </router-link>
      <router-link :to="{ path: '/index' }">
        <v-btn icon medium class="mr-2">
          <v-icon dark color="blue"> mdi-home </v-icon>
          
        </v-btn>
      </router-link>

      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-avatar size="36" class="elevation-2">
              <!-- <img src="https://z3.ax1x.com/2021/05/09/gJadhD.jpg" /> -->
              <img
                :src="
                  baseurl +
                  '/File/ShowNoticeImg?filePath=' +
                  $store.state.user.user.icon
                "
              />
            </v-avatar>
          </v-btn>
        </template>
        <v-card class="mx-auto" tile width="200">
          <v-system-bar></v-system-bar>

          <v-list-item>
            <v-list-item-icon>
              <v-icon v-text="'mdi-star'"></v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title v-if="this.$store.state.user.user != null"
                >用户：{{
                  this.$store.state.user.user.username
                }}</v-list-item-title
              >
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-icon>
              <v-icon v-text="'mdi-star'"></v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title v-if="this.$store.state.user.user != null"
                >等级：{{
                  this.$store.state.user.user.level
                }}</v-list-item-title
              >
            </v-list-item-content>
          </v-list-item>
          <v-divider></v-divider>
          <v-list nav dense>
            <v-list-item-group v-model="selectedItem" color="primary">
              <v-list-item
                v-for="(item, i) in items"
                :key="i"
                :to="item.router"
              >
                <v-list-item-icon>
                  <v-icon v-text="item.icon"></v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title v-text="item.text"></v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-divider></v-divider>
              <v-list-item>
                <v-list-item-icon>
                  <v-icon v-text="'mdi-star'"></v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title
                    @click="off()"
                    v-text="'退出登录'"
                  ></v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-system-bar></v-system-bar>
            </v-list-item-group>
          </v-list>
        </v-card>
      </v-menu>
    </v-col>
  </v-row>
</template>
<script>
export default {
  data: () => ({
    baseurl: "",
    selectedItem: "",
    items: [
      { text: "个人资料", icon: "mdi-folder", router: "/userInfo" },
      { text: "我的主题", icon: "mdi-account-multiple", router: "/myDiscuss" },
      { text: "我的收藏", icon: "mdi-star", router: "/collection" },
      { text: "我的仓库", icon: "mdi-home-account", router: "/warehouse" },
      { text: "我的好友", icon: "mdi-account-group", router: "/friend" },
    ],
  }),
  created() {
    this.initialize();
  },
  mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  methods: {
    initialize() {},
    off() {
      this.$store.dispatch("Logout").then((resp) => {
        this.$router.push("/login");
      });
    },
  },
};
</script>
