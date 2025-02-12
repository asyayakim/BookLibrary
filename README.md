📚 BookLibrary Backend
This is the backend for the BookLibrary project, developed using ASP.NET Core with PostgreSQL as the database. It serves as the backbone of the library system, providing API endpoints for book management, user authentication, loans, and other functionalities.

✨ Features
✅ Manage Books – Add, update, delete, and retrieve book details.
📊 Top 10 Loaned Books – Track and display the most borrowed books.
🔍 Advanced Search – Find books by title, author, year, or genre.
📚 Loan System – Allow users to borrow and return books.
👤 User Management – Create, update, and delete user accounts.
🔑 Authentication & Authorization – Secure user login and role-based access.
🛠 Admin Controls – Manage books and users via an admin panel.
📅 Library Events API – Store and retrieve upcoming events.

🏗 Project Structure
The backend consists of multiple projects:

BookLibrary.Api – The main API project handling HTTP requests.
BookLibrary.Database – Handles database interactions using Entity Framework Core.
BookLibrary.Logic – Contains business logic and service implementations.
🗄 Database
The project uses Postgres for data storage.

📌 Entities:

UserData – Stores user accounts (ID, username, password, role).
Books – Contains book details (title, author, genre, year, etc.).
Loans – Tracks book loans and return status.
Events – Fetch library events and schedules.

📂 API Endpoints

🛡 Authentication

POST /api/auth/register - Register a new user
POST /api/auth/login - Login a user

📖 Book Management

GET /api/Book/fetch-google-books - Fetch books from Google Books API
GET /api/Book - Retrieve all books
POST /api/Book - Add a new book
GET /api/Book/{id} - Retrieve a book by ID
PUT /api/Book/{id} - Update a book by ID
DELETE /api/Book/{id} - Remove a book by ID
DELETE /api/Book/deleteAllFavoriteBooks - Clear all favorite books
DELETE /api/Book/deleteAllLoanedBooks - Clear all loaned books

📚 Loaned Books & Favorites

POST /api/books/loan - Loan a book
GET /api/books/loaned - Retrieve all loaned books
POST /api/books/favorite - Add a book to favorites
DELETE /api/books/deleteBook - Remove a book from the library
GET /api/books/usersData - Retrieve user data
GET /api/books/mostLoaned - Retrieve most loaned books
GET /api/books/showFavorite - Retrieve favorite books
DELETE /api/books/removeFavoriteBook - Remove a book from favorites

📅 Library Events

GET /api/GetEvents - Retrieve library events

👤 User Management

GET /api/UserData - Retrieve all user data
PUT /api/UserData/changeUserData - Update user details

🛠 Admin Management

GET /api/Admin - Retrieve all admins
POST /api/Admin - Create a new admin
GET /api/Admin/{id} - Retrieve an admin by ID
DELETE /api/Admin/{id} - Remove an admin

🔐 Login Management

GET /api/Login - Retrieve login details
POST /api/Login - Login a user
GET /api/Login/{id} - Retrieve login by ID
DELETE /api/Login/{id} - Remove a login record


🛠 Technologies Used
ASP.NET Core – Web API framework
Entity Framework Core – ORM for database handling
C# – Main programming language
Swagger – API documentation
Database: PostgreSQL

🔗 Related Projects
Frontend: BookLibrary Frontend
