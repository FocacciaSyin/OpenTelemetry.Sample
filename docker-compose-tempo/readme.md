# OpenTelemetry Sample with Grafana Stack

這是一個使用 OpenTelemetry 進行應用程式觀測 (Observability) 的範例專案。整合了 Metrics (指標)、Tracing (追蹤) 與 Logging (日誌) 的蒐集與視覺化。

## 專案架構

本專案使用 Docker Compose 部署以下服務，構建一個完整的觀測平台：

### 核心服務
*   **應用程式 (Applications)**:
    *   `webapp1` & `webapp2`: 範例 ASP.NET Core 應用程式，負責產生遙測數據。
*   **觀測後端 (Observability Backends)**:
    *   **Tempo (Tracing)**: Grafana Labs 開發的分佈式追蹤後端。負責接收並儲存來自應用程式的 Trace 資料。
    *   **Loki (Logging)**: Grafana Labs 開發的日誌聚合系統。負責儲存日誌資料。
    *   **Prometheus (Metrics)**: 負責定期抓取 (Scrape) 應用程式與基礎設施的指標數據。
*   **視覺化 (Visualization)**:
    *   **Grafana**: 統一的監控儀表板介面。已預先透過 `grafana-datasources.yaml` 設定好 Prometheus, Tempo, Loki 的資料來源。

## 快速開始

### 前置需求
*   Docker Desktop 或 Docker Engine & Docker Compose
*   **注意**: `docker-compose.yaml` 中參照了 `localhost/webapp1-release:latest` 與 `localhost/webapp2-release:latest` 映像檔。請確保您本機已建立這些映像檔，或修改 `docker-compose.yaml` 以使用正確的映像檔來源。

### 啟動服務

在專案根目錄執行以下指令：

```bash
docker-compose up -d
```

### 訪問服務

服務啟動後，您可以透過瀏覽器訪問以下端口：

| 服務 | URL | 帳號 / 備註 |
| --- | --- | --- |
| **Grafana** | [http://localhost:3000](http://localhost:3000) | 帳號: `admin`<br>密碼: `pass.123` (參見 `admin_password` 檔案) |
| **Prometheus** | [http://localhost:9090](http://localhost:9090) | 查看 Targets 狀態與查詢指標 |
| **Tempo** | [http://localhost:3200](http://localhost:3200) | 追蹤後端 API |
| **Loki** | [http://localhost:3100](http://localhost:3100) | 日誌後端 API |
| **WebApp 1** | [http://localhost:5132](http://localhost:5132) | 測試網站 1 |
| **WebApp 2** | [http://localhost:5133](http://localhost:5133) | 測試網站 2 |

## 設定檔說明

各個設定檔的用途如下：

*   **`docker-compose.yaml`**: 定義整個監控堆疊的容器編排。
*   **`prometheus.yaml`**: Prometheus 的設定檔。
    *   設定了抓取任務 (Scrape Jobs)，包含監控自身、Tempo 以及兩個 WebApp。
*   **`tempo.yaml`**: Tempo 的設定檔。
    *   啟用 OTLP Receiver (gRPC: 4317, HTTP: 4318) 以接收 Trace 資料。
    *   設定 Local Storage 將資料儲存在 `./tempo-data`。
    *   設定 Metrics Generator 將 Trace 轉換為指標並寫入 Prometheus。
*   **`grafana-datasources.yaml`**: Grafana 的 Provisioning 設定。
    *   自動加入 Prometheus, Tempo, Loki 為資料來源，無需手動設定。
*   **`otel-collector-config.yaml`**: OpenTelemetry Collector 的設定檔。
    *   *注意*: 目前 `docker-compose.yaml` 中尚未包含獨立的 Collector 服務，此檔案可能是為未來擴充或獨立運作所保留。目前的架構可能是讓 App 直接發送數據到 Tempo/Loki/Prometheus。

## 資料流向 (Data Flow)

1.  **Metrics**: `webapp1` 和 `webapp2` 暴露指標端點 (如 `/metrics`)，Prometheus 根據設定定期 Pull (拉取) 資料。
2.  **Tracing**: 應用程式透過 OTLP 協議 (gRPC/HTTP) 直接將 Trace 資料發送到 **Tempo** (Ports 4317/4318)。
3.  **Logs**: 應用程式將 Log 發送到 **Loki** (通常透過 Promtail 或 OTLP Log Exporter，視 App 實作而定)。
4.  **Grafana**: 作為統一介面，向 Prometheus 查詢指標、向 Tempo 查詢追蹤鏈路、向 Loki 查詢日誌，並將其關聯展示。
