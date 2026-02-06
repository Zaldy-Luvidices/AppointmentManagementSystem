# Appointment Management System

## Overview
This project is a time‑boxed technical assessment demonstrating the development of a WPF (Windows Presentation Foundation) desktop application using the MVVM pattern with a REST Web API backend connected to a SQLite database.

## How to Run
1. Open the solution in Visual Studio 2022+.
2. Set both **AppointmentManagementSystem.Api** and **AppointmentManagementSystem.Client** projects as Startup Projects
3. Launch application

## Features Implemented
- View all appointments
- Create new appointment
- Update existing appointment
- Delete appointment

## Database
SQLite database is auto‑created on first run.
Appointment entity includes basic fields such as:
- Id (GUID)
- Title
- Description
- Patient Name
- Scheduled Date

## What I Would Improve With More Time
- Form validation (date conflicts, required fields)
- Appointment search / filtering
- Cancel appointment
- Additional appointment details (schedule time, duration, more patient details)
- CSV export
- Better UI layout
- Better UX styling
- Unit tests
- Pagination
