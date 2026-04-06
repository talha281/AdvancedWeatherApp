# Advanced Weather App

An advanced weather forecast application built with ASP.NET Core MVC and designed as a portfolio-ready project that combines strong .NET fundamentals with AI-assisted development workflow.

This project showcases:

- Real-world MVC application structure
- Clean separation of controllers, services, options, DTOs, and view models
- External API integration with Open-Meteo
- Responsive dashboard-style UI for current, hourly, and daily forecasts
- Practical use of prompt engineering and Codex to accelerate planning, implementation, and delivery

## Why I Built This

I wanted to build a project that demonstrates two things together:

1. My hands-on experience as a .NET developer building structured web applications from scratch
2. My ability to adopt AI-assisted engineering workflows in a practical, delivery-focused way

This app is not just a weather dashboard. It is also a demonstration of how I use prompt engineering, task decomposition, and AI collaboration to move faster while still keeping the project organized and production-minded.

## What This Project Includes

- City-based weather search
- Current weather conditions
- Hourly forecast view
- 7-day forecast view
- Weather highlights such as humidity, wind, pressure, UV index, visibility, sunrise, and sunset
- Friendly error handling and fallback messaging
- In-memory caching for forecast and geocoding requests
- Clean MVC architecture for maintainability and future extension

## Tech Stack

- ASP.NET Core MVC
- .NET 9
- C#
- Open-Meteo API
- Razor Views
- Bootstrap
- Custom CSS and JavaScript

## Architecture Overview

The application is organized around clear responsibilities:

- `Controllers`
  Handles routing and request flow
- `Services`
  Contains forecast, geocoding, mapping, and business logic
- `Models/OpenMeteo`
  DTOs for external API responses
- `Models/ViewModels`
  Dashboard-focused models for the UI layer
- `Options`
  Configuration binding for provider settings
- `Views`
  Razor-based UI for the weather dashboard

## AI-Assisted Development

This project is also part of my AI engineering journey.

I used prompt engineering with Codex as a development collaborator to help with:

- breaking down the project into execution steps
- parallelizing backend, frontend, verification, and project-management tasks
- generating and refining implementation structure
- speeding up UI composition and service-layer setup
- validating build issues and resolving integration mismatches

Important note:
I used AI as an accelerator, not as a replacement for engineering judgment. The architecture, direction, integration decisions, debugging, and final delivery still required developer oversight and technical decision-making.

## What This Demonstrates About My Skills

- Strong understanding of ASP.NET Core MVC application design
- Ability to integrate third-party APIs cleanly
- Practical approach to scalable code organization
- Comfort with modern AI-assisted development workflows
- Ability to use prompt engineering to improve productivity and execution quality
- Focus on shipping working software quickly without losing structure

## Running the Project

Clone the repo and run:

```powershell
dotnet restore
dotnet build
dotnet run --project .\AdvancedWeatherForecast.Web\AdvancedWeatherForecast.Web.csproj
```

Then open the local URL shown in the terminal.

## Future Improvements

- browser geolocation support
- weather alerts from richer providers
- air quality integration
- unit and integration tests
- saved favorite cities
- temperature unit switching
- charts for hourly trends

## Portfolio Note

I'm actively exploring how AI tools can improve software delivery in real engineering environments. This project reflects that mindset: using AI thoughtfully for speed, clarity, and execution while still applying software engineering fundamentals to produce a clean and usable result.
