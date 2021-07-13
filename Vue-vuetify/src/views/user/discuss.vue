<template>
  <v-container fluid >
    <v-row >
      <v-col class="pb-0">
        <v-card height="100%" class="mx-auto">
          <v-navigation-drawer right app v-model="drawer">
            <v-list-item>
              <v-list-item-content>
                <v-btn color="cyan" elevation="2" @click="intoAdd" large dark>
                  添加主题
                </v-btn>
              </v-list-item-content>
            </v-list-item>

            <v-divider></v-divider>

            <v-list dense nav>
              <v-list-item
                v-for="myitem in items"
                :key="myitem.title"
                link
                @click="orderby(myitem.id)"
              >
                <v-list-item-icon>
                  <v-icon>{{ myitem.icon }}</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                  <v-list-item-title>{{ myitem.title }}</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-navigation-drawer>
        </v-card>
      </v-col>

      <v-col cols="12" class="elevation-2 mt-3">
        <v-row>
          <v-col cols="4" md="6">
            <div class="text-h5 title pt-6">
              <v-icon class="mx-2" @click="drawer = !drawer">mdi-menu</v-icon>
              主题
            </div>
          </v-col>
          <v-col cols="8" md="6">
            <v-text-field label="搜索">
              <v-icon slot="append" color="red">
                mdi-plus
              </v-icon></v-text-field
            >
          </v-col>
        </v-row>

        <v-row justify="center">
          <v-container>
            <v-expansion-panels inset focusable>
              <v-expansion-panel
                class="p-1"
                v-for="(item, i) in discussList"
                :key="i"
              >
                <v-expansion-panel-header>
                  <v-row align="center" class="spacer" no-gutters>
                    <v-col cols="2" sm="1" md="1">
                      <v-avatar size="36px " @click="intoInfo(item.user.id)">
                        <img
                          alt="Avatar"
                          :src="
                            baseurl +
                            '/File/ShowNoticeImg?filePath=' +
                            item.user.icon
                          "
                        />
                      </v-avatar>
                      <br />
                      <span class="ml-2"> {{ item.user.username }}</span>
                    </v-col>
                    <v-col cols="6" sm="5" md="8" :style="{'color':item.color}">
                      [{{ item.type }}] {{ item.title }}
                    </v-col>

                    <v-col cols="4" sm="5" md="3">
                      <v-subheader>
                        {{ item.time }} 发布 <br />
                        {{ item.see_num }}阅览 | {{ item.agree_num }}点赞
                      </v-subheader>
                    </v-col>


                    
                    <!-- <v-col cols="12"  >
                      <v-subheader class="mr-2">
                        {{ item.time }} 发布 | {{ item.see_num }}阅览 | {{ item.agree_num }}点赞
                      </v-subheader>
                    </v-col> -->
                  </v-row>


                </v-expansion-panel-header>

                <v-expansion-panel-content>
                  {{ item.introduction }}
                  <div class="mt-2">
                    <v-btn
                      class="mr-2"
                      fab
                      small
                      dark
                      color="blue"
                      @click="intoComment(item.id)"
                    >
                      <v-icon dark> mdi-login </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark> mdi-close </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark> mdi-close </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark @click="Collection(item.id)">
                        mdi-star
                      </v-icon>
                    </v-btn>
                  </div>
                </v-expansion-panel-content>
              </v-expansion-panel>
            </v-expansion-panels>
          </v-container>
        </v-row>
      </v-col>
    </v-row>

    <div class="text-center mt-4 mb-10">
      <v-pagination
        color="cyan"
        v-model="pageIndex"
        :length="lenData"
        :total-visible="7"
        prev-icon="mdi-menu-left"
        next-icon="mdi-menu-right"
        circle
      ></v-pagination>
    </div>
  </v-container>
</template>
<script>
import discussApi from "@/api/discussApi";
import collectionApi from "@/api/collectionApi";
export default {
  data() {
    return {
      orderbyId: 0,
      drawer: "true",
      items: [
        { title: "最新主题", icon: "mdi-toy-brick-plus", id: "0" },
        { title: "最热主题", icon: "mdi-image", id: "1" },
        { title: "推荐主题", icon: "mdi-help-box", id: "2" },
      ],
      baseurl: "",
      pageIndex: 1,
      pageSize: 10,
      total: 100,
      discussList: [],
    };
  },
  watch: {
    pageIndex: {
      handler() {
        this.initializa();
      },
    },
  },
  created() {
    this.initializa();
  },
  mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  computed: {
    lenData() {
      return Math.floor(this.total / this.pageSize) + 1;
    },
  },
  methods: {
    Collection(Id) {
      collectionApi.addCollection(Id).then((resp) => {
        if (resp.status) {
          this.$dialog.message.success(resp.msg, {
            position: "top-right",
          });
        } else {
          this.$dialog.message.error(resp.msg, {
            position: "top-right",
          });
        }
      });
    },
    intoInfo(userId) {
      this.$router.push({
        path: `/userInfo`,
        query: {
          userId: userId,
        },
      });
    },
    initializa() {
      //初始化创建
      discussApi
        .getDiscussByPlateId(
          this.$store.state.home.plateId,
          this.pageIndex,
          this.orderbyId
        )
        .then((resp) => {
          this.discussList = resp.data.dataFilter;
          this.total = resp.data.total;
          this.pageSize = resp.data.pageSize;
        });
    },
    intoAdd() {
      this.$router.push({ path: "/addDiscuss" });
    },
    orderby(itemId) {
      this.orderbyId = itemId;
      this.initializa();
    },
    intoComment(discussId) {
      this.$store.dispatch("set_discussId", discussId);
      this.$router.push({ path: "/comment" });
    },
  },
};
</script>