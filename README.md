# MyMusicWebAPI

这是 MyMusic 项目的 WebAPI 项目

部分源码涉及到版权原因没有公开，如 音频格式转换 等，但是这部分代码是可选的，如果项目中报错，请根据代码中的注释删去这部分上下文即可，当然，失去这部分代码局部功能会受影响。 



## 关于 EFCore 迁移

请使用以下命令创建迁移 `shell`:

限定根目录，请确保在项目目录下，而不是解决方案目录

~~~shell
cd ./MyMusicWebAPI/MyMusicWebAPI
~~~

在创建迁移时需要指定目录

~~~shell
dotnet ef migrations add InitialCreate --output-dir EFService/ModelSnapshots
~~~

应用迁移

~~~shell
dotnet ef database update
~~~

