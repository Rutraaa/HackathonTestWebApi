# needsAPI

This is the backend part of our web application "needs". The project was developed as a test task for the hackathon "BEST::HACKath0n". The goal of the project is to create a web application where users can post requests for help or offer assistance.

## Technologies Used

- **ASP.NET Core 6**: for building the project's backend.
- **Supabase Client**: for connecting to the database and managing data in the application.

## Supabase

[Supabase](https://supabase.com/) is an open-source platform that provides backend services such as a PostgreSQL database, authentication, storage, and real-time functionality. It is designed to be a complete and scalable backend solution that integrates easily with web applications.

In our web API, Supabase is used for several reasons:

- **Data Management**: Supabase offers a PostgreSQL database, which provides a powerful and reliable way to manage application data.
- **Real-Time Capabilities**: Supabase enables real-time functionality, allowing our application to instantly update clients with any changes in data or state.
- **Authentication**: Supabase's authentication features make it easier to handle user sign-ups, logins, and session management securely and efficiently.
- **Ease of Use**: Supabase offers SDKs and client libraries that streamline development and enable seamless integration with the web application.

By leveraging Supabase in our project, we can focus on building features and user experience without spending too much time on complex backend setup and management.

Here is the updated section for the Readme file, written from your perspective in English:

### Getting Started with Supabase

To integrate Supabase into our web API, we set up a connection with the Supabase client and configure its settings. In the `Program.cs` file, the connection and options are set up as follows:

```csharp
var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddScoped(_ => new Client(supaBase.SupaBaseUrl, supaBase.SupaBaseKey, options));
```

In this code snippet:

- A new instance of `SupabaseOptions` is created to configure the client.
- The `AutoRefreshToken` and `AutoConnectRealtime` options are set to `true` to enable automatic token refresh and real-time connections.
- A scoped service is registered in the service collection using the `Client` class from Supabase. The Supabase URL and API key are used for authentication and connection.

Additionally, in our project, entities for each table are stored in the path `BackEnd/Repo`. This allows for organized and efficient data management and operations with Supabase. 
By following these steps, we can establish a strong connection with Supabase and effectively manage our application's data.

Supabase also made it easy to add authentication and authorization to our application. These features provide secure user management, allowing users to sign up, log in, and access protected resources. We will discuss these functionalities in more detail later in the controller section.

## ASP.NET Core

### Using ASP.NET Core Version 6, we have created the following controllers:
- **AnnouncementController**: This controller manages `Announcement` objects. It provides endpoints to handle the creation, retrieval and updating of announcements.

- **AuthConsumerController**: This controller is responsible for authentication and authorization for users seeking help. It provides secure user management features such as sign-up, login, and access control for these users.

- **AuthSupplierController**: This controller is responsible for authentication and authorization for users offering help. It includes functionalities such as sign-up, login, and access control tailored to these users.

These controllers form the core of our application's backend, handling data management and security features for different types of users.

For convenient work with the controllers, a folder structure has been added to store request and response models under the path `BackEnd/Contracts/{controller name}`. This organization helps keep the project tidy and maintainable by grouping models according to the corresponding controller.

## AnnouncementController

The `AnnouncementController` manages `Announcement` objects in our web application. It provides various endpoints to handle creating, retrieving and updating announcements. The controller uses several dependencies and services to interact with Supabase and manage data efficiently.

Authorization

The controller includes an authorization method `IsAuthorized` that checks if the user is authorized using a session from `SupaBaseConnection`. If the user is not authorized, the controller returns an unauthorized response.

## AuthConsumerController

The `AuthConsumerController` handles authentication and authorization for users seeking help. It provides endpoints for user sign-up and sign-in, and utilizes several services for secure and efficient operations.

## AuthSupplierController

The `AuthSupplierController` manages authentication and authorization for users offering help. It provides endpoints for user sign-up and sign-in and uses various services for secure and efficient operations.

## System Requirements

To successfully run the project, please ensure your environment meets the following system requirements:

1. **.NET 6 SDK**: The .NET 6 Software Development Kit (SDK) should be installed on your development machine. It includes the .NET CLI, libraries, and runtime necessary for building and running ASP.NET Core 6 applications.

2. **Operating System**: An operating system that supports .NET 6, such as:
    - Windows 10 or later
    - macOS 10.15 or later
    - Supported Linux distributions (e.g., Ubuntu 20.04, CentOS 8)

3. **IDE (Integrated Development Environment)**: Use an IDE that supports .NET development, such as:
    - Visual Studio 2022 (for Windows)
    - Visual Studio Code (cross-platform)
    - JetBrains Rider (cross-platform)

5. **API Keys and URLs**: Access to API keys and URLs for connecting to Supabase and other third-party services used in the project.

6. **Network Connection**: A stable internet connection is necessary for accessing external services, such as Supabase, and for connecting to remote APIs.

6. **SSL/TLS Certificates**: For secure communication, especially if your application is exposed to the public internet.

Meeting these system requirements will ensure smooth development and deployment of the project.

