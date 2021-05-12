<template>
  <v-container>
    <v-row>
      <v-col cols="2" class="pb-0">
        <v-card height="100%" class="mx-auto">
          <v-navigation-drawer permanent>
            <v-list-item>
              <v-list-item-content>
                <v-btn
                  class="d-none d-md-flex"
                  color="cyan"
                  elevation="2"
                  @click="intoAdd"
                  large
                  dark
                >
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
                @click="intoAdd"
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

      <v-col cols="10" class="elevation-2 mt-3">
        <v-row>
          <v-col cols="2" md="6">
            <div class="text-h5 title pt-6">主题</div>
          </v-col>
          <v-col cols="10" md="6">
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
                      <v-avatar size="36px">
                        <img
                          alt="Avatar"
                          :src="
                            baseurl +
                            '/File/ShowNoticeImg?filePath=' +
                            item.user.icon
                          "
                        />
                      </v-avatar>
                    </v-col>
                    <v-col cols="8" sm="5" md="8">
                      [{{ item.type }}] {{ item.title }}
                    </v-col>

                    <v-col cols="2" sm="5" md="3" class="hidden-sm-and-down">
                      <v-subheader>
                        {{ item.user.username }}<br />
                        {{ item.time }}</v-subheader
                      >
                    </v-col>
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
                      color="indigo"
                      @click="intoComment(item.id)"
                    >
                      <v-icon dark> mdi-login </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="teal">
                      <v-icon dark> mdi-format-list-bulleted-square </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark> mdi-pencil </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="purple">
                      <v-icon dark> mdi-android </v-icon>
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
export default {
  data() {
    return {
      items: [
        { title: "添加主题", icon: "mdi-toy-brick-plus" },
        { title: "其他1", icon: "mdi-image" },
        { title: "其他2", icon: "mdi-help-box" },
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
    initializa() {
      //初始化创建
      discussApi
        .getDiscussByPlateId(this.$store.state.home.plateId, this.pageIndex)
        .then((resp) => {
          this.discussList = resp.data.pageData;
          this.total = resp.data.total;
          this.pageSize = resp.data.pageSize;
        });
    },
    intoAdd() {
      this.$router.push({ path: "/addDiscuss" });
    },
    intoComment(discussId) {
      this.$store.dispatch("set_discussId", discussId);
      this.$router.push({ path: "/comment" });
    },
  },
};
</script>