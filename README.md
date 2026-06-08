# Financial Tracking API

A RESTful API built with ASP.NET Core 9 for tracking financial portfolios, assets, and transactions. Includes live crypto market data via CoinGecko integration.

## Live Demo

**API:** https://financial-tracking-api-with-csharp-production.up.railway.app

**Example endpoints:**
- `GET /api/coingecko/top?count=10` — Top 10 cryptocurrencies (live)
- `GET /api/portfolio` — All portfolios
- `GET /api/asset` — All assets
- `GET /api/transaction` — All transactions
- `GET /api/user` — All users

## Tech Stack

- **Backend:** ASP.NET Core 9, C#
- **ORM:** Entity Framework Core 9 with Pomelo (MySQL)
- **Database:** MySQL
- **External API:** CoinGecko (live crypto prices)
- **Architecture:** Repository Pattern + Service Layer
- **Deploy:** Railway

## Project Structure

```
FinancialTrackingAPI/
├── Controllers/         # HTTP endpoints
├── Data/                # AppDbContext
├── Dtos/                # Request/Response DTOs
├── Interfaces/          # Repository & Service interfaces
├── Mappers/             # DTO ↔ Model mappers
├── Models/              # Entity models
├── Repositories/        # Data access layer
├── Service/             # Business logic layer
├── Helpers/             # QueryObject (filtering/pagination)
└── Validtions/          # Custom validation attributes
```

## Models

- **User** — App users with portfolios
- **Portfolio** — A named collection of assets per user
- **Asset** — A crypto/stock holding within a portfolio
- **Transaction** — Buy/sell records linked to portfolios and assets

## API Endpoints

### Portfolios
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/portfolio | Get all portfolios |
| GET | /api/portfolio/{id} | Get portfolio by ID |
| POST | /api/portfolio | Create portfolio |
| PUT | /api/portfolio/{id} | Update portfolio |
| DELETE | /api/portfolio/{id} | Delete portfolio |

### Assets
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/asset | Get all assets |
| GET | /api/asset/{id} | Get asset by ID |
| POST | /api/asset | Create asset |
| PUT | /api/asset/{id} | Update asset |
| DELETE | /api/asset/{id} | Delete asset |

### Transactions
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/transaction | Get all transactions |
| GET | /api/transaction/{id} | Get transaction by ID |
| POST | /api/transaction | Create transaction |
| PUT | /api/transaction/{id} | Update transaction |
| DELETE | /api/transaction/{id} | Delete transaction |

### CoinGecko
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/coingecko/top?count=10 | Top N coins by market cap |
| GET | /api/coingecko/symbol/{symbol} | Get coin by symbol (btc, eth...) |

## Running Locally

### Prerequisites
- .NET 9 SDK
- MySQL

### Setup

```bash
git clone https://github.com/emirokttrn/financial-tracking-api-with-Csharp.git
cd financial-tracking-api-with-Csharp/FinancialTrackingAPI
```

Create `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=FinancialTrackingDB;User=root;Password=YOUR_PASSWORD;"
  }
}
```

Run migrations and start:
```bash
dotnet ef database update
dotnet run
```

API will be available at `http://localhost:5062`

## Author

Emir Okutturan — Computer Engineering Student, Azerbaijan State Oil and Industry University
