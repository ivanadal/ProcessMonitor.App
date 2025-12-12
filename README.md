# ProcessMonitor.ConsoleApp

A simple console client for interacting with the **ProcessMonitor.API**. Allows submitting analysis requests, retrieving history, and viewing aggregated summary statistics.

---

## Prerequisites

* Access to a running **ProcessMonitor.API** instance

  * Docker deployment (`http://localhost:8080/v1/processmonitor/...`)
  * Or traditional deployment (`https://localhost:7023/v1/processmonitor/...`)
* Set up Environment Variables ([Guide How to Set](https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/set_1)):
	* `ApiKey` must be set to match the ProcessMonitorAPI key configured in the ProcessMonitorAPI.
	* Environment variable for DOCKER deployment `RUN_ENV` should be set to docker, if it is traditional then nothing should be added in environment variables.

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

This is defined within ApiConfig, and can be changed there.  

* **Docker:**

```csharp
http://localhost:8080/v1/processmonitor/
```

* **Traditional / local:**

```csharp
https://localhost:7023/v1/processmonitor/
```

---

## Notes

* This console app is intended for **testing and demonstration**.
* In production scenarios, API authentication would normally use **OAuth2** or **JWT tokens** rather than a static API key.
* All output is printed to the console in JSON format for simplicity.
