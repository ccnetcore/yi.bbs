<template>
  <div class="text-center">
    <v-overlay :value="this.$store.state.loader.load">
      <v-progress-circular
        indeterminate
        size="64"
      ></v-progress-circular>
    </v-overlay>
  </div>
</template>