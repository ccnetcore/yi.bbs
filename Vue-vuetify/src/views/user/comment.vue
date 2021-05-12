<template>
  <div>
    <v-card class="mx-auto my-2" max-width="100%">
      <v-card-text>
        
        
        <p class="display-1 text--primary">{{ discussData.title }}</p>
<v-divider class="my-2"></v-divider>
                      <v-avatar size="60px">
                        <img
                        v-if="discussData.user!=undefined"
                          alt="Avatar"
                           :src="baseurl+'/File/ShowNoticeImg?filePath='+discussData.user.icon"
                        />
                      </v-avatar>
        <p v-if="discussData.user!=undefined">作者：{{ discussData.user.username }}</p>
        
         <p>时间：{{ discussData.time }}</p>
        <v-divider class="my-4"></v-divider>
        <div class="text--primary" v-html="discussData.content"></div>
      </v-card-text>
      <v-card-actions>
        <v-btn text color="teal accent-4" @click="reveal = true">
          更多信息
        </v-btn>
        <v-btn class="mx-2" fab dark small color="primary">
          <v-icon dark> mdi-heart </v-icon>
        </v-btn>

        <v-btn class="mx-2" fab dark small color="primary">
          <v-icon dark> mdi-heart </v-icon>
        </v-btn>
        <v-btn class="mx-2" fab dark small color="primary">
          <v-icon dark> mdi-heart </v-icon>
        </v-btn>
      </v-card-actions>

      <v-expand-transition>
        <v-card
          v-if="reveal"
          class="transition-fast-in-fast-out v-card--reveal"
          style="height: 100%"
        >
          <v-card-text class="pb-0">
            <p class="display-1 text--primary">简介</p>
            <p>{{ discussData.introduction }}</p>
          </v-card-text>
          <v-card-actions class="pt-0">
            <v-btn text color="teal accent-4" @click="reveal = false">
              关闭
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-expand-transition>
    </v-card>
    <v-card class="my-2 mb-6">
      <v-list>
        <v-subheader>评论</v-subheader>
        <v-text-field class="mx-6" v-model="form.content" label="发表评论">
          <v-icon slot="append" color="green" @click="sentComment()">
            mdi-send
          </v-icon></v-text-field
        >


  



      
        <v-list-item-group v-model="selectedItem" color="primary">
          <v-list-item v-for="(item, i) in commentList" :key="i">
            <v-list-item-icon>
               <v-avatar size="36px">
                        <img
                          alt="Avatar"
                           :src="baseurl+'/File/ShowNoticeImg?filePath='+item.user.icon"
                        />
                      </v-avatar>
            </v-list-item-icon>
            <v-list-item-content>
              <p class="light-blue--text">
                {{ i + 1 }}楼:<br />
                作者：{{item.user.username}}<v-divider class="mx-2" vertical></v-divider
                >发表时间： {{ item.time }}
              </p>
             
              <v-list-item-title v-text="item.content"></v-list-item-title>
               <v-divider></v-divider>
            </v-list-item-content>
       
          </v-list-item>
          
        </v-list-item-group>
      </v-list>
  <div class="text-center mt-4 mb-12">
      <v-pagination v-model="pageIndex" :length="lenData" :total-visible="7"
        prev-icon="mdi-menu-left"
        next-icon="mdi-menu-right" circle></v-pagination>
    </div>
    </v-card>
  </div>
</template>
<script>
import discussApi from "@/api/discussApi";
import commentApi from "@/api/commentApi";
export default {
  data: () => ({
    pageIndex: 1,
      pageSize:10,
      total:100,
    discussData: {

    },
    commentList: [],
    reveal: false,
    selectedItem: "",
    form: {
      content: "",
    },
  }),
  created() {
    this.initializa();
  },
    watch: {
    pageIndex: {
      handler() {
        this.initializa();
      },
    },
  },
   computed: {
    lenData() {
      return Math.floor(this.total / this.pageSize) + 1;
    }
   },
       mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  methods: {
    initializa() {
      discussApi
        .getDiscussByDiscussId(this.$store.state.home.discussId)
        .then((resp) => {
            this.discussData=resp.data;
        });
      commentApi
        .getCommentsByDiscussId(this.$store.state.home.discussId,this.pageIndex)
        .then((resp) => {
                   this.commentList = resp.data.pageData;
          this.pageSize=resp.data.pageSize;
          this.total=resp.data.total;
        });
    },
    sentComment() {
      commentApi
        .addComment(this.form, this.$store.state.home.discussId)
        .then((resp) => {
          //设置等级
          this.$store.dispatch("setLevel",resp.data.level);

          this.form.content="";
          this.initializa();
        });
    },
  },
};
</script>
<style scoped>
.v-card--reveal {
  bottom: 0;
  opacity: 1 !important;
  position: absolute;
  width: 100%;
}
</style>