# SID2 — School Information Database (Web Rebuild)

A CSC-450 group project built for **Palen Music Center**. SID2 modernizes their internal School Information Database (SID) — a tool used to track school visits, EdRep assignments, repairs, and accounting data — into a full-stack web application.

## What It Does

Palen Music Center staff log in and are routed to a role-specific dashboard based on their account type. Each role sees only the tools relevant to their job:

| Role | Access |
|------|--------|
| **Admin** | Manage users, schools, and stores; view all EdRep visits for the day |
| **EdRep** | View their own scheduled school visits for the day |
| **Manager** | View all visits for EdReps assigned to their store |
| **Accountant** | VPU tracking and repo management per school |
| **Repair Tech** | View open/unfinished repair tickets |

## Tech Stack

| Layer | Technology |
|-------|------------|
| Frontend | React 19 + Vite |
| Backend API | ASP.NET Core (.NET 10) |
| Database | SQL Server (Entity Framework Core) |
| Auth | Username/password with Argon2 password hashing; token returned on login |

## Project Structure

```
src/
  api/SidApi/         # ASP.NET Core Web API
    Controllers/      # Auth, User, School, Store endpoints
    Data/             # EF Core DbContext and entities
    Models/           # Request/response models
    Security/         # Password hashing (Argon2)
  web/sid2-web/       # React frontend (Vite)
    src/components/   # Login, Home, AdminHome, ManageUsers, SidePanel
  database/           # SQL schema (sid2.sql)
docs/
  backlog.md          # Full feature backlog
  sprint1.md          # Sprint 1 scope
```

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/api/auth/login` | Authenticate user; returns token + user info |
| `POST` | `/api/user` | Create a new user (Admin) |
| `GET` | `/api/user/GetUsers` | List all users |
| `POST` | `/api/school` | Create a new school |
| `POST` | `/api/store` | Create a new store |

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18+)
- SQL Server instance

### Database

Run the schema script against your SQL Server instance:

```bash
sqlcmd -S <server> -i src/database/sid2.sql
```

### API

Update the connection string in `src/api/SidApi/appsettings.json`, then:

```bash
cd src/api/SidApi
dotnet run
```

The API runs on `https://localhost:7000` by default and exposes a Swagger UI at `/swagger`.

### Frontend

```bash
cd src/web/sid2-web
npm install
npm run dev
```

The dev server runs on `http://localhost:5173`.

## User Types

Accounts are assigned one of five `userType` values: `Admin`, `EdRep`, `Accountant`, `Repair`, `Manager`. Login validates credentials and checks that the account's `active` flag is set to `true` before issuing a session token.
