# Qiskit IDE

A full-stack quantum computing learning platform built with ASP.NET Core 8, featuring a browser-based Qiskit compiler, structured courses, and an integrated AI assistant powered by LLamaSharp (Llama 2).

---

## Features

- **Online Qiskit Compiler** — Write and execute Python/Qiskit code directly in the browser using the Ace editor. Code runs server-side via a sandboxed Python subprocess.
- **AI Assistant** — An integrated LLM chat panel (Llama 2 via LLamaSharp) assists with quantum questions, code explanations, and debugging in real time.
- **Course System** — Admins can create structured courses with rich HTML content. Authenticated users can enroll and track progress.
- **Role-based Access** — Two roles (`Admin`, `User`) enforced via ASP.NET Core Identity. Admins can create courses; unauthenticated users can still access the compiler.
- **Modern UI** — Dark quantum-themed interface with animated gradient accents, bento grid layout, and full mobile responsiveness.
- **Error Handling** — Styled error pages for all HTTP status codes (401, 403, 404, 500+) with contextual messages.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core 8 Razor Pages |
| ORM | Entity Framework Core + SQL Server (LocalDB in dev) |
| Auth | ASP.NET Core Identity + Roles |
| Code editor | Ace Editor (browser) |
| AI model | LLamaSharp — Llama 2 (GGUF) |
| Frontend build | Webpack + Babel + React (for the course create form) |
| Styling | Custom CSS design system (no CSS framework for UI) + Bootstrap 5 grid |

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (included with Visual Studio)
- [Python 3.x](https://www.python.org/downloads/) with `qiskit` and `qiskit-aer` installed
- `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`
- Node.js (optional — only needed if rebuilding the webpack bundle)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mhlandr/Qiskit-IDE.git
cd Qiskit-IDE
```

### 2. Apply database migrations

```bash
cd webProject
dotnet ef database update
```

This creates the LocalDB database and seeds the default Admin account.

### 3. Install Python dependencies

```bash
pip install qiskit qiskit-aer matplotlib
```

### 4. Run the app

```bash
dotnet run
```

Or open `webProject.sln` in Visual Studio and press F5.

The app starts at `https://localhost:57317`. It redirects `/` to the compiler.

---

## Default Accounts

After running migrations, the seed creates one account automatically:

| Role | Email | Password |
|------|-------|----------|
| Admin | `admin12@example.com` | `Adminn12@123` |

Register additional users via the `/Identity/Account/Register` page. New registrations receive the `User` role.

---

## Project Structure

```
webProject/
├── Areas/
│   └── Identity/Pages/Account/   # Login, Register (custom-styled)
├── Data/
│   ├── ApplicationDbContext.cs    # EF Core DB context
│   ├── ApplicationUser.cs         # Extended Identity user model
│   ├── Migrations/                # EF Core migrations
│   └── SeedData.cs                # Admin seed + role creation
├── Models/
│   ├── Course.cs
│   ├── UserCourse.cs              # Enrollment join table
│   └── UserCourseProgress.cs
├── Pages/
│   ├── compile.cshtml             # IDE page (Ace editor + AI chat)
│   ├── Index.cshtml               # Landing page (bento layout)
│   ├── Error.cshtml               # Catch-all error page
│   ├── Courses/
│   │   ├── Index.cshtml           # Course catalog
│   │   ├── MyCourses.cshtml       # Enrolled courses
│   │   ├── Enroll.cshtml          # Enrollment form
│   │   ├── CourseDetail.cshtml    # Course content viewer
│   │   ├── CourseCreate.cshtml    # Admin: create course
│   │   └── AccessDenied.cshtml
│   ├── UserCourseProgress/
│   │   └── Index.cshtml           # Progress tracker
│   └── Shared/
│       ├── _Layout.cshtml          # Global layout (navbar, footer)
│       └── _LoginPartial.cshtml
├── wwwroot/
│   ├── css/
│   │   ├── theme.css               # Main design system (CSS custom properties, components)
│   │   └── ideStyles.css           # IDE-specific overrides
│   └── js/
│       ├── compiler/
│       │   ├── ide.js              # Ace editor init
│       │   └── lib/                # Ace editor library
│       ├── compilerScripts.js      # Code execution + AI chat fetch logic
│       ├── app.jsx                 # React entry (course create form)
│       └── CourseCreateForm.jsx
├── pythonCode/
│   └── inputTest.py               # Python execution helper
├── Program.cs                      # App bootstrap, DI, middleware, seed
├── appsettings.json
└── webProject.csproj

LLamaSharp/                        # LLamaSharp library (Llama 2 C# bindings)
```

---

## How Code Execution Works

1. The browser posts the Python code to `POST /api/CodeExecution/execute`.
2. The API controller writes the code to a temp file and spawns a Python subprocess.
3. `stdout` / `stderr` are captured and returned as JSON.
4. The browser renders the output in the IDE output panel (supports text and base64-encoded images for Qiskit circuit diagrams).

> **Security note:** The code execution is sandboxed to the Python process. For production deployments, wrap the Python runner in a Docker container with resource limits and network restrictions.

---

## AI Assistant

The AI chat uses LLamaSharp to run a quantized Llama 2 model (GGUF format) locally — no external API calls or keys required.

To enable it, place your `.gguf` model file in the project and update the model path in the relevant controller. The default model expected is a Llama 2 7B Q4 GGUF file.

---

## Configuration

All connection strings are in `appsettings.json`. The app uses SQL Server LocalDB by default:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-webProject-...;Trusted_Connection=True"
  }
}
```

For production, replace with a full SQL Server connection string and set `ASPNETCORE_ENVIRONMENT=Production`.

---

## License

MIT
