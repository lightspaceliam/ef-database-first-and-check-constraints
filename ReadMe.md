# Entity Framework Database First & Check Constraints

Keeping your code base up to date with contemporary versions of what ever technology stack you are using can be time consuming. In this example I'm attempting to demonstrate how to slowly migrate to Entity Framework when you already have an existing database.

This is known as: Database First

Keeping in mind, Entity Framework can be used for multiple purposes:

1. Convenient and direct querying from #C to database
2. Database Schema design and management

Both require backing models / Entities and a Fluent Api however, you can do one without the other and you can keep what ever naming convention you prefer.

> Use Cases

**Use Case One:**

I just want to query data without having to write TSQL and or stored procedures and want to take advantage of relational mapping to include relational data in a single query.

**Use Case Two:**

I already have a suite of data access functionality however, I want a more convenient way to manage my database schema and also facilitate more consistent deployments across multiple environments.

**Use Case Three:**

I want to migrate to a full ENtity Framework Code First strategy however, I already have a database and I don't have weeks or months to devote to a full migration all at once.

This example attempts to demonstrate how this can be done primarily for use case three but you could see how this could work for all use cases listed above.

Regardless of what works for your situation, I highly recommend you implement a the hack migration strategy to reduce risk.  

Whatever the strategy you implement, please ensure strong communication and planning is in place before taking on a hybrid Code and Database First initiative.

## Database Connectivity

Before starting, please ensure the connection strings work for your environment. The default is set up for my database server running in Docker. I've added a Windows environment as well but you may still need to make minor adjustments.

Connection strings reside in:

- EF-DbFirst-CheckConstraints/Entities/DbContexts/DatabaseFirstDbContext.cs
- Stub/appsettings.json
- Stub/Program.cs dependency injection registration


## Reverse Engineering

**Process:**

1. Execute the TSQL script: `Entities/Scripts/DbFirstCreate.sql` to simulate a database first environment 
2. In the console window execute the CLI script below - this should create 3x files:
    - `Entities/DatabaseFirstDbContext.cs`
    - `Entities/tbl_CareTeam.cs`
    - `Entities/tbl_Patient.cs`
    These can be deleted as I've already copied examples to: `Entities/ReverseEngineeringResult/...`
3. Run the Stub project and you should see data printed to the console window that includes 2x Patient records and associated CareTeam

```cli
dotnet ef dbcontext scaffold "Server=tcp:localhost,1433;Initial Catalog=DatabaseFirstDb;Persist Security Info=False;User ID=SA;Password=Local@DevPa55word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer --data-annotations --use-database-names --table database_first.tbl_Patients --table database_first.tbl_CareTeams
```

## Code First with Database First


## Check Constraints

TODO: 