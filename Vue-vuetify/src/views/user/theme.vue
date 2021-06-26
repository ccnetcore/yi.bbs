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
            <v-switch inset v-model="$vuetify.theme.dark" ></v-switch>
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



        <v-row>
          <v-col cols="9" md="10">
            <p class="my-4 ml-4 text-h7 font-weight-bold">语言</p>
            <v-subheader>可切语言，仅限已翻译过部分</v-subheader>
          </v-col>

          <v-col cols="3" md="2">

<v-menu offset-y>
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          color="cyan"
          dark
          v-bind="attrs"
          v-on="on"
          class="mt-4"
        >
          修改
        </v-btn>
      </template>
      <v-list>
        <v-list-item
          v-for="(item, index) in items"
          :key="index"
          @click="updateLang(item.id)"
        >
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>


         
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
      items: [
        {id:1, title: '简体中文' },
        {id:2, title: 'English' },
        {id:3, title: '日本語' },
      ],
      dialog: false,
      notifications: false,
      sound: true,
      widgets: false,
      color1: "",
      open:0
    };
  },
  methods: {
updateLang(id)
{
  var lang="zhHans";
  switch(id)
  {
    case 1: break;
    case 2: lang='en'; break;
    case 3: lang='ja'; break;
  }
  this.$vuetify.lang.current=lang;
},

    openDialog(id) {
     this.open=id;
      this.dialog = true;
    },
    save() {
      var cor = this.color1.substr(0, 7);
      switch(this.open)
      {
        case 1: this.$vuetify.theme.themes.light.cyan = cor;break;
        case 2:this.$vuetify.theme.themes.light.blue = cor;break;
      }
      this.$vuetify.theme.dark=true;
      this.$vuetify.theme.dark=false;
      this.dialog=false;

    },
  },
};
</script>