# BasketApi

A simple RESTful API for managing shopping baskets built with .NET Core.

## Features

- Add items to basket
- Remove items from basket
- View basket contents
- Checkout functionality
- In-memory storage for basket items

## API Endpoints

### Add Item to Basket

```
POST /api/basket/add
```

Parameters:

- `userId` (int): The ID of the user
- `item` (BasketItem): The item to add to the basket

### Remove Item from Basket

```
POST /api/basket/remove
```

Parameters:

- `userId` (int): The ID of the user
- `packageId` (int): The ID of the package to remove

### View Basket

```
GET /api/basket
```

Parameters:

- `userId` (int): The ID of the user

### Checkout

```
POST /api/basket/checkout
```

Parameters:

- `userId` (int): The ID of the user

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- Your favorite IDE (Visual Studio, VS Code, etc.)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/ebra8/BasketApi.git
```

2. Navigate to the project directory:

```bash
cd BasketApi
```

3. Run the application:

```bash
dotnet run
```

The API will be available at `https://localhost:5001` and `http://localhost:5000`.

## Swagger Documentation

When running in development mode, you can access the Swagger UI at:

```
https://localhost:5001/swagger
```

## Data Model

### BasketItem

- `PackageId` (int): Unique identifier for the package
- `Quantity` (int): Number of items

## Notes

- The current implementation uses in-memory storage, which means data will be lost when the application restarts
- The API includes basic error handling for common scenarios
- Swagger/OpenAPI documentation is enabled in development mode

## License

This project is licensed under the MIT License - see the LICENSE file for details.
