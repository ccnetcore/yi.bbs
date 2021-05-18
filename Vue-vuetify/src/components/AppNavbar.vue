<template>
  <v-card class="mx-auto " height="100%" width="100%">


    <v-list-item >
      <v-list-item-content >
        <v-list-item-title class="title blue--text"> 服装交流平台 </v-list-item-title>
        <v-list-item-subtitle> 正式版 </v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>

    <v-divider></v-divider>

    <v-list class="blue--text">
      <v-list-item v-for="item in items" :key="item.title" :to="item.router" >
        <v-list-item-icon>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-list-group :value="true" prepend-icon="mdi-account-circle" class="blue--text">
        <template v-slot:activator>
          <v-list-item-title>我的权限</v-list-item-title>
        </template>
        <v-list-item v-for="(items, i) in cruds" :key="i" :to="items.router">
          <v-list-item-title v-text="items.title"></v-list-item-title>

          <v-list-item-icon>
            <v-icon v-text="items.icon"></v-icon>
          </v-list-item-icon>
        </v-list-item>
      </v-list-group>

      <v-list-group :value="true" prepend-icon="mdi-account-circle" class="blue--text">
        <template v-slot:activator>
          <v-list-item-title>其他</v-list-item-title>
        </template>
        <v-list-item v-for="(items, i) in other" :key="i" :to="items.router">
          <v-list-item-title v-text="items.title"></v-list-item-title>

          <v-list-item-icon>
            <v-icon v-text="items.icon"></v-icon>
          </v-list-item-icon>
        </v-list-item>
      </v-list-group>
    </v-list>
  </v-card>
</template>
<script>
import userApi from "@/api/userApi";
import myaction from "../utils/myaction";
export default {
  data() {
    return {
      actions: [],
      cruds: [], //这个是权限目录
      other: [], //这个是其他目录
      items: [], //这个是根目录
    };
  },
  created() {
    this.initialize();
  },
  methods: {
    initialize() {
//先查询是否在myactions中
//如果在，添加到我的权限中
//如果不在，再判断前4个字符是不是/my/,是就添加到其他中，不是就添加到根目录
      var user = this.$store.state.user.user;
      userApi.getActionByUserId(user.id).then((resp) => {
        const response = resp.data;
        this.actions = response;

        for (var i = 0; i < this.actions.length; i++) {
          if (this.actions[i].router == "") {
            continue;
          }
          var mydata = {
            title: "",
            icon: "mdi-view-dashboard",
            router: "/index",
          };
          mydata.title = this.actions[i].action_name;
          mydata.icon = this.actions[i].icon;
          mydata.router=this.actions[i].router;

          if (myaction.actionList.indexOf(this.actions[i].action_name) != -1) {
            this.cruds.push(mydata);
          } else {
            var data1 = this.actions[i].router.substring(0, 4); //前4个 ：/my

            if (data1 != "/my/") {
              this.items.push(mydata);
            } else {
              var data2 = this.actions[i].router
                .substring(4)
                .replace(/\//g, "%2F"); //后面
              mydata.router = data1 + data2;
              this.other.push(mydata);
            }
          }
        }
      });
    },
  },
};
</script>