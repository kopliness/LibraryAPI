 ## Стек технологий:
 - .Net 7.0
 - Entity Framework Core
 - MS SQL
 - AutoMapper
 - JWTBearer
 - Swagger

## Для запуска:
Клонировать репозиторий
```bash
https://github.com/kopliness/LibraryAPI
```
Перейти в папку с проектом
```bash
cd LibraryAPI/Library.Web
```
Изменить строку подключения 
```bash
"ConnectionStrings": {
    "DefaultConnection": "Data Source=(yourServer);Integrated Security=True;Initial Catalog=Library;MultipleActiveResultSets=True;TrustServerCertificate=True"
  }
```
Создать миграции
```bash
Add-Migration FirstMigration
```
Применить миграции
```bash
Update-Database
```
Дальше нужно запустить проект командой 
```bash
  dotnet run --project .\Library.Web\Library.Web.csproj --launch-profile https
```
Перейти на страницу приложения
https://localhost:7148/swagger/index.html
## Функционал:

Для неавторизованных пользователей:
- регистрация и авторизация

Для авторизованных пользователей:
- получение списка всех книг
- получение определённой книги по её Id или ISBN(опционально)
- добавление новой книги
- изменение информации о существующей книге
- удаление книги

## Примечания
  1. В качестве архитектуры для данного задания была выбрана архитектура 3-layer. Данная архитектура включает в себя 3 слоя: слой представления, слой бизнес-логики, слой доступа к данным.
  2. Помимо репозиториев были реализованы сервисы для более удобного взаимодействия.
  3. Для Id был выбран тип Guid за счет большего удобства.
  4. Неавторизованный пользователь при попытке выполнить какую-либо операцию получит ошибку 401.
  5. Для обработки ошибки добавления новой книги с уже имеющимся ISBN реализовано исключение BookExistsException.
  6. Для обработки ошибки с неверным логином или паролем будет вызвано исключение NotFoundException.
  7. Для обработки других ошибок реализован ErrorExceptionHandling.
