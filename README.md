# Nominatim Client

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A simple C# client for interacting with the Nominatim API, which is a search engine for the OpenStreetMap database.

## Overview

This client allows you to perform location-based searches using the Nominatim API. It supports structured queries and handles API responses, providing a convenient interface for integrating Nominatim functionality into your C# applications.


## Getting Started

1. Install the package via NuGet (not available yet).
2. Create an instance of `NominatimClient`.
3. Use the `SearchAsync` method to perform location searches.

## Installation

```bash
# Clone the repository
git clone https://github.com/yourusername/NominatimClient.git

# Navigate to the project directory
cd NominatimClient

# Build the solution
dotnet build
```
## Usage

```bash
// Create an instance of NominatimClient
var nominatimClient = new NominatimClient();

// Create a structured query model
var searchModel = new StructuredQuerySearchModel
{
    Amenity = "restaurant",
    City = "New York",
    Country = "USA"
};

// Perform a location search
var result = await nominatimClient.SearchAsync(searchModel);

// Handle the result
if (result.Status == Status.Success)
{
    var apiResponse = result.ApiResponse;
    // Process the API response
}
else
{
    var errorMessage = result.ErrorMessage;
    // Handle the error
}
