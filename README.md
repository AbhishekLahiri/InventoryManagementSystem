# Inventory Management System

A modern full-stack Inventory Management System built with **Blazor WebAssembly**, **ASP.NET Core Web API**, and **Entity Framework Core**.

The application provides a scalable architecture for managing inventory items, categories, and stock information through a clean separation between frontend, backend, and shared domain contracts.

---

## Overview

This project demonstrates enterprise-grade application design using the Microsoft .NET ecosystem.

Key goals include:

- Inventory item management
- Category management
- RESTful API architecture
- Clean separation of concerns
- DTO-based communication
- Testable and maintainable code structure
- Blazor WebAssembly client experience

---

## Architecture

The solution follows a layered architecture:

```text
┌──────────────────────────┐
│     Blazor WebAssembly   │
│          Frontend        │
└────────────┬─────────────┘
             │ HTTP
             ▼
┌──────────────────────────┐
│    ASP.NET Core Web API  │
│         Backend          │
└────────────┬─────────────┘
             │ EF Core
             ▼
┌──────────────────────────┐
│        Database          │
│ SQL Server / SQLite      │
└──────────────────────────┘
```

---

## Solution Structure

```text
InventoryManagementSystem
│
├── InventoryManagementSystem.Api
│   ├── Controllers
│   ├── Services
│   ├── Data
│   └── Configuration
│
├── InventoryManagementSystem.UI
│   ├── Pages
│   ├── Components
│   ├── Services
│   └── wwwroot
│
├── InventoryManagementSystem.Shared
│   ├── DTOs
│   ├── Models
│   └── Contracts
│
├── InventoryManagementSystem.Tests
│   └── Unit Tests
│
└── InventoryManagementSystem.slnx
```

### Project Responsibilities

| Project | Purpose |
|----------|----------|
| InventoryManagementSystem.Api | Backend REST API |
| InventoryManagementSystem.UI | Blazor WebAssembly frontend |
| InventoryManagementSystem.Shared | Shared DTOs and models |
| InventoryManagementSystem.Tests | Unit and integration testing |

---

## Technology Stack

### Frontend

- Blazor WebAssembly
- Bootstrap 5
- C#

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- RESTful Services

### Database

- SQL Server
- SQLite (development/testing)

### Development

- .NET Core
- C# 14
- Visual Studio 2026

---

## Features

### Inventory Management

- Create inventory items
- Update inventory details
- Delete inventory items
- View inventory records
- Search inventory data

### Category Management

- Create categories
- Update categories
- Delete categories
- Associate items with categories

### API Features

- RESTful endpoint design
- DTO-based request/response contracts
- Strong typing
- Validation support

### UI Features

- Responsive layout
- Component-based architecture
- Client-side rendering
- API integration

---

## API Design

Example endpoint structure:

```http
GET    /api/inventory/items
GET    /api/inventory/items/{id}

POST   /api/inventory/items
PUT    /api/inventory/items/{id}
DELETE /api/inventory/items/{id}

GET    /api/inventory/categories
POST   /api/inventory/categories
```

---

## Data Transfer Objects (DTOs)

The application uses DTOs to:

- Separate domain models from API contracts
- Reduce payload size
- Improve maintainability
- Prevent over-posting
- Improve security

---

## Getting Started

### Prerequisites

Install:

- .NET SDK (latest stable version)
- Visual Studio 2026 or VS Code
- SQL Server (optional)
- Git

---

## Clone the Repository

```bash
git clone https://github.com/AbhishekLahiri/InventoryManagementSystem.git
cd InventoryManagementSystem
```

---

## Running the Backend

Navigate to the API project:

```bash
cd InventoryManagementSystem.Api
```

Restore packages:

```bash
dotnet restore
```

Run the API:

```bash
dotnet run
```

The API will start on the configured local URL.

---

## Running the Frontend

Open a second terminal:

```bash
cd InventoryManagementSystem.UI
```

Restore packages:

```bash
dotnet restore
```

Run the Blazor application:

```bash
dotnet run
```

Open the browser and navigate to the URL displayed in the terminal.

---

## Running Tests

Navigate to the solution root and execute:

```bash
dotnet test
```

This will run all tests contained in:

```text
InventoryManagementSystem.Tests
```

---

## Development Principles

This project follows:

- Separation of Concerns (SoC)
- REST API Best Practices
- Dependency Injection
- DTO Pattern
- Clean Architecture Concepts
- Reusable Shared Contracts
- Test-Driven Development Friendly Structure

---

## Future Enhancements

Potential improvements:

- Authentication & Authorization
- JWT Security
- Role-Based Access Control
- Inventory Analytics Dashboard
- Audit Logging
- Stock Alerts
- Barcode Scanning
- Pagination & Filtering
- Docker Support
- CI/CD Pipelines
- Cloud Deployment

---

## Author

**Abhishek Lahiri**

GitHub:
https://github.com/AbhishekLahiri

---
