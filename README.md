# URL Shortener

This project is a simple yet powerful URL shortening service created with .NET and ASP.NET Core. It provides a RESTful API to create, manage, and track shortened URLs, using a PostgreSQL database for persistence.

## Key Features

- **Shorten URLs**: Convert any long URL into a unique, short equivalent.
- **Redirection**: Automatically redirect users from the short URL to the original long URL.
- **Click Tracking**: Counts the number of times each short link is accessed.
- **URL Statistics**: Retrieve access counts and other details for any short URL.
- **Full CRUD Operations**: Create, Read, Update, and Delete short URLs.
- **API Documentation**: Comes with built-in Swagger (OpenAPI) support for easy API exploration and testing during development.

## Technology Stack

- **Backend**: .NET 8 / ASP.NET Core
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger (OpenAPI)

## Getting Started

To get the project running on your local machine, please follow these steps.

### Prerequisites

You will need to have the following software installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [PostgreSQL](https://www.postgresql.org/)

### Installation and Setup

1. **Clone the repository**:

   ```bash
   git clone <your-repository-url>
   cd url-shortener
   ```

2. **Configure Database Connection**:  
   Open the `Url-Shortener/appsettings.json` file. Update the `DefaultConnection` string with your local PostgreSQL credentials:

   ```json
   {
   	"ConnectionStrings": {
   		"DefaultConnection": "Host=localhost;Database=data;Username=your_postgres_user;Password=your_postgres_password"
   	}
   }
   ```

3. **Apply Database Migrations**:  
   Navigate to the `Url-Shortener` project directory and run:

   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:

   ```bash
   dotnet run --project Url-Shortener
   ```

   The API will now be available at:

   - http://localhost:5224
   - https://localhost:7290

5. **Access API Documentation**:  
   Open your browser and go to [http://localhost:5224/swagger](http://localhost:5224/swagger) to use the Swagger UI.

---

## API Endpoints

All endpoints are under the base path `/url`.

### POST `/`

**Creates a new shortened URL.**

- **Request Body**:

  ```json
  {
  	"url": "https://www.example.com/very/long/url/to/shorten",
  	"shortCode": "example"
  }
  ```

- **Success Response (200 OK)**:

  ```
  Url shortened! http://localhost:5224/url/shorten/example
  ```

---

### GET `/`

**Retrieves all shortened URLs.**

- **Success Response (200 OK)**:

  ```json
  [
  	{
  		"id": 1,
  		"url": "https://www.example.com/very/long/url/to/shorten",
  		"shortCode": "example",
  		"createdAt": "2025-06-11T20:45:00.000Z",
  		"updatedAt": null,
  		"accessCount": 0
  	}
  ]
  ```

---

### GET `/shorten/{shortcode}`

**Redirects to the original URL.**

- **Example**:  
  Accessing `http://localhost:5224/url/shorten/example` redirects to `https://www.example.com/very/long/url/to/shorten`.

---

### GET `/{shortcode}/stats`

**Fetches statistics for a specific short URL.**

- **Success Response (200 OK)**:

  ```json
  {
  	"id": 1,
  	"url": "https://www.example.com/very/long/url/to/shorten",
  	"shortCode": "example",
  	"createdAt": "2025-06-11T20:45:00.000Z",
  	"updatedAt": null,
  	"accessCount": 1
  }
  ```

---

### PUT `/shorten/{shortcode}`

**Updates the original URL of an existing short code.**

- **Request Body**:

  ```json
  {
  	"url": "https://www.new-destination.com"
  }
  ```

- **Success Response (200 OK)**:

  ```json
  {
  	"id": 1,
  	"url": "https://www.new-destination.com",
  	"shortCode": "example",
  	"createdAt": "2025-06-11T20:45:00.000Z",
  	"updatedAt": "2025-06-11T20:50:00.000Z",
  	"accessCount": 1
  }
  ```

---

### DELETE `/shorten/{shortcode}`

**Deletes a short URL.**

- **Success Response (202 Accepted)**:  
  An empty response indicating success.

---
