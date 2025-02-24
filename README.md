# Patient Management System

This project is a full-stack application for managing patients, built with **.NET 8** for the backend and **React + Vite + Tailwind CSS + TypeScript** for the frontend.

## 🗂️ Project Structure
```
├── backend/       # ASP.NET Core Web API (.NET 8)
└── frontend/      # React + Vite + Tailwind + TypeScript
```

---

## 🚀 Getting Started

### 📦 Backend Setup (ASP.NET Core Web API)
1. **Navigate to the backend folder:**
   ```bash
   cd backend
   ```

2. **Configure the SQL Server connection string:**  
   Open the file `appsettings.Development.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PatientDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```
   🔔 **Note:** Make sure SQL Server Express is installed and accessible.

3. **CORS Configuration (if needed):**  
   If the React app runs on a port different from `http://localhost:5173`, update the `Program.cs` file in the backend:
   ```csharp
   var allowedOrigins = "http://localhost:5173"; // Change this to match your React app URL
   builder.Services.AddCors(options =>
   {
       options.AddPolicy("CorsPolicy", policy =>
       {
           policy.WithOrigins(allowedOrigins)
                 .AllowAnyHeader()
                 .AllowAnyMethod();
       });
   });
   
   app.UseCors("CorsPolicy");
   ```

4. **Run the backend:**
   Make sure you are in the interviewTest.PatientService.API project and run the following command.
   The migration will be performed automatically by the project.
   ```bash
   dotnet restore
   dotnet run
   ```

---

### 💻 Frontend Setup (React + Vite + Tailwind)
1. **Navigate to the frontend folder:**
   ```bash
   cd frontend
   ```

2. **Configure the environment variables:**  
   Create a `.env` file in the `frontend` directory and add:
   ```env
   VITE_API_BASE_URL=http://localhost:5000/api  # Replace with your backend URL if different
   ```

3. **Install dependencies:**
   ```bash
   npm install
   ```

4. **Run the frontend:**
   ```bash
   npm run dev
   ```

5. **Access the app:**  
   Open your browser at the URL provided in the terminal (usually `http://localhost:5173`).

---

## 📑 Features
- 📝 CRUD operations for patients (Create, Read, Update, Delete).
- 📅 Modal forms with full validations for patient information.
- 🌎 CORS support for secure communication between frontend and backend.
- 🎨 Responsive UI with Tailwind CSS.
- 🛡️ Validation and error handling with clear alerts.

---

## ⚙️ Technologies Used
- **Backend:** .NET 8, Entity Framework Core, SQL Server Express
- **Frontend:** React, Vite, TypeScript, Tailwind CSS, Axios, React Hook Form

---

## 🚧 Troubleshooting
- **CORS Issues:**  
  Make sure to update the allowed origins in `Program.cs` to match the frontend URL.
- **Database Connection Errors:**  
  Verify the connection string and ensure SQL Server Express is running.
- **Port Conflicts:**  
  Change the ports in the frontend `.env` or backend `launchSettings.json` as needed.

---

## 📢 Contributions
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

---

## 📝 License
This project is licensed under the MIT License.

---

🚀 **Enjoy building with the Patient Management System!** 😊
