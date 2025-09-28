# Readify

**Readify** is a fully functional, responsive, and user-friendly e-library web application.  
It is built with **React & TypeScript** on the frontend and **ASP.NET Core** on the backend, connected through REST APIs.



## Project Features

### Core Features

#### Admin

- Manage books: add, update, delete, and view.  
- Manage authors: add, update, delete, and view.  
- Manage genres: add, delete, and view.  
- View registered users and their borrowing history.  

#### User & Admin

- User authentication: **login, logout, and register**.  
- Browse and view books with detailed info.  
- **Search & filter** books.  
- Borrow and return books.  
- View and update personal profile.  
- Track currently borrowed books.  
- View borrowing history.  


### Extra Features

- Borrowing rate limits.  
- Borrowing due date tracking.  
- Notifications for:  
  - Upcoming due dates.  
  - Overdue books.  
  - Newly added books.  



### Future Features

- AI-powered book recommendations (based on borrowing history).  
- Book reservation system.  
- Real-time notification updates.  


## Development Approach

The development of this project was carried out in **7 key phases**:

1. **Defining Project Features** – identified core and extra functionalities.  
2. **Database Design** – structured entities, relationships, and constraints.  
3. **Project Setup**  
   - **Frontend:** React, TypeScript, TailwindCSS, Axios  
   - **Backend:** ASP.NET Core, Dapper, MySQL ( For Database)
4. **Authentication & State Management** – implemented token-based authentication using **JWTs** and configured global state with **Redux**.  
5. **Feature Development** – built admin functionalities first, followed by user features.  
6. **Testing** – validated APIs and UI with **Postman**, **Swagger**, and **browser developer tools**.  
7. **Documentation** – prepared detailed project documentation.

I’ve shared a detailed breakdown of these phases in a series of LinkedIn posts.  
You can start reading from **[Phase 1 here](https://www.linkedin.com/posts/hadyabdallahsafa_fullstackdevelopment-webdevelopment-reactjs-activity-7371797383292284928-HTZk?utm_source=share&utm_medium=member_desktop&rcm=ACoAADy9VLYB3eC6Id_JgPlGzjgt4k6V8fuMstY)**.  



## Backend Architecture & Best Practices

To build this application, I developed around **30 API endpoints** and structured the backend using the **Repository Pattern**.  
This approach separates **data access** from **business logic**, resulting in organized, maintainable, and testable code.



###  Project Structure
- **Controllers** – Handle HTTP requests and responses.  
- **Services** – Contain business logic.  
- **Repositories** – Manage database access.  
- **Models** – Define the structure of data.  
- **DTOs** – Used for two main purposes:  
  1. Transfer data between layers (e.g., controller ↔ service).  
  2. Decouple internal models from external representations.  



### Background Jobs
- Integrated **Hangfire** to schedule background jobs (outside the main request/response flow).  
- Example: Notification feature that sends reminders about due dates and overdue books.

### Data Integrity

- Implemented database transactions in the borrowing feature to ensure atomic operations where borrowing records and available copies update always succeed or fail together.




### Business Logic Highlights
To ensure the app goes beyond basic CRUD, additional rules were enforced:  
- Users cannot borrow more than **3 books at a time**.  
- A user cannot borrow a book that is **already borrowed**.  
- Book copies cannot be updated to a number lower than the **currently borrowed copies**.  


### Error Handling

- Covered edge cases (e.g., invalid inputs, unauthorized access, unavailable resources).  
- Returned **clear and user-friendly messages** for both client-side and server-side errors.  
- Ensured **consistent response formats** for errors and successes.  
- Users are always notified about the **success or failure** of their requests.  


### Security Best Practices
- **Password hashing** for secure credential storage.  
- **Protected routes** based on authentication and role-based access control.  
- **Backend validation** (not relying only on frontend checks).  
- **Parameterized queries** to prevent SQL injection.  
- **User-specific data** is always derived from the authentication token (instead of trusting client-sent IDs).  

## Note

The frontend part of this project is in a separate repository. Make sure to [check it out here](https://github.com/hadysafa/readify) for the complete application.

---

<p align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" alt="ASP.NET Core" width="250"/>
</p>

