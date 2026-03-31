# Product API

A simple ASP.NET Core Web API built to understand **request lifecycle, endpoint flow, controllers, routing, and CRUD fundamentals**.

This project was built as a practical learning exercise and is designed to serve as a **foundation for future enterprise retail application development**.(if developed)

---

## 🎯 Purpose

The goal of this project is to understand how an ASP.NET Core API processes requests end-to-end, including:

* Request flow through middleware
* Endpoint routing
* Controllers and action methods
* Model binding
* Response generation
* REST API design basics

This project is intentionally kept simple so the core concepts are clear.

---

## 🛠️ Tech Stack

* **ASP.NET Core Web API**
* **C#**
* **Swagger / OpenAPI**
* **In-memory data storage**
* **Visual Studio Code**

---

## 📂 Project Structure

```text
ProductApi
│
├── Controllers
│   └── ProductsController.cs
│
├── Models
│   └── Product.cs
│
├── Program.cs
│
└── README.md
```

---

## 📘 Features

Implemented endpoints:

* `GET /products`
* `GET /products/{id}`
* `POST /products`

This project currently uses an **in-memory list** instead of a database.

---

## 🚀 API Endpoints

### Get All Products

```http
GET /products
```

Returns the list of all products.

---

### Get Product By Id

```http
GET /products/{id}
```

Example:

```http
GET /products/1
```

Returns a single product by id.

---

### Create Product

```http
POST /products
```

Sample request body:

```json
{
  "id": 3,
  "name": "Laptop",
  "price": 65000
}
```

Returns:

* `201 Created`
* created product object
* location header of the new resource

---

## 🔄 Request Flow Understanding

One of the main learning objectives of this project is understanding how a request flows inside ASP.NET Core.

Example request:

```http
GET /products/1
```

Flow:

```text
Client Request
   ↓
Kestrel Web Server
   ↓
Middleware Pipeline (Program.cs)
   ↓
Routing (MapControllers)
   ↓
ProductsController
   ↓
Action Method
   ↓
JSON Response
```

---

## 📖 Concepts Practiced

This project helped reinforce:

* **Kestrel server**
* **middleware pipeline**
* **routing**
* **controllers**
* **action methods**
* **HTTP verbs**
* **model binding**
* **status codes**
* **RESTful API basics**


