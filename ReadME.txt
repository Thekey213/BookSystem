# BookSystem

BookSystem is a web-based application for managing authors and books. It allows users to perform CRUD (Create, Read, Update, Delete) operations on books and authors using a clean, well-structured architecture.

## Table of Contents

- [Overview](#overview)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Setup Instructions](#setup-instructions)
- [Features](#features)
- [Contributing](#contributing)

## Overview

BookSystem is an ASP.NET Core Web API designed to manage books and authors in a library system. The application leverages the Repository and Service patterns to decouple data access logic from business logic, ensuring a clean, maintainable codebase.

### Key Components
- **Author Management**: Full CRUD operations for managing authors.
- **Book Management**: Full CRUD operations for managing books.
- **Relational Database**: Utilizes Entity Framework Core for managing the relationship between authors and books.

## Technologies

- **ASP.NET Core 8**
- **Entity Framework Core**
- **In-Memory Database (for development)**
- **Repository Pattern**
- **Service Layer**
- **Dependency Injection**
- **Swagger for API Documentation**

## Architecture

BookSystem follows a layered architecture, with the following components:

### 1. **Controllers**
Controllers handle incoming HTTP requests and map them to appropriate service calls. They expose API endpoints for managing books and authors.

### 2. **Services**
The service layer contains business logic and interacts with repositories. It handles CRUD operations and applies business rules.

- **AuthorService**: Manages operations related to authors.
- **BookService**: Manages operations related to books.

### 3. **Repositories**
Repositories abstract data access logic and interact with the database through Entity Framework Core.

- **AuthorRepository**: Manages author-related database operations.
- **BookRepository**: Manages book-related database operations.

### 4. **ApplicationDbContext**
The `ApplicationDbContext` class connects Entity Framework Core to the database. It holds `DbSet` properties for both `Author` and `Book` entities.

## Setup Instructions

### Prerequisites
- **.NET 8 SDK**
- **Visual Studio 2022** or **VS Code**

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-repo/BookSystem.git
   cd BookSystem
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

4. **Access the API Documentation**
   Swagger will be available at the following URL:
   ```
   https://localhost:5001/swagger
   ```

## Features

- **Author Management**: Add, edit, delete, and view authors.
- **Book Management**: Add, edit, delete, and view books.
- **In-Memory Database**: Uses Entity Framework Core's In-Memory provider for easy testing and development.
- **Repository Pattern**: Separates data access from business logic for better maintainability.
- **Service Layer**: Applies business rules and orchestrates operations.

### Example API Endpoints

- **GET** `/api/authors` - Get all authors.
- **POST** `/api/authors` - Add a new author.
- **GET** `/api/books` - Get all books.
- **POST** `/api/books` - Add a new book.

## Contributing

Contributions are welcome! Fork the repository and submit a pull request with your changes.
