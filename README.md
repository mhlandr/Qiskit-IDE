# Qiskit IDE

A full-stack quantum computing learning platform built with ASP.NET Core 8. Features a browser-based Qiskit compiler, structured courses, and an integrated AI assistant powered by LLamaSharp (Llama 2).

---

## Features

- **Online Qiskit Compiler** — Write and execute Python/Qiskit code directly in the browser using the Ace editor. Code runs server-side via a sandboxed Python subprocess.
- **AI Assistant** — Integrated LLM chat (Llama 2 via LLamaSharp) for quantum questions, code explanations, and debugging.
- **Course System** — Admins create structured courses with rich HTML content. Authenticated users can enroll and track progress.
- **Role-based Access** — `Admin` and `User` roles enforced via ASP.NET Core Identity.
- **Modern UI** — Dark quantum-themed interface with animated gradient accents, bento grid layout, and full mobile responsiveness.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core 8 Razor Pages |
| ORM | Entity Framework Core + SQL Server |
| Auth | ASP.NET Core Identity + Roles |
| Code editor | Ace Editor (browser) |
| AI model | LLamaSharp — Llama 2 (GGUF) |
| Frontend build | Webpack + Babel + React |
| Styling | Custom CSS + Bootstrap 5 grid |

---

## Running the Qiskit environment with Docker

The repository includes a Dockerfile that sets up a sandboxed Python environment with Qiskit and Qiskit-Aer — the same environment the server uses to execute quantum circuits.

**Location:** `LLamaSharp/LLama.WebAPI/Doker/Dockerfile`

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Windows / Mac / Linux)

### 1. Clone the repository

```bash
git clone https://github.com/mhlandr/Qiskit-IDE.git
cd Qiskit-IDE
```

### 2. Build the image

```bash
docker build -t qiskit-ide LLamaSharp/LLama.WebAPI/Doker/
```

### 3. Run a circuit

```bash
docker run --rm -v "$(pwd)/output:/usr/src/app/output" qiskit-ide
```

This runs the sample Bell-state circuit, prints the measurement counts to the terminal, and saves a circuit diagram PNG to an `output/` folder in your current directory.

**On Windows (Command Prompt), replace `$(pwd)` with `%cd%`:**

```cmd
docker run --rm -v "%cd%/output:/usr/src/app/output" qiskit-ide
```

### 4. Run your own script

Mount your Python file into the container instead of using the default:

```bash
docker run --rm \
  -v "$(pwd)/output:/usr/src/app/output" \
  -v "$(pwd)/my_circuit.py:/usr/src/app/test_script.py" \
  qiskit-ide
```

---

## Running locally (without Docker)

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (included with Visual Studio)
- [Python 3.x](https://www.python.org/downloads/) with Qiskit installed
- `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`

### 1. Install Python dependencies

```bash
pip install qiskit qiskit-aer matplotlib pylatexenc numpy
```

### 2. Apply database migrations

```bash
cd webProject
dotnet ef database update
```

### 3. Run the app

```bash
dotnet run
```

Or open `webProject.sln` in Visual Studio and press F5. The app starts at `https://localhost:7136`.

---

## Default Accounts

After running migrations, the seed creates one account automatically:

| Role | Email | Password |
|------|-------|----------|
| Admin | `admin12@example.com` | `Adminn12@123` |

Register additional users via `/Identity/Account/Register`. New registrations receive the `User` role.

---

## AI Assistant Setup

The AI chat uses LLamaSharp to run a quantized Llama 2 model locally — no external API calls or keys required.

Place your `.gguf` model file (Llama 2 7B Q4 recommended) in the project and update the model path in the relevant controller. The model file is not included in the repository due to its size.

---

## Project Structure

```
webProject/
├── Areas/Identity/Pages/Account/   # Login, Register (custom-styled)
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── ApplicationUser.cs
│   ├── Migrations/
│   └── SeedData.cs                 # Admin seed + role creation
├── Models/
│   ├── Course.cs
│   ├── UserCourse.cs
│   └── UserCourseProgress.cs
├── Pages/
│   ├── compile.cshtml              # IDE page (Ace editor + AI chat)
│   ├── Index.cshtml                # Landing page
│   ├── Courses/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   └── js/
│       ├── compiler/ide.js         # Ace editor init
│       ├── compilerScripts.js      # Code execution + AI chat fetch
│       ├── app.jsx                 # React entry (course create form)
│       └── CourseCreateForm.jsx
├── pythonCode/inputTest.py
├── Program.cs
├── appsettings.json
└── webProject.csproj

LLamaSharp/                         # LLamaSharp library (Llama 2 C# bindings)
Dockerfile                          # Multi-stage build (ASP.NET + Python)
docker-compose.yml                  # App + SQL Server 2022
```

---

## How Code Execution Works

1. The browser posts Python code to `POST /api/CodeExecution/execute`.
2. The API controller writes the code to a temp file and spawns a Python subprocess.
3. `stdout` / `stderr` are captured and returned as JSON.
4. The browser renders output in the IDE panel, including base64-encoded circuit diagram images.

> **Security note:** For production, the Docker setup already isolates the Python subprocess inside the container. Add resource limits (`mem_limit`, `cpus`) to `docker-compose.yml` and restrict outbound network access as needed.

---

## License

MIT
