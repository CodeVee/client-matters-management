# Client Matters API
<hr>
  An API for Managing Clients and their associated Matters

# Table of contents

- [Usage](#usage)
- [Installation](#installation)
- [Technology Used](#technology)
- [Validations](#validations)
- [Status Codes](#statuscodes)
    - [Success](#success)
    - [Error](#error)

# Usage

The API is hosted on Heroku and documented using Swagger found [**Here**](https://client-matters.herokuapp.com/swagger/index.html).

# Installation

*For **development only**.*

Clone this repository and navigate inside the project folder and restore the dependencies by running:

```csharp
dotnet restore
```

After installing the dependencies, build the project by executing:

```csharp
dotnet build
```

 Navigate to the WebAPI folder and run

```csharp
dotnet run
```

# Technology Used
The following tools were used in the development of this API

- [AspNetCore 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [MicroElements.Swashbuckle.FluentValidation](https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation)
- [FluentValidation.AspNetCore](https://fluentvalidation.net)
- [MySql](https://www.mysql.com)

# Validations 
The following Validation rules should be noted when attempting to create/update a client or a matter

Parameter | Validation
----------|------------
FirstName, LastName | Must be only Alphabetic characters
Code | Must be made up of 2 Uppercase Letters followed by 3 digits
Amount | Must be greater than 0


# StatusCodes

The following Status codes are returned,
### Success
- 200 : With the associated requested data.
- 201 : With the newly created data
- 204 : Returns nothing back

### Error
- 400 : Validation Error
- 404 : The requested entity does not exist
- 500 : Server Error

