# ProcessMonitor.ConsoleApp

A simple console client for interacting with the **ProcessMonitor.API**. Allows submitting analysis requests, retrieving history, and viewing aggregated summary statistics.

---

## Prerequisites

* Access to a running **ProcessMonitor.API** instance

  * Docker deployment (`http://localhost:8080/v1/processmonitor/...`)
  * Or traditional deployment (`https://localhost:7023/v1/processmonitor/...`)
* Environment variable `ApiKey` must be set to match the API key configured in the API.
* Environment variable for DOCKER deployment RUN_ENV=docker, if it is traditional then nothing should be added in environment variables.

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
2) History
3) Summary
0) Exit
```

* **Analyze:** Submit a new action and guideline for evaluation.
* **History:** Paginated list of previous analyses.
* **Summary:** Aggregated statistics over all stored analyses.

---

## Configuration

The console app uses **different URLs depending on deployment**:

This is defined within ApiConfig, and can be changed there. If DOCKER is used, it is expected to set environment variable like this: RUN_ENV=docker, if it is traditional then nothing should be added in environment variables.

* **Docker:**

```csharp
http://localhost:8080/v1/processmonitor/
```

* **Traditional / local:**

```csharp
https://localhost:7023/v1/processmonitor/
```

Make sure the `ApiKey` environment variable matches the API key expected by the API (`X-Api-Key` header).

---

## Notes

* This console app is intended for **testing and demonstration**.
* In production scenarios, API authentication would normally use **OAuth2** or **JWT tokens** rather than a static API key.
* All output is printed to the console in JSON format for simplicity.
