<template >
  <div >
    <v-row >
      <v-col cols="12" sm="6" md="8" >
        <v-carousel
          cycle
          height="300"
          hide-delimiter-background
          show-arrows-on-hover
         
        >
          <v-carousel-item v-for="(slide, i) in slides" :key="i">
            <v-sheet :color="colors[i]" height="100%">
              <v-row class="fill-height" align="center" justify="center">
                <div class="display-3">{{ slide }} Slide</div>
              </v-row>
            </v-sheet>
          </v-carousel-item>
        </v-carousel>
      </v-col>
      <v-col cols="12" sm="6" md="4">
        <v-card class="mt-8 mx-auto">
          <v-sheet
            class="v-sheet--offset mx-auto"
            color="cyan"
            elevation="12"
            max-width="calc(100% - 32px)"
          >
            <v-sparkline
              :labels="labels"
              :value="value"
              color="white"
              line-width="2"
              padding="16"
            ></v-sparkline>
          </v-sheet>

          <v-card-text class="pt-0">
            <div class="title font-weight-light mb-2">User Registrations</div>
            <div class="subheading font-weight-light grey--text">
              Last Campaign Performance
            </div>
            <v-divider class="my-2"></v-divider>
            <v-icon class="mr-2" small> mdi-clock </v-icon>
            <span class="caption grey--text font-weight-light"
              >last registration 26 minutes ago</span
            >
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>



    <v-row class="text-center">
      <v-col v-for="(item,i) in plateList" :key="i" cols="12" sm="4" md="3">
        <v-hover v-slot="{ hover }">
          <v-card class="mx-auto" color="grey lighten-4" max-width="600">
            <v-img
              :aspect-ratio="16 / 9"
              src="https://z3.ax1x.com/2021/05/05/gMZNjK.jpg"
            >
              <v-expand-transition>
                <div
                  v-if="hover"
                  class="d-flex transition-fast-in-fast-out cyan darken-2 v-card--reveal display-3 white--text"
                  style="height: 100%"
                >
                  {{item.name}}
                </div>
              </v-expand-transition>
            </v-img>
            <v-card-text class="pt-6" style="position: relative">
              <v-btn
                absolute
                color="cyan"
                class="white--text"
                fab
                large
                right
                top
                @click="intoDiscuss(item.id)"
              >
                <v-icon>mdi-chevron-right</v-icon>
              </v-btn>
              <div class="font-weight-light title mb-2 cyan--text">
               {{item.name}}
              </div>
              <div class="font-weight-light mb-2 grey--text">
                Our Vintage kitchen utensils delight any chef.<br />
                Made of bamboo by hand
              </div>
            </v-card-text>
          </v-card>
        </v-hover>
      </v-col>

      <v-col cols="12" sm="6" md="4"> </v-col>
    </v-row>
  </div>
</template>
<script>
import plateApi from "@/api/plateApi";

export default {
  data() {
    return {
      plateList: [],
      labels: ["12am", "3am", "6am", "9am", "12pm", "3pm", "6pm"],

      value: [423, 446, 675, 510, 590, 610, 760],
      colors: [
        "cyan",
        "light-blue",
        "cyan",
        "light-blue"
      ],
      slides: ["First", "Second", "Third", "Fourth"],
    };
  },
  created(){
    this.initializa();
  },
  methods: {
    initializa() {
      plateApi.getPlates().then((resp) => {
        this.plateList = resp.data;
      });
    },
    intoDiscuss(plateId) {
     this.$store.dispatch("set_plateId",plateId)
      this.$router.push({ path: "/discuss"});
    },
  },
};
</script>
<style scoped>
.v-card--reveal {
  align-items: center;
  bottom: 0;
  justify-content: center;
  opacity: 0.5;
  position: absolute;
  width: 100%;
}
</style>

<style scoped>
.v-sheet--offset {
  top: -24px;
  position: relative;
}
</style>