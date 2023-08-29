#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CommentManagementService.WebApi/CommentManagementService.WebApi.csproj", "CommentManagementService.WebApi/"]
COPY ["src/Architecture/Crosscutting/EmpCore.Crosscutting.DistributedCache/EmpCore.Crosscutting.DistributedCache.csproj", "Architecture/Crosscutting/EmpCore.Crosscutting.DistributedCache/"]
COPY ["src/Architecture/Infrastructure/EmpCore.Infrastructure.MessageBus/EmpCore.Infrastructure.MessageBus.CAP.csproj", "Architecture/Infrastructure/EmpCore.Infrastructure.MessageBus/"]
COPY ["src/Architecture/Infrastructure/EmpCore.Infrastructure/EmpCore.Infrastructure.csproj", "Architecture/Infrastructure/EmpCore.Infrastructure/"]
COPY ["src/Architecture/Domain/EmpCore.Domain/EmpCore.Domain.csproj", "Architecture/Domain/EmpCore.Domain/"]
COPY ["src/Architecture/Presentation/EmpCore.Api/EmpCore.Api.csproj", "Architecture/Presentation/EmpCore.Api/"]
COPY ["src/Architecture/Presentation/EmpCore.WebApi.Swagger/EmpCore.WebApi.Swagger.csproj", "Architecture/Presentation/EmpCore.WebApi.Swagger/"]
COPY ["src/CommentManagementService.Application/CommentManagementService.Application.csproj", "CommentManagementService.Application/"]
COPY ["src/Architecture/Application/EmpCore.Application/EmpCore.Application.csproj", "Architecture/Application/EmpCore.Application/"]
COPY ["src/Architecture/QueryStack/EmpCore.QueryStack.Dapper/EmpCore.QueryStack.Dapper.csproj", "Architecture/QueryStack/EmpCore.QueryStack.Dapper/"]
COPY ["src/CommentManagementService.Domain/CommentManagementService.Domain.csproj", "CommentManagementService.Domain/"]
COPY ["src/CommentManagementService.Persistence/CommentManagementService.Persistence.csproj", "CommentManagementService.Persistence/"]
COPY ["src/Architecture/Infrastructure/EmpCore.Persistence.EntityFrameworkCore/EmpCore.Persistence.EntityFrameworkCore.csproj", "Architecture/Infrastructure/EmpCore.Persistence.EntityFrameworkCore/"]
COPY ["src/Libs/BlogPostManagementService.Contracts.dll", "Libs/"]
RUN dotnet restore "CommentManagementService.WebApi/CommentManagementService.WebApi.csproj"
COPY . .
WORKDIR "/src/CommentManagementService.WebApi"
RUN dotnet build "CommentManagementService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommentManagementService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommentManagementService.WebApi.dll"]