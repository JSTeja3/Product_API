# Product API

A production-style ASP.NET Core Web API built to simulate a **retail inventory and order management backend**.

This project started as a simple Product CRUD API and has been progressively enhanced using **clean architecture, SOLID principles, middleware, repository pattern, pagination, filtering, inventory, and order placement workflows**.


---

## 🛠️ Tech Stack

* **ASP.NET Core Web API**
* **C#**
* **Swagger / OpenAPI**
* **In-memory data storage**
* **Visual Studio Code**

---

## 🏗️ Architecture

Current layered architecture:

Controller → Service → Repository → In-Memory Data

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
├── IServices
│   └── IProductService.cs
│   └── IStockService.cs
│   └── IDiscountService.cs
│   └── IOrderService.cs
|
├── Services
│   └── ProductService.cs
│   └── StockService.cs
│   └── DiscountService.cs
│   └── OrderService.cs
|
├── IRepository
│   └── IProductRepository.cs
│   └── IOrderRepository.cs
|
├── Repository
│   └── ProductRepository.cs
│   └── OrderRepository.cs
│
├── Models
│   └── BaseEntity.cs
│   └── Product.cs
│   └── Order.cs
│   └── PagedResponse.cs
|
├── Middlewares
│   └── ExceptionMiddleware.cs
│   └── LoggingMiddleware.cs
│
├── Program.cs
│
└── README.md
```

---

## 📘 Features

Implemented endpoints:

### ✅ Products


* `GET /products`
* `GET /products/{id}`
* `POST /products`
* `GET /products/search?name={value}`
* `PUT /products/{id}`
* `Delete /products/{id}`
* `GET /products?pageNumber={value}&pageSize={value}&category={value}&minprice={value}&maxprice={value}`


### ✅ Stock

* `GET /stocks/{id}/availability`
* `GET /stocks/{id}/update`

### ✅ Discount

* `GET /discounts/{id}`

### ✅ Orders


* `POST /orders`





