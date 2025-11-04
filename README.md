# TCG Database API

A .NET 7 ASP.NET Core Web API for managing your Trading Card Game (TCG) collections. Currently supports Yu-Gi-Oh! with integration to the [YGOPRODeck API](https://ygoprodeck.com/api-guide/), with plans to support additional TCGs. Card data is stored in a PostgreSQL database.

## Features

- ✅ Fetch card data from YGOPRODeck API
- ✅ Store cards with complete information (sets, images, prices)
- ✅ Support for cards with Card ID or Card Name
- ✅ Handle newer sets not yet in YGOPRODeck database
- ✅ Clean architecture with separation of concerns
- ✅ PostgreSQL database support
- ✅ Swagger/OpenAPI documentation

## Prerequisites

- .NET 7.0 SDK or later
- PostgreSQL database
- Git

## Getting Started

### 1. Clone the Repository

```bash
git clone <your-repo-url>
cd TcgDatabase
```

### 2. Configure Database

Create `TcgDatabase.API/appsettings.json` from the example file:
```bash
cp TcgDatabase.API/appsettings.json.example TcgDatabase.API/appsettings.json
```

Update the connection string in `TcgDatabase.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=Yu_Gi_Oh_Data;Username=postgres;Password=your_password"
  }
}
```

### 3. Run Database Migrations

```bash
cd TcgDatabase.Infrastructure
dotnet ef database update --startup-project ../TcgDatabase.API --context AppDbContext
```

**Note:** If you don't have Entity Framework tools installed:

```bash
dotnet tool install --global dotnet-ef
```

### 4. Run the API

```bash
cd TcgDatabase.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## API Endpoints

### GET `/api/Card`

Get all cards in the database.

**Response:**
```json
[
  {
    "id": "80181649",
    "name": "A Case for K9",
    "description": "When this card is activated..."
  }
]
```

### POST `/api/Card`

Add a card to the database by fetching it from YGOPRODeck API.

**Request Body:**
```json
{
  "cardId": "80181649",
  "setId": "JUSH-EN040"
}
```

**Or using card name:**
```json
{
  "cardName": "A Case for K9",
  "setId": "JUSH-EN040"
}
```

**Parameters:**
- `cardId` (optional): 8-digit card passcode (recommended - more reliable)
- `cardName` (optional): Exact card name
- `setId` (required): Set code (e.g., "JUSH-EN040")

**Note:** Provide either `cardId` OR `cardName`, not both.

**Response:**
```json
{
  "id": "80181649",
  "name": "A Case for K9",
  "description": "When this card is activated..."
}
```

## Handling Newer Sets

If you provide a `setId` that isn't yet in the YGOPRODeck database (newer sets), the API will:
- Still add the card to your database
- Store the `setId` you provided
- Set other set fields (name, rarity, price) to `null`
- Log a warning with available sets

You can update these fields later when YGOPRODeck adds the set to their database.

## Project Structure

```
TcgDatabase/
├── TcgDatabase.API/              # Web API layer (Controllers, Program.cs)
├── TcgDatabase.Application/      # Business logic layer (Services, DTOs)
├── TcgDatabase.Domain/           # Domain models
├── TcgDatabase.Infrastructure/   # Data access layer (DbContext, Repositories, Models)
└── test/                         # Test project
```

## Architecture

This project follows **Clean Architecture** principles:

- **Domain**: Core business entities
- **Application**: Business logic and use cases
- **Infrastructure**: Data access, external services
- **API**: Controllers and HTTP handling

## Technologies Used

- .NET 7.0
- ASP.NET Core Web API
- Entity Framework Core 7.0
- PostgreSQL (via Npgsql)
- Swagger/OpenAPI
- HttpClient for YGOPRODeck API integration

## YGOPRODeck API

This project uses the [YGOPRODeck API v7](https://ygoprodeck.com/api-guide/). Please respect their rate limits:
- **20 requests per 1 second**
- Exceeding the limit results in a 1-hour block

**Important:** Download and store data locally to minimize API calls. The API responses are cached for 2 days on YGOPRODeck's side.

## Database Schema

### Cards
- Basic card information (id, name, type, description, etc.)
- Links to sets, images, and prices

### CardSets
- Set information (code, name, rarity, price)
- Set details can be null for newer sets not in YGOPRODeck

### CardImages
- Image URLs (full, small, cropped)

### CardPrices
- Prices from multiple marketplaces

### MonsterCards
- Monster-specific data (ATK, DEF, Level, Attribute, etc.)

## Development

### Adding a Migration

```bash
dotnet ef migrations add MigrationName --project TcgDatabase.Infrastructure --startup-project TcgDatabase.API --context AppDbContext
```

### Updating Database

```bash
dotnet ef database update --project TcgDatabase.Infrastructure --startup-project TcgDatabase.API --context AppDbContext
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is open source and available under the MIT License.

## Acknowledgments

- [YGOPRODeck](https://ygoprodeck.com/) for providing the free Yu-Gi-Oh! API
- All contributors and the Yu-Gi-Oh! community

## Support

For issues or questions, please open an issue on GitHub.

