 # LibraryAPI
 Brief description - library API service with functions for managing books, authors and user accounts.
 ## Table of Contents
 * [Functionality](#functionality)
 * [Technology Stack](#technology-stack)
 * [Before Running](#before-running)
 * [Notes](#notes)

## Functionality:

For unauthorized users:
- Registration and authorization

For authorized users:
- adding, deleting, changing authors
- retrieve list of all books
- retrieve a certain book by its Id or ISBN
- adding a new book
- change information about an existing book
- book deletion
 
 ## Technology Stack:
 - .Net 7.0
 - Entity Framework Core
 - MS SQL
 - AutoMapper
 - JWTBearer
 - Swagger
 - Serilog
 - FluentValidation

## Before Running

#### 1. Check out the repository
You can clone an existing repository to your local computer, or to a codespace:

```sh
$ git clone git@github.com:kopliness/LibraryAPI.git
```
Then change directory to `LibraryAPI`:
```sh
$ cd LibraryAPI/Library.API
```

#### 2.Find and open a file `appsettings.json`

Change the connection string
```bash
"ConnectionStrings": {
    "DefaultConnection": "Data Source=(yourServer);Integrated Security=True;Initial Catalog=Library;MultipleActiveResultSets=True;TrustServerCertificate=True"
  }
```
Apply the migrations to your database with the command
```sh
$  dotnet ef database update
```
Launch the application with the command
```sh
$  dotnet run --project .\Library.API.csproj --launch-profile https
```
#### 3.Go to the application page
You can go to the application page at this link

https://localhost:7148/swagger/index.html

## Notes
  1. A 3-layer architecture was chosen as the architecture for this assignment. This architecture includes 3 layers: presentation layer, business logic layer, data access layer. There are also 2 additional layers: common and tests.
  2. In addition to repositories, services for more convenient interaction were implemented.
  3. Guid type was chosen for Id due to more convenience.
  4. An unauthorized user will get a 401 error when trying to perform any operation.
  5. Midvars have been created for input validation and exception handling.
  6. Passwords are encrypted by algorithm before entering into the database.
  7. When authorization takes place, the password is decrypted and compared to the entered password.
  8. Added swagger annotations for more detailed descriptions of endpoints.
  9. Also described status codes that are returned after endpoints are executed.


  
