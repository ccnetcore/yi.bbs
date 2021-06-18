<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" md="8">
        <v-row>
              
          <v-col cols="12">
            <p class="title text--secondary">好友列表</p>
          </v-col>
          <v-col cols="12">
            <v-text-field label="好友搜索" prepend-inner-icon="mdi-magnify">
              <v-icon slot="append" color="cyan"> mdi-send </v-icon>
            </v-text-field>
          </v-col>



          <v-col cols="12" sm="6" md="4" v-for="index in 8" :key="index">
              <v-hover v-slot="{ hover }">
            <v-card class="mx-auto" color="cyan" dark :elevation="hover ? 12 : 2">
              <v-card-title>
                <v-row>
                  <v-col cols="2">
                    <v-icon large left> mdi-twitter </v-icon></v-col
                  >

                  <v-col cols="8" class="text-center">
                    <span class="text-h8 font-weight-light "
                      >Twitter</span
                    ></v-col
                  >

                  <v-col cols="2"> <v-icon large> mdi-twitter </v-icon></v-col>
                </v-row>
              </v-card-title>

              <v-card-text class="text-h7 font-weight-bold">
                "Turns out semicolon-less style is easier and safer in TS
                because most gotcha edge cases are type invalid as well."
              </v-card-text>

              <v-card-actions>
                <v-list-item class="grow">
                  <v-list-item-avatar color="grey darken-3">
                    <v-img
                      class="elevation-6"
                      alt=""
                      src="https://avataaars.io/?avatarStyle=Transparent&topType=ShortHairShortCurly&accessoriesType=Prescription02&hairColor=Black&facialHairType=Blank&clotheType=Hoodie&clotheColor=White&eyeType=Default&eyebrowType=DefaultNatural&mouthType=Default&skinColor=Light"
                    ></v-img>
                  </v-list-item-avatar>

                  <v-list-item-content>
                    <v-list-item-title>Evan You</v-list-item-title>
                  </v-list-item-content>

                  <v-row align="center" justify="end">
                    <v-icon class="mr-1"> mdi-heart </v-icon>
                    <span class="subheading mr-2">256</span>
                    <span class="mr-1">·</span>
                    <v-icon class="mr-1"> mdi-share-variant </v-icon>
                    <span class="subheading">45</span>
                  </v-row>
                </v-list-item>
              </v-card-actions>
            </v-card>
              </v-hover>
          </v-col>
        </v-row>
      </v-col>

      <v-col cols="12" md="4">
        <v-row>
          <v-col cols="12">
            <p class="title text--secondary">请求列表</p>
          </v-col>
          <v-col cols="12" class="text-center">
            <v-btn width="100%" color="cyan" dark>添加好友</v-btn>
          </v-col>
          <v-col cols="12" v-for="index in 8" :key="index">
              <v-hover v-slot="{ hover }">
            <v-card class="mx-auto" color="cyan" dark :elevation="hover ? 12 : 2">
              <v-card-title>
                <v-row>
                  <v-col cols="2">
                    <v-icon large left> mdi-twitter </v-icon></v-col
                  >

                  <v-col cols="8" class="text-center">
                    <span class="text-h8 font-weight-light"
                      >Twitter</span
                    ></v-col
                  >

                  <v-col cols="2"> <v-icon large> mdi-twitter </v-icon></v-col>
                </v-row>
              </v-card-title>

              <v-card-text class="text-h7 font-weight-bold">
                "Turns out semicolon-less style is easier and safer in TS
                because most gotcha edge cases are type invalid as well."
              </v-card-text>

              <v-card-actions>
                <v-list-item class="grow">
                  <v-list-item-avatar color="grey darken-3">
                    <v-img
                      class="elevation-6"
                      alt=""
                      src="https://avataaars.io/?avatarStyle=Transparent&topType=ShortHairShortCurly&accessoriesType=Prescription02&hairColor=Black&facialHairType=Blank&clotheType=Hoodie&clotheColor=White&eyeType=Default&eyebrowType=DefaultNatural&mouthType=Default&skinColor=Light"
                    ></v-img>
                  </v-list-item-avatar>

                  <v-list-item-content>
                    <v-list-item-title>Evan You</v-list-item-title>
                  </v-list-item-content>

                  <v-row align="center" justify="end">
                    <v-icon class="mr-1"> mdi-heart </v-icon>
                    <span class="subheading mr-2">256</span>
                    <span class="mr-1">·</span>
                    <v-icon class="mr-1"> mdi-share-variant </v-icon>
                    <span class="subheading">45</span>
                  </v-row>
                </v-list-item>
              </v-card-actions>
            </v-card>
              </v-hover>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  data: () => ({
    //
  }),
};
</script>