<template>
  <v-container fluid>
    <v-row>
         <!-- 删除提示框 -->
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="headline"
                >你确定要删除此条记录吗？</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeDelete"
                  >取消</v-btn
                >
                <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                  >确定</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>


      <v-col cols="12"  class="elevation-2 mt-3">
        <v-row>
          <v-col cols="4" md="6">
            <div class="text-h5 title pt-6">收藏</div>
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
                      @click="intoComment(item.id, item.plate.id)"
                    >
                      <v-icon dark> mdi-login </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark> mdi-format-list-bulleted-square </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark> mdi-pencil </v-icon>
                    </v-btn>

                    <v-btn class="mx-2" fab dark small color="cyan">
                      <v-icon dark @click="deleteItem(item.id)"> mdi-close </v-icon>
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
import collectionApi from "@/api/collectionApi";
export default {
  data() {
    return {
      dialogDelete: false,
      delId: 0,
      dialog: false,
      plateId: "",
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
      collectionApi.getCollections(this.pageIndex).then((resp) => {
        this.discussList = resp.data.pageData;
        this.total = resp.data.total;
        this.pageSize = resp.data.pageSize;
      });
    },

    deleteItem(id) {
      this.delId = id;
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      collectionApi.delCollection(this.delId).then(() => this.initializa());
      this.closeDelete();
    },
    closeDelete() {
      this.dialogDelete = false;
    },

    close() {
      this.dialog = false;
    },
    save() {
      labelApi.addLabelByUserId(this.form).then(() => this.initializa());
      this.close();
    },
    intoComment(discussId, plateId) {
      this.$store.dispatch("set_plateId", plateId);
      this.$store.dispatch("set_discussId", discussId);
      this.$router.push({ path: "/comment" });
    },
  },
};
</script>