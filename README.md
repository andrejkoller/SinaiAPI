## Short description

SinaiAPI is a RESTful API built with ASP.NET Core (.NET 9, C# 13) for managing departments, workplaces, reservations, users, and guides within an organizational environment. The API supports authentication, real-time updates, and flexible resource management.

## Features

- **Department & Workplace Management:**  
  Create, update, and manage departments and their associated workplaces, including status tracking (Available, Reserved, Blocked).

- **Reservation System:**  
  Reserve workplaces, track reservation status, and manage user bookings.

- **User Authentication:**  
  Secure registration and login endpoints with JWT-based authentication.

- **Guide Management:**  
  CRUD operations for guides associated with departments or workplaces.

- **Real-Time Updates:**  
  SignalR hub for broadcasting reservation changes.

## Tech Stack

- **.NET 9 / C# 13**
- **Entity Framework Core** (with migrations)
- **ASP.NET Core Web API**
- **SignalR** (for real-time communication)
- **System.Text.Json** (with enum string converters)

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server or compatible database

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/andrejkoller/SinaiAPI.git
cd SinaiAPI
```

### 2. Configure the database connection

Edit the connection string in `appsettings.json` or `appsettings.Development.json`:

```bash
"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=SinaiDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Apply database migration

Make sure the Entity Framework Core CLI is installed:

 ```bash
dotnet tool install --global dotnet-ef
```

Then apply the migrations:

 ```bash
dotnet ef database update
```

### 4. Run the API

 ```bash
dotnet run --project SinaiAPI
```

