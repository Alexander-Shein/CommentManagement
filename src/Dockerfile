#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CommentManagementService.WebApi/CommentManagementService.WebApi.csproj", "CommentManagementService.WebApi/"]
COPY ["Architecture/Crosscutting/EmpCore.Crosscutting.DistributedCache/EmpCore.Crosscutting.DistributedCache.csproj", "Architecture/Crosscutting/EmpCore.Crosscutting.DistributedCache/"]
COPY ["Architecture/Infrastructure/EmpCore.Infrastructure.MessageBus/EmpCore.Infrastructure.MessageBus.CAP.csproj", "Architecture/Infrastructure/EmpCore.Infrastructure.MessageBus/"]
COPY ["Architecture/Infrastructure/EmpCore.Infrastructure/EmpCore.Infrastructure.csproj", "Architecture/Infrastructure/EmpCore.Infrastructure/"]
COPY ["Architecture/Domain/EmpCore.Domain/EmpCore.Domain.csproj", "Architecture/Domain/EmpCore.Domain/"]
COPY ["Architecture/Presentation/EmpCore.Api/EmpCore.Api.csproj", "Architecture/Presentation/EmpCore.Api/"]
COPY ["Architecture/Presentation/EmpCore.WebApi.Swagger/EmpCore.WebApi.Swagger.csproj", "Architecture/Presentation/EmpCore.WebApi.Swagger/"]
COPY ["CommentManagementService.Application/CommentManagementService.Application.csproj", "CommentManagementService.Application/"]
COPY ["Architecture/Application/EmpCore.Application/EmpCore.Application.csproj", "Architecture/Application/EmpCore.Application/"]
COPY ["Architecture/QueryStack/EmpCore.QueryStack.Dapper/EmpCore.QueryStack.Dapper.csproj", "Architecture/QueryStack/EmpCore.QueryStack.Dapper/"]
COPY ["CommentManagementService.Domain/CommentManagementService.Domain.csproj", "CommentManagementService.Domain/"]
COPY ["CommentManagementService.Persistence/CommentManagementService.Persistence.csproj", "CommentManagementService.Persistence/"]
COPY ["Architecture/Infrastructure/EmpCore.Persistence.EntityFrameworkCore/EmpCore.Persistence.EntityFrameworkCore.csproj", "Architecture/Infrastructure/EmpCore.Persistence.EntityFrameworkCore/"]
COPY ["CommentManagementService.Application/BlogPostManagementService.Contracts.dll", "CommentManagementService.Application/"]
RUN dotnet restore "CommentManagementService.WebApi/CommentManagementService.WebApi.csproj"
COPY . .
WORKDIR "/src/CommentManagementService.WebApi"
RUN dotnet build "CommentManagementService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommentManagementService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "CommentManagementService.WebApi.dll"]
