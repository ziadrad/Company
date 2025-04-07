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

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/ziadrad/Mvc_Project.git
