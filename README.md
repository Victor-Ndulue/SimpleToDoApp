
# SimpleTodoApp

Welcome to the SimpleTodoApp! This application provides a RESTful API for managing a to-do list, allowing users to create, retrieve, update, and delete tasks.

Table of Contents
Getting Started
API Endpoints
Authentication Endpoints
Task Management Endpoints
Project Structure
Running the Application
Contributing
License
Getting Started
To get started with the SimpleTodoApp, clone the repository and navigate to the project directory:

bash
Copy code
git clone https://github.com/yourusername/simpletodoapp.git
cd simpletodoapp
Ensure you have the necessary dependencies installed and run the application:

bash
Copy code
# Install dependencies
dotnet restore

# Run the application
dotnet run
The API will be accessible at http://localhost:5000.

API Endpoints
The API provides endpoints for both authentication and task management.

Authentication Endpoints
1. Create an Account
Endpoint: POST /api/v1/Auth/create-account
Description: Creates a new account.
Request Body:
json
Copy code
{
  "userName": "string",
  "userEmail": "string",
  "password": "string"
}
2. Sign In
Endpoint: POST /api/v1/Auth/sign-in
Description: Grants login access and authentication.
Request Body:
json
Copy code
{
  "userName": "string",
  "password": "string"
}
3. Request Password Reset
Endpoint: POST /api/v1/Auth/request-password-reset
Description: Sends a request to reset the password.
Request Body:
form
Copy code
userEmail: string
4. Reset Password
Endpoint: POST /api/v1/Auth/reset-password
Description: Resets the user's account password upon validation.
Request Body:
json
Copy code
{
  "userEmail": "string",
  "otp": "string",
  "newPassword": "string"
}
Task Management Endpoints
1. Create a Task
Endpoint: POST /api/tasks/add-task
Description: Creates a new task.
Request Body:
form
Copy code
title: string
description: string
recurrence: enum(values: None, Daily, Weekly, Monthly, Yearly)
dueDateTime: string($date-time)
2. Update a Task
Endpoint: PUT /api/v1/tasks/update-task
Description: Updates an existing task.
Request Body:
form
Copy code
taskId: string
title: string
description: string
recurrence: enum
dueDateTime: string($date-time)
3. Update Task Status
Endpoint: PUT /api/v1/tasks/update-task-status
Description: Updates the status of an existing task.
Request Body:
form
Copy code
taskId: string
taskStatus: enum(value: Pending, InProgress, Completed)
4. Get Tasks
Endpoint: GET /api/v1/tasks/get-user-tasks
Description: Retrieves tasks associated with the user.
Query Parameters:
$filter: Allows filtering of tasks (e.g., by status or due date).
$select: Specifies the fields to return.
$orderby: Specifies sorting criteria.
$count: Includes the total number of tasks.
$top: Specifies the number of tasks to return.
$skip: Specifies how many tasks to skip.
$expand: Expands nested entities.
$format: Specifies the response format (e.g., json or xml).
5. Delete a Task
Endpoint: DELETE /api/v1/tasks/delete-task
Description: Deletes a specific task.
Query Parameters:
query
Copy code
taskId: string
Project Structure
The project follows a standard .NET structure:

arduino
Copy code
SimpleTodoApp/
├── Controllers/
│   └── TasksController.cs
├── Models/
│   └── Task.cs
├── Services/
│   └── TaskService.cs
├── SimpleTodoApp.csproj
└── Program.cs
Folder Descriptions:
Controllers/: Contains API controllers that handle HTTP requests.
Models/: Defines the application's core data models.
Services/: Implements business logic and data access.
SimpleTodoApp.csproj: Defines project dependencies and build configurations.
Program.cs: The entry point of the application.
Running the Application
To run the application locally:

Ensure you have the .NET SDK installed.
Clone the repository and navigate to the project directory.
Install dependencies and run the application:
bash
Copy code
dotnet restore
dotnet run
Access the API at http://localhost:5000.
Contributing
Contributions are welcome! Please follow these steps:

Fork the repository.
Create a new branch for your feature or fix.
Commit your changes and push them to your fork.
Submit a pull request to the main repository.
Ensure that your code adheres to the project's coding standards and includes appropriate tests.

License
This project is licensed under the MIT License. See the LICENSE file for more details.
