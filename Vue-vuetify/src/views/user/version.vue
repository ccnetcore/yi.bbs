<template>
<v-container fluid>
  <v-timeline>
    <v-timeline-item
      v-for="(item,key) in form"
      :key="key"
      :color="item.color"
      large
    >
      <template v-slot:opposite>
        <span>2020/10/01 10:21:51</span>
      </template>
       <v-hover v-slot="{ hover }">
      <v-card  :elevation="hover ? 12 : 2">
        <v-card-title class="headline">
          {{item.ver}}
        </v-card-title>
        <v-card-text v-html="item.context">
       
        </v-card-text>
      </v-card>
        </v-hover>
    </v-timeline-item>
  </v-timeline>
</v-container>
</template>
<script>
import versionApi from "@/api/versionApi";
export default {
data(){
    return{
        form:[]
    }
},
created(){
this.initializa();
},
methods:{
     initializa() {
        versionApi.getVersions().then(resp=>{
            this.form= resp.data;
        })
     }
}
}

</script>