# Inventory Management System

A full-stack, enterprise-ready inventory tracking application built using **Blazor WebAssembly** on the frontend and an **ASP.NET Core Web API** backend, powered by **Entity Framework Core**.

---

## 🚀 Architecture Highlights

* **Decoupled Architecture:** Clean separation of concerns between client (Blazor UI) and server (REST API).
* **Domain-Driven RESTful Routing:** Organized hierarchical endpoints (`api/inventory/items` and `api/inventory/categories`) for predictable client interaction and clean domain scaling.
* **Data Transfer Objects (DTOs):** Segregated Read/Write models (`UpdateItemDto`, `InventoryItemDto`) to enforce explicit network payloads and optimize database query performance by eliminating unnecessary data round-trips.

---

## 🛠️ Technology Stack

* **Frontend:** Blazor WebAssembly, Bootstrap 5
* **Backend:** ASP.NET Core Web API, EF Core, SQL Server / SQLite
* **Language & Runtime:** C# 12, .NET

---

## 💻 Getting Started

### Prerequisites
* .NET SDK (Latest version)
* Visual Studio 2022 / VS Code

### Running the Application

1. **Clone the repository:**
   git clone <your-repository-url>

2. **Navigate to the API backend project directory and run it:**
   cd InventoryManagementSystem.Api
   dotnet run

3. **Open a second terminal, navigate to the Blazor Client project directory, and start the frontend:**
   cd InventoryManagementSystem.UI
   dotnet run