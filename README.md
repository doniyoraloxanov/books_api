# Books Management API

## Overview
The **Books Management API** is a RESTful service built using ASP.NET Core Web API that allows users to manage books securely. The API supports essential book operations, including adding, updating, retrieving, and soft deleting books, with proper validation and authentication via JWT.

## Technologies Used
- **Framework**: .NET 8 / .NET 9
- **Database**: SQL Server (EF Core / Dapper) or MongoDB (MongoDB C# Driver)
- **Authentication**: JWT-based authentication
- **Architecture**: 3-layered (Models, Data Access, API)
- **API Documentation**: Swagger

## Features
### Book Management
- Add single and bulk books (with validation to prevent duplicates)
- Update book details
- Soft delete single or bulk books

### Retrieval Operations
- Get a paginated list of book titles sorted by popularity
- Get detailed information of a specific book

### Popularity Score Calculation (on-the-fly, not stored in DB)
- Based on book views and years since publication
- **Formula**: `Popularity Score = BookViews * 0.5 + YearsSincePublished * 2`

### Security & Validation
- JWT authentication to secure all endpoints
- Input validation to ensure correct data submission

