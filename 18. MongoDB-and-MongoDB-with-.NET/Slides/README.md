<!-- section start -->

<!-- attr: {id: 'title', class: 'slide-title', hasScriptWrapper: true} -->
# MongoDB and using MongoDB with .NET
## Welcome to the JSON-stores world

<div class="signature">
    <p class="signature-course">Databases</p>
    <p class="signature-initiative">Telerik Software Academy</p>
    <a href="http://academy.telerik.com" class="signature-link">http://academy.telerik.com</a>
</div>

<!-- section start -->

<!-- attr: {id: 'table-of-contents', class:'table-of-contents'} -->
# Table of Contents

*   MongoDB Overview
    *   Installation and drivers
    *   Structure and documents
*   Hosting locally MongoDB
*   DB Viewers
    *   Command-line interface (CLI), UMongo
*   Queries

<!-- section start -->

<!-- attr: {class: 'slide-section'} -->
#   MongoDB Overview
##  Document-based databases

#   MongoDB

*   MongoDB is an open-source document store database
    *   Stores JSON-style objects with dynamic schemas
    *   Support for indices
    *   Has document-based queries
        *   CRUD operations

#   Installing MongoDB

*   Download the latest MongoDB from http://https://mongodb.org/downloads
    *   Installers for all major platforms
*    When installed, MongoDB needs a driver to be usable with each platform:
    *   One for .NET, another for Node.JS, etc...


<!-- attr: {class: 'slide-section'} -->
#   Installing MongoDB
##  [Demo](http://)

#   Running MongoDB locally

*   Once installed, MongoDB can be started
    *   Navigate to the MongoDB install folder in CMD
    *   And run: `$ mongod`

        *   May be necessary to create folder `c:\data\db`  

*   While running, MongoDB can be used with any language and platform
    *   Provided there is a driver for this platform

<!-- attr: {class: 'slide-section'} -->
#   Running MongoDB locally
##  [Demo](http://)

<!-- section start -->

<!-- attr: {class: 'slide-section'} -->
#   Using MongoDB in .NET
##  Packages and stuff

#   Using MongoDB in .NET

*   To use MongoDB from .NET, a MongoDB driver for .NET must be installed
    *   The official is http://docs.mongodb.org/ecosystem/drivers/csharp/

*   To install it, just type in Package Management Console:

    ```
    PM> Install-Package MongoDB.Driver -Version 2.0.1
    ```

#   Using MongoDB in .NET

*   Once installed, you can connect and use MongoDB:

```cs
var connectionString = "mongodb://localhost";
//var connectionString = "URL_IN_THE_WEB";

var client = new MongoClient(connectionString);
var db = client.GetDatabase("books");
```

<!-- attr: {class: 'slide-section'} -->
#   Using MongoDB in .NET
##  [Demo](http://)

<!-- section start -->

#   MongoDB Viewers

*   MongoDB is an open-source DB system
    *   So there are many available viewers
*   Some, but not all are:  
    *   MongoDB CLI
        *   Comes with installation of MongoDB
        *   Execute queries inside the CMD/Terminal
        *   The most commonly used
    *   MongoVUE & UMongo
        *   Provide UI to view, edit are remove DB documents
        *   Kind of slappy

<!-- attr: {class: 'slide-section'} -->
#   MongoDB Viewers
##  Demo

<!-- section start -->

<!-- attr: {class: 'slide-section'} -->
#   MongoDB Driver APIs: Insert, Read, Update, Remove
##  [Demo](http://)

<!-- section start -->

<!-- attr: {id: 'questions', class: 'slide-questions', showInPresentation: true} -->
# MongoDB and MongoDB in .NET
## Questions
