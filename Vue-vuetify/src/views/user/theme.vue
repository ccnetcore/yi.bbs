<template>
  <v-card>
    <v-color-picker
      dot-size="25"
      show-swatches
      swatches-max-height="200"
      v-model="color1"
      mode="rgba"
    ></v-color-picker>
    <v-btn @click="test" color="cyan">修改主题</v-btn>
  </v-card>
</template>
<script>
export default {
  data() {
    return {
      color1: "",
    };
  },
  methods: {
    test() {
      const cor = this.color1.substr(0, 7);
      var themeData = this.$store.state.theme.light;
      themeData.cyan = cor;
      this.$store.dispatch("set_light", themeData);
    },
  },
};
</script>