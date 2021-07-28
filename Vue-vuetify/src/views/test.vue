<template>
  <div>
    <v-file-input
    ref="upFile"
      small-chips
      multiple
      label="点击上传文件"
      show-size
      counter
      @change="uploadFile"
    ></v-file-input>





    <p v-for="(item,index) in fileUrls" :key="index">{{item}}</p>
  </div>
  
</template>
<script>
 //   深刻理解一下文件上传：
    // <input type="file" ref="imgFile" @change="uploadImage()" class="d-none" />
    // <v-btn dark color="cyan" @click="choiceImg" class="mt-4">编辑</v-btn>
    // choiceImg：触发input点击事件，这个要绑定要按钮单机事件上
    // uploadImage：发送图片过去，这个要绑定要@change上，当有东西改变

    // 通过ref获取文件，然后通过axios发送文件给后端，后端返回一个回调地址，前端解析一下换成图片和文件名
    import axios from "axios";
export default {
  data:()=>{
    return {
      fileUrls:[],
    }
  },
    mounted() {
    this.baseurl = process.env.VUE_APP_BASE_API;
  },

methods:{
   uploadFile(myFiles) {

  
      let formData = new FormData();
for(var i=0;i<myFiles.length;i++)
{
formData.append("file", myFiles[i]);
}

      
      axios
        .post(this.baseurl + "/File/upLoadFile", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then((response) => {
         var resp=response.data.data;
for(var i=0;i<resp.length;i++)
{
  resp[i]= resp[i].replace("#", this.baseurl);
}



this.fileUrls=resp;


        });
    },
}
}
</script>
