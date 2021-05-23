<template>
  <div>正在验证qq中。。。。。。</div>
</template>
<script>
export default {
  created() {
    if (QC.Login.check()) {
      //检查是否登录
       alert("已经登录");
      QC.Login.getMe(function (openId, accessToken) {
        that.openId = openId;
        that.accessToken = accessToken;
        that.loginByQQ();
      });
    } else {
      alert("登录失败");
    }
  },
};
</script>