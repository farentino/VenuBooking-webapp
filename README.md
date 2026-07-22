# VenuBookin# VenuBooking Web App

## Project Overview

VenuBooking is an ASP.NET Core MVC web application built for managing venues, events, and bookings. The system allows booking specialists to create venues, create events, upload venue images, make bookings, and search/filter booking records.

The application was first developed locally using SQL Server LocalDB and Azurite, then later moved to Azure using Azure SQL Database, Azure Storage Account, and Azure App Service.

---

## Main Features

* Add and view venues
* Upload venue images
* Add and view events
* Assign event types to events
* Create venue bookings
* Prevent double bookings
* Prevent deletion of venues/events linked to active bookings
* Search bookings by Booking ID or Event Name
* Filter bookings by:

  * Event Type
  * Venue
  * Date range
* Store images in Blob Storage
* Display booking information from joined Venue, Event, and Booking tables
* Run locally or in Azure

---

## Technologies Used

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server LocalDB
* Azure SQL Database
* Azurite
* Azure Blob Storage
* Azure App Service
* Bootstrap
* Visual Studio

---

## How to Run the Web App Locally

### 1. Open the Project

Open the project in Visual Studio.

### 2. Check the Database Connection

In `appsettings.json`, make sure the local database connection string is set correctly:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=VenuBookingDB;Trusted_Connection=True;",
  "BlobConnection": "UseDevelopmentStorage=true"
}
```

### 3. Run the SQL Script

Open SQL Server Object Explorer and run the database script to create the required tables:

* Venue
* EventType
* Event
* Booking

This will also insert sample data into the database.

### 4. Start Azurite

If you are testing image uploads locally, open Command Prompt and run:

```cmd
azurite --skipApiVersionCheck
```

Leave this window open while running the app.

### 5. Run the Application

Click the green Run button in Visual Studio.

The web app should open in the browser.

---

## How to Use the Web App

### Venues Page

The Venues page shows all available venues.

Each venue displays:

* Venue ID
* Venue name
* Location
* Capacity
* Venue image

To add a new venue:

1. Click **Create New Venue**
2. Enter the venue name
3. Enter the location
4. Enter the capacity
5. Upload an image
6. Click **Save Venue**

The venue will be saved and displayed on the Venues page.

---

### Events Page

The Events page shows all events in the system.

Each event displays:

* Event ID
* Event name
* Event type
* Start date
* End date
* Image

To add a new event:

1. Click **Create New Event**
2. Enter the event name
3. Select the start date
4. Select the end date
5. Choose an event type
6. Add an image URL if needed
7. Click **Save Event**

The event will be saved and displayed on the Events page.

---

### Bookings Page

The Bookings page shows all booking records in a consolidated view.

The page displays:

* Booking ID
* Venue name
* Venue location
* Venue capacity
* Event name
* Event type
* Event start date
* Event end date
* Booking date

To create a booking:

1. Click **Create Booking**
2. Select a venue
3. Select an event
4. Choose the booking date
5. Click **Save Booking**

If the same venue is already booked for the same date, the system will show an error message and stop the double booking.

---

## Search and Filtering

The Bookings page includes search and filtering features.

Users can search by:

* Booking ID
* Event Name

Users can also filter by:

* Event Type
* Venue
* Date Range

This helps booking specialists quickly find specific bookings without scrolling through all records manually.

---

## Validation Rules

The application includes validation to improve reliability.

### Double Booking Prevention

The system does not allow the same venue to be booked more than once for the same date.

### Delete Restriction

The system prevents users from deleting venues or events that are linked to active bookings.

This protects the database from broken relationships and missing booking information.

---

## Local Blob Storage

During local development, the project uses Azurite to simulate Azure Blob Storage.

Images are uploaded into a local blob container called:

```text
venue-images
```

This allows image upload testing without using live Azure cloud credits.

---

## Azure Cloud Deployment

For the cloud version, the application uses:

### Azure SQL Database

Used to store the Venue, EventType, Event, and Booking data.

### Azure Storage Account

Used to store uploaded venue images in the `venue-images` container.

### Azure App Service

Used to host the ASP.NET Core MVC web application online.

The deployed application uses Azure connection strings instead of local development connection strings.

---

## Important Azure Configuration

In Azure App Service, the following connection strings must be added under Environment Variables:

### DefaultConnection

Used for Azure SQL Database.

### BlobConnection

Used for Azure Storage Account.

The connection string values should not be shared publicly because they contain sensitive information.

---

## Screenshots Required for Submission

The final submission should include screenshots showing:

* The application running locally or on Azure
* The Venues page
* The Events page
* The Bookings page
* Search and filtering working
* Double booking validation
* Delete restriction validation
* Azure SQL Database tables and data
* Azure Storage Account container named `venue-images`
* Uploaded blob images inside the container
* Azure App Service overview page
* The deployed web app running from the Azure URL
* Proof that Azure resources were deleted after completion

---

## Resource Cleanup

After recording the video and taking all screenshots, delete the Azure resources to avoid unnecessary costs.

The resources to delete include:

* Azure App Service
* App Service Plan
* Azure SQL Database
* Azure SQL Server
* Azure Storage Account

If all resources are inside one resource group and only belong to this project, the whole resource group can be deleted.

---

## Conclusion

VenuBooking is a web-based venue booking system that helps manage venues, events, and bookings. It supports image uploads, validation, searching, filtering, and cloud deployment. The project demonstrates how a local ASP.NET Core MVC application can be moved from LocalDB and Azurite to Azure SQL Database, Azure Storage, and Azure App Service.
