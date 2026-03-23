# 降级到 .NET 8.0 方案

## 原因
当前系统安装的是 .NET 8.0 SDK，但项目要求 .NET 10.0。如果无法安装 .NET 10.0，可以降级项目到 .NET 8.0。

## 需要修改的文件

### 1. 项目文件 (`src/MinsuXize.Web/MinsuXize.Web.csproj`)
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>  <!-- 修改为 net8.0 -->
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />  <!-- 降级版本 -->
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

</Project>
```

### 2. 全局配置文件 (如果有)
检查 `global.json` 文件，确保目标版本匹配。

### 3. Dockerfile (如果有)
如果使用 Docker，更新基础镜像标签。

## 降级步骤

### 步骤 1：备份当前项目
```bash
cp -r minsuxize minsuxize-backup-net10
```

### 步骤 2：修改项目文件
```bash
# 使用 sed 命令修改
sed -i 's/net10.0/net8.0/g' src/MinsuXize.Web/MinsuXize.Web.csproj
sed -i 's/Version="10.0.5"/Version="8.0.0"/g' src/MinsuXize.Web/MinsuXize.Web.csproj
```

### 步骤 3：清理并重新构建
```bash
# 清理构建缓存
dotnet clean

# 删除 bin 和 obj 目录
rm -rf src/MinsuXize.Web/bin
rm -rf src/MinsuXize.Web/obj

# 恢复 NuGet 包
dotnet restore

# 重新构建
dotnet build src/MinsuXize.Web/MinsuXize.Web.csproj
```

### 步骤 4：运行项目
```bash
dotnet run --project src/MinsuXize.Web/MinsuXize.Web.csproj
```

## 可能的问题

### 1. API 兼容性
.NET 10.0 可能有一些新的 API，在 .NET 8.0 中不可用。需要检查：
- 项目是否使用了 .NET 10.0 特有的 API
- Entity Framework Core 10.0 特性是否兼容 8.0

### 2. 包依赖
检查所有 NuGet 包是否支持 .NET 8.0：
```bash
dotnet list package
```

### 3. 运行时特性
如果项目使用了 .NET 10.0 的新运行时特性，可能需要调整代码。

## 验证步骤

### 1. 构建成功
```bash
dotnet build --no-restore
```

### 2. 运行成功
```bash
dotnet run --project src/MinsuXize.Web/MinsuXize.Web.csproj
```

### 3. 功能测试
- 访问首页
- 测试管理员登录
- 验证数据库连接

## 回滚方案
如果降级后出现问题，可以恢复备份：
```bash
rm -rf minsuxize
cp -r minsuxize-backup-net10 minsuxize
```

## 建议
1. 优先尝试安装 .NET 10.0 SDK
2. 如果无法安装，再使用此降级方案
3. 降级后需要充分测试所有功能