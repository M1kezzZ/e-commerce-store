# E-Commerce Store Application

## Overview
This project is a proof-of-concept E-Commerce store built using modern web development technologies. It demonstrates the integration of a .Net backend with a React frontend, utilizing Redux for state management, and Material UI for styling.

## Features
- Backend built with .Net 7
- Frontend developed with React 18
- Client-side state management using Redux
- Styling with Material UI
- TypeScript for type safety and development efficiency
- Entity Framework for database operations
- ASP.NET Core Identity for authentication
- React Router 6 for navigation

## Key Functionalities
- Developer environment setup
- .Net WebAPI application creation using the dotnet CLI
- React single-page application for the user interface
- Database queries and updates with Entity Framework
- User login and registration with ASP.NET Identity
- Client-side routing with React Router
- Object mapping with Automapper
- UI components with Material Design
- Reusable form components with React Hook Form
- Data operations: paging, sorting, searching, and filtering
- Order creation from the shopping basket
- Payment processing with Stripe (3D Secure)
- Application deployment to Heroku

## Installation and Setup

### Prerequisites
- .Net 7 SDK
- Node.js
- SQL Server (or another compatible database)

### Steps

1. **Clone the repository**
    ```bash
    git clone https://github.com/your-username/e-commerce-store.git
    cd e-commerce-store
    ```

2. **Backend Setup**
    - Navigate to the `backend` directory
    - Restore .Net dependencies
      ```bash
      dotnet restore
      ```
    - Apply Entity Framework migrations
      ```bash
      dotnet ef database update
      ```
    - Run the .Net application
      ```bash
      dotnet run
      ```

3. **Frontend Setup**
    - Navigate to the `frontend` directory
    - Install Node.js dependencies
      ```bash
      npm install
      ```
    - Start the React application
      ```bash
      npm start
      ```

4. **Environment Variables**
    - Configure the necessary environment variables in the `.env` files for both backend and frontend, such as database connection strings, Stripe API keys, etc.

## Usage
After setting up the project, you can access the application by navigating to `http://localhost:3000` in your browser. You will be able to register, log in, browse products, add items to the shopping basket, and complete purchases using Stripe.

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request or open an issue.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements
This project was inspired by various online resources and tutorials on .Net, React, and web development best practices.

