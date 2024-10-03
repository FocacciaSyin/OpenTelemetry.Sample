# PrometheusWebApi


## 參考文章

[使用 Docker Compose 啟動 Grafana Tempo - Yowko's Notes](https://blog.yowko.com/docker-compose-grafana-tempo/)

[將 ASP.NET 的 Trace 整合至 Grafana Tempo - Yowko's Notes](https://blog.yowko.com/aspdotnet-tempo/)

[Example: Use OpenTelemetry with Prometheus, Grafana, and Jaeger](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-prgrja-example)

## 開始實作

> 目標：建立一個 ASP.NET WEBAPI 產出 OpenTelemetry
> 1. 使用 Prometheus 作為監控(Metrics)
> 2. 使用 Grafana 作為監控視覺化
> 3. 使用 Tempo 作為分散式追蹤(Tracing)


1. 安裝環境
```
cd Docker
docker-compose -f docker-compose-tempo.yaml up -d
```

2. 確認 Grafana正常運作
```
http://localhost:3000/

帳密
admin
pass.123
```



## Grafana

Dashboard 

[ASP.NET OTEL Metrics | Grafana Labs](https://grafana.com/grafana/dashboards/17706-asp-net-otel-metrics/)

[OpenTelemetry dotnet webapi | Grafana Labs](https://grafana.com/grafana/dashboards/20568-opentelemetry-dotnet-webapi/)

目前 dotnet 8 可以正常顯示的版本 [aspire/src/Grafana/dashboards at main · dotnet/aspire (github.com)](https://github.com/dotnet/aspire/tree/main/src/Grafana/dashboards)



![image-20241003144646514](Images\docker_1.png)

