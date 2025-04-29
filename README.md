# FleetTrack

FleetTrack is a lightweight fleet management web application that helps you:

- **Register & Manage Vehicles**  
  Add, edit and remove vehicles in your fleet, including make, model, plate number and maintenance history.

- **Plan & Track Routes**  
  Define routes by specifying origin and destination addresses; distances are calculated dynamically via the TomTom Routes API.

- **Monitor Maintenance**  
  Keep track of upcoming inspections and service intervals for each vehicle, with configurable reminders and status flags.

---

## 🌟 Features

- **Vehicle CRUD** – Create, read, update and delete vehicles  
- **Route Planner** – Input addresses, fetch turn-by-turn distance & duration via TomTom  
- **Real-Time Distance Calculation** – Automatic recalculation on route edits  
- **Maintenance Scheduling** – Log inspection dates, service types and next-due reminders  
- **Responsive UI** – Built with Bootstrap 5 for mobile-friendly dashboards and forms  
- **Data Persistence** – No-SQL storage in MongoDB for flexible schema evolution

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET MVC (.NET 9)  
- **Database:** MongoDB (hosted locally)  
- **MongoDB Driver:** [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver/) for C#/.NET  
- **Mapping & Routing:** [TomTom Routes API](https://developer.tomtom.com/)  
- **Front-end:** Bootstrap 5
