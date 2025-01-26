# otelcol-observability-demo

This project demonstrates the implementation of observability using OpenTelemetry Collector (otelcol), Loki, Prometheus, and Tempo.

## Project Structure

- **observability-config/**: Contains configurations for observability tools, centered around OpenTelemetry Collector.
- **weather-forecast/**: A sample application for weather forecasting used to generate observable data.
- **docker-compose.yml**: File for orchestrating Docker containers for the application and observability tools.

## Architecture Overview

### Core Components

- **OpenTelemetry Collector (otelcol)**:
   - Central hub for collecting telemetry data (logs, metrics, traces) from the application.
   - Configured to export data to Prometheus, Loki, and Tempo.

- **Prometheus**:
   - Collects and stores metrics from otelcol.

- **Loki**:
   - Aggregates and stores logs forwarded by otelcol.

- **Tempo**:
   - Handles traces collected by otelcol.

- **Grafana**:
   - Provides a unified dashboard for visualizing metrics, logs, and traces.

### Data Flow

1. **Application to otelcol**:
   - The `weather-forecast` app sends logs, metrics, and traces to otelcol via OTLP.

2. **otelcol to Observability Tools**:
   - Logs → Loki
   - Metrics → Prometheus
   - Traces → Tempo

3. **Grafana**:
   - Queries Prometheus, Loki, and Tempo for visualization.

## Prerequisites

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Getting Started

1. **Clone the repository**:

   ```bash
   git clone https://github.com/lucalamalfa91/otelcol-observability-demo.git
   cd otelcol-observability-demo
   ```

2. **Start the services**:

   ```bash
   docker-compose up -d
   ```

   This command will start all services defined in `docker-compose.yml`.

3. **Check the status of the services**:

   ```bash
   docker-compose ps
   ```

   Ensure all containers are running correctly.

## Accessing Observability Tools

- **Prometheus**: [http://localhost:9090](http://localhost:9090)
- **Grafana**: [http://localhost:3000](http://localhost:3000)
- **Loki**: [http://localhost:3100](http://localhost:3100)
- **Tempo**: [http://localhost:3200](http://localhost:3200)

> **Note**: The above ports are default. If you modified configurations, adjust accordingly.

## Configuring Grafana

1. **Log in to Grafana** using the default credentials:
   - **Username**: `admin`
   - **Password**: `admin`

2. **Add data sources**:
   - **Prometheus**:
      - URL: `http://prometheus:9090`
   - **Loki**:
      - URL: `http://loki:3100`
   - **Tempo**:
      - URL: `http://tempo:3200`

3. **Import dashboards**:
   - Go to `Create` > `Import` and enter the desired dashboard ID or upload a JSON file.

## Cleanup

To stop and remove all services:

```bash
docker-compose down
```

## Tool Documentation

- [OpenTelemetry Collector Contrib](https://github.com/open-telemetry/opentelemetry-collector-contrib)
- [Grafana](https://grafana.com/docs/)
- [Tempo](https://grafana.com/docs/tempo/latest/)
- [Loki](https://grafana.com/docs/loki/latest/)
- [Prometheus](https://prometheus.io/docs/)

## Contributions

Contributions are welcome! Feel free to open issues or pull requests to improve this project.
