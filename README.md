# yi.bbs

## 基于.Net5与Vue下的BBS论坛毕业设计系统

本论坛极度自由化！论坛各处需求，无需通过修改代码或者数据库，可直接进入系统可视化管理。
成为一个为你私人定制的论坛！

#### 适宜人群：

１．私人想搭建论坛

２．私人想搭建博客

３．为了信息交流

４．参考学习

5．完成课程设计或者毕业设计（请著名使用Yｉ框架）

#### 更新目录：
v1.0.0------2021/5/12

我们将持续更新下去！

#### 系统特点：

1：前后端完全分离  2：响应式布局  3：高并发  4：可集群  5：安全  6：高扩展  7：高性能  8：逻辑删除  9：简易


#### 功能模块：

用户管理、权限管理、角色管理、主题管理，板块管理，版主管理，标签管理、等级管理，经验管理、信息管理

等等，持续更新

#### 演示效果：
登录：

![登录](https://user-images.githubusercontent.com/68722157/118130434-edbd4980-b42f-11eb-884f-f5b6f3c9cdc3.png)
![登录2](https://user-images.githubusercontent.com/68722157/118130461-f44bc100-b42f-11eb-809e-9e5bb1a28f45.png)

主页：

![主界面](https://user-images.githubusercontent.com/68722157/118130541-0e859f00-b430-11eb-9a7e-e772de028960.png)

![主界面2](https://user-images.githubusercontent.com/68722157/118130546-0fb6cc00-b430-11eb-9037-0806c5ebdaf5.png)

主题：

![主题](https://user-images.githubusercontent.com/68722157/118130691-3f65d400-b430-11eb-8dd1-def151292b81.png)

![主题2](https://user-images.githubusercontent.com/68722157/118130701-412f9780-b430-11eb-94e6-69aac07d0696.png)

我的主题：

![我的主题](https://user-images.githubusercontent.com/68722157/118131928-b354ac00-b431-11eb-8eff-b8605e02229a.png)

![我的主题2](https://user-images.githubusercontent.com/68722157/118131937-b485d900-b431-11eb-9977-66d3e758ea2a.png)

评论：

![评论](https://user-images.githubusercontent.com/68722157/118130568-17767080-b430-11eb-961c-4bc3e94a47a6.png)

![评论2](https://user-images.githubusercontent.com/68722157/118130571-18a79d80-b430-11eb-844b-7e05f5042e0f.png)

添加主题：

![添加主题](https://user-images.githubusercontent.com/68722157/118130605-21986f00-b430-11eb-9366-0dfe6f708aed.png)

![添加主题2](https://user-images.githubusercontent.com/68722157/118130624-26f5b980-b430-11eb-8e21-0c207406a3b5.png)

我的信息：

![我的信息](https://user-images.githubusercontent.com/68722157/118130655-3248e500-b430-11eb-80d1-564644fbeb77.png)

![我的信息2](https://user-images.githubusercontent.com/68722157/118130659-337a1200-b430-11eb-95fc-1c6e45b1aec3.png)

权限管理：

![权限管理](https://user-images.githubusercontent.com/68722157/118130742-4ee51d00-b430-11eb-83b4-8be49285a961.png)

![权限管理2](https://user-images.githubusercontent.com/68722157/118130748-51477700-b430-11eb-8297-e5bf92472902.png)

导航：

![导航](https://user-images.githubusercontent.com/68722157/118130725-4856a580-b430-11eb-86ca-6e5743ab53ae.png)


#### 其他：
等待更新



==========================================================================

#### 本系统应用Yi意框架
基于.NET5的前后端分离三层架构的高并发高解耦简易型框架--Yi意框架（意义是为了开发更简易）

#### 软件架构

Yi意框架

	Yi意框架源码

		CC.Yi（.Net5核心版本）

	Yi意框架应用（图片管理系统）

		CC.Yi.PictureManagement（Betav0.0.1Yi框架后端）

		CC.Yi.PictureManagement（Vue-ElementUI前端）

	Yi意框架应用（作业管理系统）

		CC.Yi.WorkManagement（v1.0.0Yi意框架后端）

		CC.Yi.WorkManagement（Vue-ElementUI前端）

	Yi意框架应用（权限管理系统）

		CC.Yi.RolePermission（v1.1.0Yi意框架后端）

		CC.Yi.RolePermission（Vue-Vuetify前端）

	Yi意框架应用（题库管理系统）

		CC.Yi.QuestionBank（v1.2.0Yi意框架后端）

		CC.Yi.QuestionBank（Vue-Vuetify前端）

	Yi意框架最终应用（论坛系统）

		CC.Yi.jift（v2.0.0Yi意框架后端）

		CC.Yi.jift（Vue-Vuetify前端）


架构：后端.NET5 ，前端Vue

支持的数据库： SQL Server，MySQL，SQLite，reidis

操作系统：Windows，Linux

扩展：EFcore，Autofac，Identity，Castle，Redis，Swagger，T4 ，Nlog ，Jwt

封装：Json处理模块，滑动验证码模块，base64图片处理模块，HTTP请求,过滤器

思想：简单工厂模式，抽象工厂模式，观察者模式，面向AOP思想，面向对象开发

其他: nginx前后端集群配置，前后端跨域设置，权限认证配置，swaggerToken配置


#### 目录结构
![输入图片说明](https://images.gitee.com/uploads/images/2021/0321/023715_59bef411_3049273.png "屏幕截图.png")

Model：模型层（first code代码优先添加模型，数据自动生成）

DAL：数据处理层（处理数据但并未加载进内存）

BLL：业务逻辑层（数据的逻辑将在这层处理）

Common：工具层（工具人层，方法已封装，一键调用）

API：接口层（接入Swagger，可视化测试接口）


#### 安装教程
我们将在之后更新教程手册！

1.  下载全部源码
2.  使用Visual Studio 2019在windows环境中打开CC.Yi.sln文件即可


#### 使用说明
我们将在之后更新教程手册！

1.  添加一个数据库，并修改连接数据库的配置文件
2.  添加模型类，使用Add-Migration xxx迁移，再使用Update-Database更新数据库
3.  向T4Model添加模型名，一键转换生成T4
4.  控制器构造函数进行依赖注入直接使用

#### 联系我们：
QQ：454313500


