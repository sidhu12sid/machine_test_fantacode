# 🧪 FantaCode Machine Test

A full-stack project built with **.NET 8 (ASP.NET Core)** for the backend and **Angular 20** for the frontend. It includes a login system, a dashboard with a sidebar, and a bar chart displaying sales data using **Chart.js**.
You can access the deployed frontend site at :

[Deployed Frontend](https://login-app-client.vercel.app/)

You can find the frontend code in login-client-app folder. Or you can refer this repo [Frontend Repo](https://github.com/sidhu12sid/login-app-client)

---

## 🚀 Tech Stack

### 🔧 Backend
- **ASP.NET Core 8**
- **Entity Framework Core (Code First)**
- **SQLite** (for persistent storage)

### 🌐 Frontend
- **Angular 20**
- **Chart.js** (for rendering sales bar chart)

---

### APIs
I have published the apis to the azure app services. Below are the curls to the respective APIs.

- User create api

    `curl --location 'https://machinetest-fantacode-b0asgtbvb4cuf0eq.canadacentral-01.azurewebsites.net/api/Auth/register-user' \
--header 'Content-Type: application/json' \
--data-raw '{
  "username": "Sidharth",
  "password": "Pass@123",
  "email": "sidharth@yahoo.in"
}
'`

- Login api
  
    `curl --location 'https://machinetest-fantacode-b0asgtbvb4cuf0eq.canadacentral-01.azurewebsites.net/api/Auth/user-login' \
--header 'Content-Type: application/json' \
--data-raw '{
  "userName": "sidharth",
  "password": "Pass@123"
}
'`

## 🔐 Features

- ✅ Login page with  user authentication
- ✅ Dashboard with routing
- ✅ Sidebar navigation layout
- ✅ Bar chart showing dummy sales data
- ✅ Persistent user data using SQLite

---

## 🧑‍💻 Demo Credentials

Use the following credentials to log in:

```txt
Username: Sidharth  
Password: Pass@123
