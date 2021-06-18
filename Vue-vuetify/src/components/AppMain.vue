<template>
  <div>
    <v-breadcrumbs :items="items" class="grey lighten-5">
      <template v-slot:item="{ item }">
        <v-breadcrumbs-item :to="item.to" :disabled="false">
          {{ item.text }}
        </v-breadcrumbs-item>
      </template>
    </v-breadcrumbs>
    <v-progress-linear
      :active="this.$store.state.loader.load"
      :indeterminate="this.$store.state.loader.load"
      color="cyan accent-4"
      striped
    ></v-progress-linear>

    <v-container fluid>
      <v-slide-y-transition>
        <router-view></router-view>
      </v-slide-y-transition>

      <!-- <Loader></Loader> -->
    </v-container>
  </div>
</template>
<script>
// import Loader from "../components/Loader.vue"
export default {
  // components: {
  //     Loader
  // },
  data: () => ({
    items: [{ text: "首页", to: "/index" }],
  }),
  watch: {
    $route(to, from) {
      if (to.path == "/index") {
        this.items = [{ text: "首页", to: "/index" }];
      } else if (to.path == "/discuss") {
        this.items = [
          { text: "首页", to: "/index" },
          { text: "主题", to: "/discuss" },
        ];
      } else if (to.path == "/comment") {
        this.items = [
          { text: "首页", to: "/index" },
          { text: "主题", to: "/discuss" },
          { text: "评论", to: "/comment" },
        ];
      } else if (to.path == "/addDiscuss") {
        this.items = [
          { text: "首页", to: "/index" },
          { text: "主题", to: "/discuss" },
          { text: "添加主题", to: "/addDiscuss" },
        ];
      } else if (to.path == "/action") {
        this.items = [
          { text: "首页", to: "/index" },
          { text: "权限管理", to: "/action" },
        ];
      } else if (to.path == "/user") {
        this.items = [
          { text: "首页", to: "/index" },
          { text: "用户管理", to: "/user" },
        ];
      } else if (to.path == "/role") {
        this.items = [
          { text: "首页", to: "/index" },
          { text: "角色管理", to: "/role" },
        ];
      }
    },
  },
};
</script>
