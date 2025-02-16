# Entity Framework - A Hybrid Database and Code First Approach & Check Constraints

Keeping your code base up to date with contemporary versions of what ever technology stack you are using can be time consuming. In this example I'm attempting to demonstrate how to slowly migrate to Entity Framework when you already have an existing database.

This is known as: Database First

Keeping in mind, Entity Framework can be used for multiple purposes:

1. Convenient and direct querying with #C
2. Database Schema design and management

Both require backing models, otherwise known as Entities and a Fluent Api however, you can do one without the other (CRUD or Schema management) and you can keep what ever naming convention you prefer.

> Use Cases

**Use Case One:**

I just want to query data without having to write TSQL and or stored procedures and want to take advantage of relational mapping to include relational data in a single query.

**Use Case Two:**

I already have a suite of data access functionality however, I want a more convenient way to manage my database schema and also facilitate more consistent deployments across multiple environments.

**Use Case Three:**

I want to migrate to a full Entity Framework Code First strategy however, I already have a database and I don't have weeks or months to devote to a full migration all at once.

This example attempts to demonstrate how this can be done primarily for use case three but you could see how this could work for all use cases listed above.

Regardless of what works for your situation, I highly recommend you implement a the hack migration strategy to reduce risk. 

**Hack Migration Strategy:**

Like I mentioned above, you could just use Entity Framework for interoperability. That being said, someone could start implementing migrations. Whilst I tried this out and got an error:

CareTeam and Patient tables already exists. 

To resolve this, Entity Framework's model snapshot: /Migrations/{WhatEverYouCalledYourFluentApi}ModelSnapshot simply needs to know about this change and a record needs to be inserted into the `__EFMigrationsHistory` table. To do this:

1. Create a migration: `dotnet ef migrations add HackMigrationForPatientAndCareTeam`
2. Comment out the actual auto generated migration commands in both the Up and Down
3. Execute the migration: `dotnet ef database update`

Now you have removed the errors and if required later, you can start adding schema migrations with Entity Framework. 

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

Results of the above command can be found in: `Entities/ReverseEngineeringResult/...`

## Code First with Database First

Since the initial success of taking advantage of Entity Framework's reverse engineering to make both the `Patient` and `CareTeam` tables available for CRUD operations, I've now switched to a Code First approach and added the table: `Observations`.

Because I also implemented the **Hack Migration Strategy** I can now consistently and safely deploy this schema migration to any database.

## Check Constraints

I was unaware you could add constraints to database table columns, mitigating the responsibility to the database to implement the `check constraint` to ensure only specified values where allowed. 

After I had seen this in a LinkedIn post, I had to try it out:

https://github.com/efcore/EFCore.CheckConstraints/blob/main/EFCore.CheckConstraints/CodeAnnotations.cs

You could also just add an empty Entity Framework schema migration and add:

```c#
//  Based on a table added with EF's Code First approach: [code_first].[Observations]

ALTER TABLE [code_first].[Observations]  WITH CHECK ADD  CONSTRAINT [CK_Observations_Status_Enum] CHECK  (([Status]>=(0) AND [Status]<=(3)))
GO
ALTER TABLE [code_first].[Observations] CHECK CONSTRAINT [CK_Observations_Status_Enum]
GO
```


## Conventions

I created 2x custom schemas to differentiate between database and code first:

- database_first
- code_first

Both the Patient and CareTeam tables implemented naming conventions I have experienced from the past:

- The need to append table names with `tbl_{Name}`
- Hungarian notation:
  - INT iColumnName
  - VARCHAR vchColumnName
  - DATETIME dteColumnName
  - and many more

To add to this I just wanted to demonstrate how you can configure Entity Framework to translate schema, table names and column names to something more contemporary with out even needing to make hard changes. Letting Entity Framework do all the heavy lifting.

From this:

From this:

```sql
CREATE TABLE database_first.tbl_CareTeams (
    iCareTeamID INT IDENTITY(1, 1) NOT NULL,
    vchName NVARCHAR(150) NOT NULL,
    CONSTRAINT PK_tbl_CareTeams_iCareTeamId PRIMARY KEY CLUSTERED (iCareTeamID)
);
```


to this in C#:

```c#
[DataContract]
public abstract class EntityBase : IEntity
{
	[Key]
	[DataMember]
	public virtual int Id { get; set; }
}

[Table("tbl_CareTeams", Schema = "database_first")]
public class CareTeam : EntityBase
{
    [Key]
    [Column("iCareTeamID")]
    [DataMember]
    public override int Id { get; set; }
    
    [Column("vchName")]
    [DataMember]
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(150, ErrorMessage = "Name exceeds {1} characters")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
```
