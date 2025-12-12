# ProcessMonitor.ConsoleApp

A simple console client for interacting with the **ProcessMonitor.API**. Allows submitting analysis requests, retrieving history, and viewing aggregated summary statistics.

---

## Prerequisites

* Access to a running **ProcessMonitor.API** instance

  * Docker deployment (`http://localhost:8080/v1/processmonitor/...`)
  * Or traditional deployment (`https://localhost:7023/v1/processmonitor/...`)
* Environment variable `ApiKey` must be set to match the API key configured in the API.

```powershell
# Windows PowerShell
$env:ApiKey="testsecret"

# Linux / macOS
export ApiKey="testsecret"
```

---

## Getting Started

1. **Clone the repository**

```bash
git clone https://github.com/ivanadal/ProcessMonitor.App
cd ProcessMonitor.App
```

2. **Build the console app**

```bash
dotnet build
```

3. **Run the console app**

```bash
dotnet run
```

---

## Features

Once the app starts, you will see a menu:

```
1) Analyze
2) Get History
3) Get Summary
0) Exit
```

* **Analyze:** Submit a new action and guideline for evaluation.
* **Get History:** Paginated list of previous analyses.
* **Get Summary:** Aggregated statistics over all stored analyses.

---

## Configuration

The console app uses **different URLs depending on deployment**:

* **Docker:**

```csharp
http://localhost:8080/v1/processmonitor/analyze
http://localhost:8080/v1/processmonitor/history
http://localhost:8080/v1/processmonitor/summary
```

* **Traditional / local:**

```csharp
https://localhost:7023/v1/processmonitor/analyze
https://localhost:7023/v1/processmonitor/history
https://localhost:7023/v1/processmonitor/summary
```

Make sure the `ApiKey` environment variable matches the API key expected by the API (`X-Api-Key` header).

---

## Notes

* This console app is intended for **testing and demonstration**.
* In production scenarios, API authentication would normally use **OAuth2** or **JWT tokens** rather than a static API key.
* All output is printed to the console in JSON format for simplicity.
