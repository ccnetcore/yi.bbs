<template>
  <div>
    <v-breadcrumbs :items="items" class="grey lighten-5" >
      <template v-slot:item="{ item }">
        <v-breadcrumbs-item :to="item.to"  :disabled="false">
          {{ item.text }}
        </v-breadcrumbs-item>
      </template>
    </v-breadcrumbs>

    <v-container>
      <v-slide-y-transition>
        <router-view></router-view>
      </v-slide-y-transition>
    </v-container>
  </div>
</template>
<script>
export default {
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
