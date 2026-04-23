# PrintsStudio

PrintsStudio is a full-stack printing service platform built for custom print orders, designer collaboration, and client-facing product showcase. The project combines a `Blazor WebAssembly` frontend with an `ASP.NET Core` backend and follows a layered architecture to keep UI, business logic, data access, and domain models separate.

It is designed around a real service workflow: visitors can explore products, customers can place personalized orders, designers can manage their work and bookings, and admins can manage the catalog and users from one system.

## What The Project Does

PrintsStudio covers the main flows you would expect in an online print and design business:

- browse product categories such as business cards, wedding cards, flyers, shirts, and other custom print items
- view product details, templates, and image galleries
- sign up and log in with role-based access
- place custom orders with uploaded designs and selected options
- submit bookings, reviews, and contact inquiries
- manage designer-related workflows
- support admin-side management for products, templates, orders, and users

## How It Is Implemented

The application is split into five main projects so each layer has a clear responsibility.

### `PrintsStudio.Client`

This is the `Blazor WebAssembly` frontend. It contains:

- page components and reusable UI components
- client-side service classes for API calls
- UI state and interaction logic
- styles, assets, and frontend behavior

The client talks to the backend through HTTP APIs and renders the customer-facing experience in the browser.

### `PrintsStudio.Server`

This is the `ASP.NET Core` backend entry point. It contains:

- API controllers
- application startup and dependency injection setup
- authentication and authorization wiring
- production configuration for demo deployment

It exposes the REST endpoints used by the Blazor frontend.

### `PrintsStudio.Application`

This layer contains application-level logic and contracts:

- service interfaces
- DTOs and shared response models
- orchestration logic between controller and repository layers

It helps keep business rules out of the UI and infrastructure details.

### `PrintsStudio.Infrastructure`

This layer handles persistence and framework-specific concerns:

- Entity Framework Core database access
- Identity user management
- repository implementations
- data storage details for products, orders, reviews, contact forms, bookings, and users

### `PrintsStudio.Domain`

This is the core domain layer. It contains:

- entities
- core models
- business-facing types
- interfaces used by the higher-level services

This keeps the project centered around the problem domain rather than framework code.

## Architecture Overview

The app follows a layered structure similar to clean architecture:

1. The `Client` sends requests to backend API endpoints.
2. The `Server` receives requests and routes them to application services.
3. The `Application` layer applies business logic and coordinates work.
4. The `Infrastructure` layer persists and retrieves data.
5. The `Domain` layer defines the core business entities used throughout the project.

That separation makes the project easier to understand, extend, and maintain.

## Tech Stack

- Frontend: `Blazor WebAssembly`, `Bootstrap`, `.NET 8`
- Backend: `ASP.NET Core Web API`, `.NET 8`
- Data access: `Entity Framework Core`
- Authentication: `ASP.NET Core Identity`
- Demo deployment: `Ubuntu EC2`, `Nginx`, `systemd`, `SQLite`
- CI/CD: `GitHub Actions`

## Project Structure

```text
PrintsStudio/
├── PrintsStudio.Client/          Frontend UI and client-side services
├── PrintsStudio.Server/          API entry point and app configuration
├── PrintsStudio.Application/     Service contracts, DTOs, app logic
├── PrintsStudio.Infrastructure/  EF Core, repositories, Identity, persistence
├── PrintsStudio.Domain/          Core entities and domain contracts
├── deployment/aws-ec2/           EC2 deployment scripts and Nginx/service files
└── .github/workflows/            GitHub Actions automation
```

## Authentication And Roles

The project uses ASP.NET Core Identity for authentication and role management.

Current role model:

- `Visitor`: public browsing
- `Customer`: ordering and customer-side flows
- `Designer`: booking and designer-side flows
- `Admin`: management workflows

Notes:

- the seeded admin account is `admin@printsstudio.com`
- the seeded admin password is `Admin@123`
- public signup is intentionally limited to `Customer` and `Designer`

## Frontend Direction

The frontend has been tuned to feel more polished and consistent through:

- shared typography and color tokens
- cleaner navigation and footer styling
- improved button and form styling
- subtle section animations and smoother visual rhythm

This gives the project a better client-demo feel without changing its core structure.

## Deployment

For demo hosting, the project includes a simple EC2 deployment setup that serves:

- the frontend through `Nginx`
- the backend as a `systemd` service
- lightweight demo data through `SQLite`

Deployment docs:

- [AWS EC2 Deployment Guide](./PrintsStudio/deployment/aws-ec2/README.md)

## CI/CD

A simple GitHub Actions deployment workflow is included for push-based deployment to EC2.

Files:

- [GitHub Actions Workflow](./PrintsStudio/.github/workflows/deploy-ec2.yml)
- [Required GitHub Secrets](./PrintsStudio/.github/DEPLOYMENT_SECRETS.md)

The workflow is intended for simple demo delivery rather than a large-scale production pipeline.

## Why This Project Is Useful

PrintsStudio is a strong portfolio project because it demonstrates:

- full-stack application design
- role-based authentication
- layered backend architecture
- real CRUD workflows
- file upload handling
- deployment automation
- practical demo hosting on cloud infrastructure

## Status

This repository is set up for showcase and demo usage. The current deployment path is intentionally optimized for simplicity, low traffic, and fast iteration.
