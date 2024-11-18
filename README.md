# Elasticsearch Integration Demo - C# Web API Project

This project demonstrates integrating Elasticsearch with a .NET Core Web API. It includes examples on setting up Elasticsearch, indexing documents, and performing search queries—ideal for applications requiring fast, scalable search capabilities and handling large datasets.

## Table of Contents
- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setting Up Elasticsearch](#setting-up-elasticsearch)
- [Project Setup](#project-setup)
  - [1. Clone the Repository](#1-clone-the-repository)
  - [2. Configure Elasticsearch Settings](#2-configure-elasticsearch-settings)
  - [3. Install Dependencies](#3-install-dependencies)
- [Configuration](#configuration)
  - [Index Settings](#index-settings)
- [API Endpoints](#api-endpoints)
  - [1. POST /products/createProduct](#1-post-productscreateproduct)
  - [2. GET /products/searchProduct?query={query}](#2-get-productssearchproductqueryquery)
  - [3. GET /products/getProduct/{id}](#3-get-productsgetproductid)
  - [4. DELETE /products/deleteProduct/{id}](#4-delete-productsdeleteproductid)
- [License](#license)

## Overview

The project is a Web API built using ASP.NET Core and integrates Elasticsearch to manage products and allow users to perform CRUD operations and searches. Built on .NET 6, it uses Elastic.Clients.Elasticsearch for communication with the Elasticsearch cluster.

## Technologies Used
- C# / .NET 6: The core framework for the API.
- Elastic.Clients.Elasticsearch: Official Elasticsearch client for .NET.
- ASP.NET Core Web API: Framework for creating RESTful API endpoints for search and CRUD operations.

## Features

- Connects to an **Elasticsearch** cluster using **Elastic.Clients.Elasticsearch** library.
- Allows indexing, retrieving, and deleting documents (Products) in Elasticsearch.
- Implements search functionality for querying indexed data.
- Configurable **index settings** such as **shards**, **replicas**.

## Prerequisites

- **Elasticsearch**: You must have an Elasticsearch instance running, either locally or on a remote server.
- **.NET SDK**: This project uses .NET 6. Make sure you have it installed on your system.

You can download and install Elasticsearch from [here](https://www.elastic.co/downloads/elasticsearch).

## Setting Up Elasticsearch

1. Download and install Elasticsearch on your machine or use a hosted Elasticsearch service (like [Elastic Cloud](https://cloud.elastic.co/)).
2. Start the Elasticsearch service on your local machine or use a remote server's URI.
3. Ensure your Elasticsearch instance is running on the default port (`9200`), or modify the URI in the configuration accordingly.

## Project Setup

### 1. Clone the repository

```bash
git clone https://github.com/PesDuuu/Elasticsearch-CSharp-example-demo.git
cd Elasticsearch-CSharp-example-demo
```

### 2. Configure the Elasticsearch Settings
Open **appsettings.json** and configure the **Elasticsearch** settings section. It should look like this:

```json
{
  "Elasticsearch": {
    "Uri": "http://localhost:9200",  // Change this to your Elasticsearch instance URI
    "IndexName": "products",        // Index name for storing documents
    "Shards": 1,                    // Number of shards for the index
    "Replicas": 1                   // Number of replicas for the index
  }
}
```

### 3. Install Dependencies

```console
dotnet add package Elastic.Clients.Elasticsearch
```
## Configuration

### Index Settings

The project configures Elasticsearch with the following settings:

- **Shards**: Controls how the data is distributed across nodes. Set to 1 by default.
- **Replicas**: Determines how many copies of your data are stored. Set to 1 by default.

## API ENDPOINTS

### CRUD Operations

### 1. POST /products/createProduct
Description: Indexes a new product into Elasticsearch.

**Request Body:**

```json
{
  "id": 1,
  "name": "Sample Product",
  "description": "This is a sample product.",
  "price": 19.99
}
```

### 2. GET /products/searchProduct?query={query}
Description: Searches for products based on the name field.

**Query Parameters:**

- **query**: The search term to query for in the name field.
**Example:**

```bash
GET /products/search?query=sample
```

### 3. GET /products/getProduct/{id}
Fetches a specific product by id.

**Response:**

```json
{
  "id": 1,
  "name": "Sample Product",
  "description": "This is a sample product.",
  "price": 19.99
}
```

### 4. DELETE /products/deleteProduct/{id}
Description: Delete a product by its ID.

## License

Feel free to modify the configuration to suit your Elasticsearch cluster and project requirements. This demo application is designed to help you understand how Elasticsearch can be integrated with a C# Web API and can be extended to support more complex use cases and workflows.
