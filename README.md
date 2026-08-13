## About the project

This **API**, developed using **.NET 8**, adopts the principles of **Domain-Driven Design (DDD)** to offer a structured and effective solution for personal expense management. The main objective is to allow users to register their expenses, detailing information such as title, date and time, description, amount, and payment type, with the data being securely stored in a **MySQL** database.

The **API** architecture is based on **REST**, utilizing standard **HTTP** methods for efficient and simplified communication. Additionally, it is complemented by **Swagger** documentation, which provides an interactive graphical interface for developers to easily explore and test the endpoints.

Among the NuGet packages used, **AutoMapper** is responsible for mapping between domain objects and request/response objects, reducing the need for repetitive and manual boilerplate code. **FluentAssertions** is used in unit tests to make assertions more readable, helping write clear and understandable tests. For validations, **FluentValidation** is used to implement validation rules in a simple and intuitive way within the request classes, keeping the code clean and easy to maintain. Finally, **EntityFramework** acts as an ORM (Object-Relational Mapper) that simplifies database interactions, allowing the use of .NET objects to manipulate data directly without the need to handle raw SQL queries.

### Features

- **Domain-Driven Design (DDD)**: Modular structure that facilitates the understanding and maintenance of the application domain.
- **Unit Tests**: Comprehensive tests with FluentAssertions to ensure functionality and quality.
- **Report Generation**: Ability to export detailed reports to **PDF and Excel**, offering an effective and visual analysis of expenses.
- **RESTful API with Swagger Documentation**: Documented interface that facilitates integration and testing for developers.

### Built with

![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

* Visual Studio version 2022+ or Visual Studio Code
* Windows 10+ or Linux/MacOS with [.NET SDK][dot-net-sdk] installed
* MySQL Server

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Images -->
[hero-image]: images/heroimage.png

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
