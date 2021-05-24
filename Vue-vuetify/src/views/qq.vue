<template>
  <div>正在验证qq中。。。。。。</div>
</template>
<script>
export default {
  data() {
    return {};
  },
  created() {
    var that = this;

    if (QC.Login.check()) {
      // 检查是否登录
      // QC.api("get_user_info", {})
      //     .success(function(s){//成功回调
      //         alert("获取用户信息成功！当前用户昵称为："+s.data.nickname);
      //     })
      //返回的值为true说明让登录，直接跳转到主页面，并进行自动登录

      QC.Login.getMe(function (openId, accessToken) {
        if (that.$route.query.state == 0) {
          that.$store.dispatch("qqLogin", openId).then((resp) => {
            if (resp.status) {
              that.$router.push({ path: "/" });
            } else {
              alert(resp.msg);
              that.$router.push({ path: "/register" });
            }
          });
        } else if (that.$route.query.state == 1) {
          //如果是为了绑定qq，传opid和userId
          that.$store.dispatch("qqUpdate", openId).then((resp) => {
            if (resp.status) {
              that.$router.push({ path: "/" });
            } else {
              alert(resp.msg);
              that.$router.push({ path: "/userinfo" });
            }
          });
        }
      });
      QC.Login.signOut();
    } else {
      alert("登录失败");
    }
  },
};
</script>