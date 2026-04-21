# Product API

A production-style ASP.NET Core Web API built to simulate a **retail inventory and order management backend**.


---

## 🛠️ Tech Stack

* **ASP.NET Core Web API(.NET 8)**
* **C#**
* **Swagger / OpenAPI**
* **Entity FrameWork Core(EF Core 8)**
* **In-Memory Database (for development)**
* **Visual Studio Code**

---

## 🧠 Key Concepts Applied

- SOLID Principles  
- Repository Pattern  
- Dependency Injection  
- Async / Await  
- EF Core (`AsNoTracking`, `Include`)  
- Cache-Aside Pattern  
- Middleware Pipeline  
- Concurrency Handling  
- Performance Optimization  

---

## 📘 Features

### 📦 Product Management
- CRUD operations for products
- Pagination and filtering

### 🛒 Order Management
- Place orders
- Stock validation before order creation

### 📊 Inventory Management
- Check stock availability
- Update stock safely

### ⚡ Caching
- Cache-aside pattern for product retrieval
- Cache invalidation on updates

### 🔄 Async API
- Full async flow using Task-based programming

### 🗄️ Database Integration
- EF Core with DbContext
- Async queries (`ToListAsync`, `FindAsync`)

### 📝 Logging
- Structured logging using `ILogger`
- Request + business event logging

### 🔒 Concurrency Handling
- Prevents overselling using locking
- Returns proper HTTP responses (`409 Conflict`)

### 🚀 Performance Optimization
- Latency tracking via middleware
- Cache vs DB performance comparison
- `AsNoTracking` for faster read operations

---

## 🏗️ Architecture

Current layered architecture:

Controller → Service → Repository → EF Core → In-Memory DataBase

Modules:

- Products
- Stock / Inventory
- Orders
- Discount Strategies

---

## 📂 Project Structure

```text
ProductApi
│
├── Controllers
│   └── ProductsController.cs
│   └── StockController.cs
│   └── DiscountController.cs
│   └── OrderContoller.cs
|
|
├── Services
│   ├── Interfaces/
|   │   └── IProductService.cs
|   │   └── IStockService.cs
|   │   └── IDiscountService.cs
|   │   └── IOrderService.cs
|   │   └── IProductCacheService.cs
|   |
│   └── ProductService.cs
│   └── StockService.cs
│   └── OrderService.cs
│   └── FestivalDiscount.cs
│   └── BulkDiscount.cs
│   └── ProductCacheService.cs
|
|
├── Repository
│   ├── Interfaces/
|   │   └── IProductRepository.cs
|   │   └── IOrderRepository.cs
|   |
│   └── ProductRepository.cs
│   └── OrderRepository.cs
│
|
├── Models
│   └── BaseEntity.cs
│   └── Product.cs
│   └── Order.cs
│   └── PagedResponse.cs
|
|
├── Middlewares
│   └── ExceptionMiddleware.cs
│   └── LoggingMiddleware.cs
│
|
├── Data
│   └── AppDbContext.cs
|
|
├── Program.cs
│
|
└── README.md
```

---

## Implemented endpoints:

### ✅ Products


* `GET /products`
* `GET /products/{id}`
* `POST /products`
* `GET /products/search?name={value}`
* `PUT /products/{id}`
* `Delete /products/{id}`
* `GET /products/filters?pageNumber={value}&pageSize={value}&category={value}&minprice={value}&maxprice={value}`


### ✅ Stock

* `GET /stocks/{id}/availability`
* `GET /stocks/{id}/update`

### ✅ Discount

* `GET /discounts/{id}`

### ✅ Orders


* `Get /orders`
* `POST /orders`
* `POST /orders/simulate-concurrent-orders`





