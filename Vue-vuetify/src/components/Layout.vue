<template>
  <div>
    <v-app id="inspire">
      <v-navigation-drawer v-model="drawer" class="accent-4" app>
        <app-navbar></app-navbar>
        <template v-slot:append>
          <div class="pa-2">
            <v-btn block @click="off()"> 退出 </v-btn>
          </div>
        </template>
      </v-navigation-drawer>

      <v-app-bar app class="white">
        <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
        <app-header></app-header>
      </v-app-bar>

      <v-main>
        <app-main></app-main>
        
           <v-footer :absolute="true"
            >
          <v-col class="text-center" cols="12">
            {{ new Date().getFullYear() }} — <strong>Jiftcc</strong>
          </v-col>
        </v-footer>
      </v-main>
    </v-app>
  </div>
</template>
<script>
import AppHeader from "./AppHeader.vue";
import AppNavbar from "./AppNavbar.vue";
import AppMain from "./AppMain.vue";
import accountApi from "@/api/accountApi";
export default {
  data() {
    return {
      drawer: null,
    };
  },
  methods: {
    off() {
      this.$store.dispatch("Logout").then((resp) => {
        this.$router.push("/login");
      });
    },
  },
  components: {
    AppHeader,
    AppNavbar,
    AppMain,
  },
};
</script>
