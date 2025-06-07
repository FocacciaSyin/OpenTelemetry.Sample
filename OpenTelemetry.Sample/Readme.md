# 專案說明

> WebApplication1 = 入口 WebAPI
>
> WebApplication2 = 整理資料 WebAPI 所以包含 Service & Repository

# 首次使用

注意 Docker 訪問內部其他 Container 是使用
http://host.docker.internal:5132





再改一下確定可以成功

> 有安裝Slowcheetcj 要使用 BUILD_CONFIGURATION=Release

Podman 實作後失敗 先改使用 docker
Podman 是使用
http://host.containers.internal:5132



```
cd 到 sln 路徑

podman build -f .\WebApplication1\Dockerfile -t webapp1-release:latest --build-arg BUILD_CONFIGURATION=Release .
podman build -f .\WebApplication2\Dockerfile -t webapp1-release:latest --build-arg BUILD_CONFIGURATION=Release .

用原生的方式
podman run -d --name webapp1-release --env ASPNETCORE_ENVIRONMENT=Release -p 5132:8080 webapplication1-release:latest
podman run -d --name webapp2-release --env ASPNETCORE_ENVIRONMENT=Release -p 5133:8080 webapplication2-release:latest

用SlowCheetch的方式 
podman run -d --name webapp1 -p 5132:8080 webapplication1-release:latest --network monitoring
podman run -d --name webapp2 -p 5133:8080 webapplication2-release:latest --network monitoring

podman run -d --name webapp1-test -e ASPNETCORE_ENVIRONMENT=Release -p 5199:8080 webapp1-release:latest --network monitoring



```

# Grafana

透過 VSCode 可以看到 Containers 內的結構 Files\etc\promtail\config.yml 有設定檔。
![image-20241218173401408](Images\docker.png)

rider > Services > Podman > Container > 右鍵 > Show Files 也可以看到



