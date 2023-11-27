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

## How to use
1. First of all, you need to register.
![image](https://github.com/kopliness/LibraryAPI/assets/92124944/890d483b-db66-464f-b103-8c40efd286df)
2. After you have completed the previous step, you need to obtain your JWT-Token. You can do this by using this endpoint
![image](https://github.com/kopliness/LibraryAPI/assets/92124944/5440ebd6-1012-4abf-b359-6e679e2f4ea5)
3. After that, you need to copy the JWT-Token and paste it into the "Authorize" window, but don't forget to put the word "Bearer" at the beginning.
![image](https://github.com/kopliness/LibraryAPI/assets/92124944/3102e93f-c21e-4619-b928-6584214d4847)
4. Now your account is authorized. Go to adding books.
5. First of all, let's add the necessary author.
   ![image](https://github.com/kopliness/LibraryAPI/assets/92124944/5ebcd54a-43a6-4a1c-ab5f-e33c961b1e0f)
6. Next, you need to get the ID of the author you added. Скопируйте authorId.
   ![image](https://github.com/kopliness/LibraryAPI/assets/92124944/83b52614-6d29-40fe-a1cb-f560394f0fa7)
7. Now you need to add your book. Add properties to your book, and in the "authors" property, paste the authorId you copied in.
   ![image](https://github.com/kopliness/LibraryAPI/assets/92124944/447b0345-6673-4c4b-8473-9beaafad49b2)
8. All right! You have completed the task, now you can continue to perform the book operations you need.

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


  
