# Mvc_Project

This repository contains my first MVC project. The project follows a layered architecture pattern and is organized into the following parts:

- **DAL (Data Access Layer):** Handles data storage, retrieval, and communication with the database.
- **BLL (Business Logic Layer):** Contains business rules, validations, and core application logic.
- **PL (Presentation Layer):** Manages the user interface and user interactions.

## Table of Contents

- [Introduction](#introduction)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Features](#Features)
- [Installation](#installation)
- [Running the Project](#running-the-project)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## Introduction

This is my first foray into building an MVC project. The application demonstrates the use of the Model-View-Controller design pattern to separate concerns, making it easier to manage and scale the project as needed.

## Project Structure

The project is structured into three main layers:

- **assignement_3.DAL:** Contains all data access code, including database connectivity and queries.
- **assignement_3.BLL:** Holds the business logic and rules of the application.
- **assignement_3.PL:** Manages the presentation layer. This is where the controllers, views, and client-side assets (HTML, CSS, JavaScript) reside.

The solution file `assignement_3.sln` is located in the root directory and ties all the layers together.

## Technologies Used

- **C#:** Primary programming language used for the application.
- **HTML/CSS/JavaScript:** Used for building the web interface.
- **ASP.NET MVC:** Implements the MVC architecture (assuming ASP.NET MVC or a similar framework based on the project structure).

## Prerequisites

Before running the project, ensure that you have the following installed on your development machine:

- [Visual Studio](https://visualstudio.microsoft.com/) (2019 or later recommended) or a compatible IDE.
- .NET Framework (or .NET Core/5/6 depending on your project configuration) with the appropriate ASP.NET and Web Development workloads.
- SQL Server (if the DAL requires a database instance) or an alternative database system as configured in the project.

## Features

- **Dynamic Role-Based System:** Supports multiple user roles that can be dynamically configured.
- **Authentication:** 
  - Standard login and signup functionality.
  - Social login with Google and Facebook.
- **Password Recovery:** 
  - "Forgot Password" feature via email.
  - SMS-based password recovery.
- **CRUD Operations:** 
  - Manage roles, employees, and departments with full Create, Read, Update, Delete operations.
- **AJAX Search:** 
  - Asynchronous search functionality for improved user experience.
- **Responsive Design:** 
  - Ensures optimal viewing and interaction experience across a wide range of devices.

- **Data Validation:** 
  - Implements robust data validation to ensure data integrity and security.


##Demo
Demo Link : 	companymvc.tryasp.net

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/ziadrad/Mvc_Project.git


2.**Open assignement_3.sln in Visual Studio.**

3. **Restore NuGet packages:**

- Visual Studio should automatically restore the necessary NuGet packages upon opening the solution. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

4.**Configure the database (if applicable):**

-If your application requires a database connection, update the connection string in the appropriate configuration file ( appsettings.json).

## Contributing

Contributions, suggestions, and improvements are welcome! Feel free to fork the repository and submit a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.



## Acknowledgements

- Thanks to all the resources and tutorials that helped in building this project.
- Special thanks to Route Academy for support and continuous learning.
