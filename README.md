# MyMusicWebAPI

这是 MyMusic 项目的 WebAPI 项目，以下是主要的项目框架和技术。

`ASP.NET Core 6.0` `C#10` `EFCore7` `SQLServer` `Jwt` `RSA`

部分源码涉及到版权原因没有公开，如 音频格式转换 等，但是这部分代码是可选的，如果项目中报错，请根据代码中的注释删去这部分上下文即可，当然，失去这部分代码局部功能会受影响。 

目前使用 EFCore ORM ，后续依赖分离预计维护两套 EFCore & Dapper （当然，在未来1个月内应该不会实现，EFCore 将作为主力框架使用）

得益于 Asp.NET 的独立服务器 Kestrel，目前 静态文件 和 Web 服务器都由此项目提供服务，这些文件统一在 `wwwRoot` 目录下，后续应该会独立出来，现阶段是如此。



## 关于接口规范

目标是遵循 RESTful API 规范，但是现在，现阶段只有部分接口满足了这个规范，后续迭代会逐渐完善，也就是说此项目实现这个规范的过程是渐进性的（好吧，其实我对 RESTful  的理解也是渐进性的）



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

如果是第一次创建迁移，可能需要手动创建一个数据库，请在 `appsettings.json` 中配置数据库相关信息。

项目开发时使用的是 SQL Server 2019，一般情况下 SQL Server 2012 以上的版本应该都是支持的。
