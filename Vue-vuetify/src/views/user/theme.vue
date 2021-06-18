<template>
  <div>
    <v-container fluid>
      <v-card class="pa-8">
        <p class="mb-4 text-h6 text--secondary">样式设置</p>
        <v-divider></v-divider>

        <v-row>
          <v-col cols="9" md="10">
            <p class="my-4 ml-4 text-h7 font-weight-bold">深色模式</p>
            <v-subheader>开启后，样式将变更为深色</v-subheader>
          </v-col>

          <v-col cols="3" md="2">
            <v-switch inset v-model="switch1" @click="dark()"></v-switch>
          </v-col>
        </v-row>
        <v-divider></v-divider>

        <v-row>
          <v-col cols="9" md="10">
            <p class="my-4 ml-4 text-h7 font-weight-bold">核心主题</p>
            <v-subheader>可修改相当核心主题颜色</v-subheader>
          </v-col>

          <v-col cols="3" md="2">
            <v-btn class="mt-4" dark @click="openDialog(1)" color="cyan">修改</v-btn>
          </v-col>
        </v-row>
        <v-divider></v-divider>

        <v-row>
          <v-col cols="9" md="10">
            <p class="my-4 ml-4 text-h7 font-weight-bold">次要主题</p>
            <v-subheader>可修改当前次要主题颜色</v-subheader>
          </v-col>

          <v-col cols="3" md="2">
            <v-btn class="mt-4" dark @click="openDialog(2)" color="blue">修改</v-btn>
          </v-col>
        </v-row>
        <v-divider></v-divider>
      </v-card>
    </v-container>

    <v-row justify="center">
      <v-dialog
        v-model="dialog"
        fullscreen
        hide-overlay
        transition="dialog-bottom-transition"
      >
        <v-card class="text-center align-center">
          <v-toolbar dark color="blue">
            <v-btn icon dark @click="dialog = false">
              <v-icon>mdi-close</v-icon>
            </v-btn>
            <v-toolbar-title>色彩提取</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
              <v-btn dark text @click="save"> 保存 </v-btn>
            </v-toolbar-items>
          </v-toolbar>

          <v-color-picker
          class="mx-auto mt-8"
            dot-size="25"
            show-swatches
            width="500"
            swatches-max-height="200"
            v-model="color1"
            mode="rgba"
          ></v-color-picker>

        </v-card>
      </v-dialog>
    </v-row>
  </div>
</template>
<script>
export default {
  data() {
    return {
      dialog: false,
      notifications: false,
      sound: true,
      widgets: false,
      switch1: false,
      color1: "",
      open:0
    };
  },
  methods: {
    dark() {
      this.$vuetify.theme.dark = this.switch1;
    },
    openDialog(id) {
     this.open=id;
      this.dialog = true;
    },
    save() {
      const cor = this.color1.substr(0, 7);
      var themeData = this.$store.state.theme.light;

      switch(this.open)
      {
        case 1: themeData.cyan = cor;break;
        case 2:themeData.blue = cor;break;
      }
     
      this.$store.dispatch("set_light", themeData);
      this.dialog=false;
    },
  },
};
</script>