<template>
  <div>
    <v-container>
      <v-row class="text-center align-center">
        <v-col col="12" md="12">
          <v-card>
            <v-switch
              v-model="switch1"
              :label="'是否开启深色模式'"
              @click="dark()"
            ></v-switch>
          </v-card>
        </v-col>
        <v-col col="12" md="6">
          <v-card>
            <v-color-picker
              dot-size="25"
              show-swatches
              swatches-max-height="200"
              v-model="color1"
              mode="rgba"
            ></v-color-picker>
            <v-btn @click="updata1" color="cyan">修改核心主题</v-btn>
          </v-card>
        </v-col>

        <v-col col="12" md="6">
          <v-card>
            <v-color-picker
              dot-size="25"
              show-swatches
              swatches-max-height="200"
              v-model="color2"
              mode="rgba"
            ></v-color-picker>
            <v-btn @click="updata2" color="blue">修改次要主题</v-btn>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>
<script>
export default {
  data() {
    return {
      switch1: false,
      color1: "",
      color2: "",
    };
  },
  methods: {
    dark() {
      this.$vuetify.theme.dark = this.switch1;
    },

    updata1() {
      const cor = this.color1.substr(0, 7);
      var themeData = this.$store.state.theme.light;
      themeData.cyan = cor;
      this.$store.dispatch("set_light", themeData);
    },
    updata2() {
      const cor = this.color2.substr(0, 7);
      var themeData = this.$store.state.theme.light;
      themeData.blue = cor;
      this.$store.dispatch("set_light", themeData);
    },
  },
};
</script>