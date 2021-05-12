<template>
<v-app>
    <v-container class="text-center">
 
 <v-btn
      class="m-2"
      fab
      dark
      large
      color="purple"
      @click="mylink()"
    >
      <v-icon dark>
        mdi-android
      </v-icon>
    </v-btn>


    
 
    </v-container>
    </v-app>
</template>
<script>
export default {
  methods: {
    mylink() {
     const mylink=this.$route.params.mylink
        var tempwindow=window.open();
      tempwindow.location=mylink;    
    }
  },
};
</script>