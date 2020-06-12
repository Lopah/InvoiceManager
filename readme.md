# Invoice Manager

## Installation & Getting Started

To get this project into working state all you need is Visual Studio (or other editor capable of debugging .NET Core 3.1)

### Creating user secrets
First you need to initialize your user store via:
```
dotnet user-secrets init
```

Then you need to set the API key
```
dotnet user-secrets set "secretKey" "12345"
```
Feel free to set any secret key you want, keep in mind that you need to edit the POSTMAN config otherwise it won't let you in.

## What is this project?

This project was given to me as an assignment to do to showcase my current skills as an ASP.NET Core developer.

This project showcases:
* My ability to design an API (I've created postman tests in docs folder, so it's possible to see endpoints in action)
* Basic knowledge of MVC design pattern and implementation of it
* Implementing the Repository pattern for accessing data
* Mapping between layers via AutoMapper

## Project Requirements (Assignment)

I needed to implement an MVC app with following features:
* Creating/Editing Invoices
* Adding/Removing Invoice Items

For the API I needed 3 endpoints and an additional feature:
* Getting all unpaid invoices
* Paying an invoice (changing an attribute)
* Editing invoice via PATCH request
* Locking the API behind a secret key provided in the Request Header