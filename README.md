Hospital Management System

This is a Hospital Management System built using ASP.NET MVC. The system aims to manage the operations of a hospital, including patient records, appointments, staff management, and billing.

## Features

- **Patient Management**: Add, update, and delete patient records.
- **Appointment Scheduling**: Schedule, update, and cancel appointments.
- **Staff Management**: Manage hospital staff details.
- **Billing System**: Generate and manage patient bills.
- **Authentication**: Secure login for staff and administrators.

## Technologies Used

- **ASP.NET MVC**: For building the web application.
- **Entity Framework**: For database operations.
- **SQL Server**: As the database.
- **Bootstrap**: For responsive design.
- **jQuery**: For client-side scripting.


## Installation

1. **Clone the repository**:
    git clone https://github.com/yourusername/hospital-management-system.git

2. **Open the solution**:
    Open the `HospitalManagementSystem.sln` file in Visual Studio.

3. **Restore NuGet packages**:
    In Visual Studio, go to `Tools` > `NuGet Package Manager` > `Manage NuGet Packages for Solution` and restore the packages.

4. **Update the database connection string**:
    Update the connection string in the `appsettings.json` file to point to your SQL Server instance.
   "DefaultConnection": "Server=[Your_Server];Database=Hospital_MangementDb;Trusted_Connection=True;MultipleActiveResultSets=true"

