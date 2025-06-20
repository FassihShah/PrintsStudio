# PrintsStudio – Full Stack Blazor Web Application

**PrintsStudio** is a dynamic, modern full-stack printing service platform built with **Blazor WebAssembly** for the frontend and a **Clean Architecture ASP.NET Core API** on the backend. It allows users to customize, preview, and order printed products such as business cards, wedding invitations, and personalized templates.

> Explore the live experience of role-based dashboards, real-time template customization, and seamless product ordering in a production-grade architecture.


---

## Key Features

- 🔐 **Role-Based Access** (Admin, Designer, Customer)
- 🛒 **Customizable Product Ordering**
- 🎨 **Pre-designed Template Gallery**
- 👨‍🎨 **Designer Portfolio & Booking System**
- 📦 **Order Tracking & Management**
- 📄 **Contact Form, Reviews, FAQs, and More**
- 💻 **Admin Dashboard** for managing products, templates, users

---

## Technology Stack

| Layer       | Technology                               |
|------------|-------------------------------------------|
| Frontend   | Blazor WebAssembly (.NET 8), Bootstrap 5  |
| Backend    | ASP.NET Core Web API                      |
| Architecture | Clean Architecture (Domain, App, Infra, API) |
| ORM        | Entity Framework Core (Code First)        |
| Identity   | ASP.NET Core Identity                     |
| Tools      | AutoMapper, Dependency Injection, REST    |

---

## Project Structure

```bash

PrintsStudio/
.
├── PrintsStudio.Domain/           # Core domain models (Entities, Enums, Interfaces)
├── PrintsStudio.Application/      # Application logic (CQRS, DTOs, Services)
├── PrintsStudio.Infrastructure/   # Infrastructure concerns (DB, repositories, file services)
├── PrintsStudio.Server/           # ASP.NET Core Web API (entry point for backend)
├── PrintsStudio.Client/           # Blazor WebAssembly frontend (UI logic and components)
└── PrintsStudio.sln               # Visual Studio solution file
```
---

