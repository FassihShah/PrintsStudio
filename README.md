# PrintsStudio

PrintsStudio is a modern web application designed for an online printing service. It allows users to upload documents, customize print options, select design templates, place orders, and interact with designers. The project leverages Blazor for the frontend UI and is structured for easy scalability and maintainability.


## Project Overview

PrintsStudio aims to streamline the printing order process by providing a user-friendly platform where customers can easily upload their files, choose customization options, browse design templates, and communicate with designers. The system also includes an admin dashboard to manage products, orders, and designer profiles efficiently.


## Features

- User registration and authentication
- Document upload with print customization options
- Multiple product design templates
- Designer profiles and availability management
- Order management system with order item details
- Admin dashboard for managing products, designers, and orders
- Responsive UI built with Blazor

## Technology Stack

- Frontend: Blazor 
- Backend: ASP.NET Core
- Database: SQL Server
- Language: C#
- Authentication: ASP.NET Core Identity with role management (Admin, Designer, Customer)
- Other: Entity Framework Core for data access, OData, JS Interop, SignalR, Ajax


## Project Structure

PrintsStudio.sln

|
├── PrintsStudio.Client           
├── PrintsStudio.Server           
├── PrintsStudio.Domain           
├── PrintsStudio.Infrastructure   
├── PrintsStudio.Application           

