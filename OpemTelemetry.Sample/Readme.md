# 專案說明

> WebApplication1 = 入口 WebAPI
>
> WebApplication2 = 整理資料 WebAPI 所以包含 Service & Repository



# 首次使用

注意 Docker 訪問內部其他 Container 是使用
http://host.docker.internal:5132


Podman 實作後失敗 先改使用 docker
Podman 是使用
http://host.containers.internal:5132
```
cd 到 sln 路徑

podman build -f .\WebApplication1\Dockerfile -t webapplication1:latest .
podman build -f .\WebApplication2\Dockerfile -t webapplication2:latest .

podman run -d --name webapplication1 --env ASPNETCORE_ENVIRONMENT=Release -p 5132:8080 webapplication1:latest 
podman run -d --name webapplication2 --env ASPNETCORE_ENVIRONMENT=Release -p 5133:8080 webapplication2:latest 
```

# Grafana

透過 VSCode 可以看到  Containers 內的結構 Files\etc\promtail\config.yml 有設定檔。
![image-20241218173401408](Images\docker.png)

rider > Services > Podman > Container > 右鍵 > Show Files 也可以看到




