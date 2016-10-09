<!-- section start -->

<!-- attr: {id: 'title', class: 'slide-title', hasScriptWrapper: true} -->

# Entity Framework Performance
<div class="signature">
    <p class="signature-course">Databases</p>
    <p class="signature-initiative">Telerik Software Academy</p>
    <a href="http://academy.telerik.com" class="signature-link">http://academy.telerik.com</a>
</div>

<!-- section start -->
<!-- attr: { id:'table-of-contents', class:'table-of-contents' } -->
# Table of Contents
* [SQL Profilers](#what-is-sql-profiler)
* [The N+1 Query Problem](#the-n1-query-problem)
* [Incorrect Use of ToList()](#incorrect-use-of-tolist)
* [Incorrect use of SELECT *](#incorrect-use-of-select-)
* [Deleting objects faster with native SQL](#deleting-entities)

<!-- section start -->
<!-- attr: { id:'what-is-sql-profiler', class:'slide-section', showInPresentation:true } -->
<!-- # SQL Profilers
## How to Trace All Executed SQL Commands? -->

# What is SQL Profiler
* `SQL Profilers` intercept the SQL queries executed at the server side
  * Powerful tools to diagnose the hidden Entity Framework queries
* SQL Server has "SQL Server Profiler" tool
  * Part of Enterprise / Developer edition (paid tool)
* A free SQL Profiler exists for SQL Server:
  * Express Profiler: http://expressprofiler.codeplex.com
  * Easy-to-use, open-source, lightweight, powerful, … and works!

<!-- attr: { class:'slide-section table-of-contents', showInPresentation:true } -->
<!-- # Express Profiler
## [Demo]()

<!-- section start -->
<!-- attr: { id:'the-n1-query-problem', class:'slide-section', showInPresentation:true } -->
<!-- # The N+1 Query Problem
## What is the N+1 Query Problem and How to Avoid It? -->

# The N+1 Query Problem
* What is the N+1 Query Problem?
  * Imagine a database that contains tables Products, Suppliers and Categories
    * Each product has a supplier and a category
  * We want to print each Product along with its Supplier and Category:

```cs
foreach (var product in context.Products)
{
  Console.WriteLine("Product: {0}; {1}; {2}",
    product.ProductName, product.Supplier.CompanyName,
    product.Category.CategoryName);
}
```

<!-- attr: { hasScriptWrapper:true, showInPresentation:true } -->
<!-- # The N+1 Query Problem -->
* This code will execute N+1 SQL queries:

```cs
foreach (var product in context.Products)
{
  Console.WriteLine("Product: {0}; {1}; {2}",
    product.ProductName, product.Supplier.CompanyName,
    product.Category.CategoryName);
}
```

<div class="fragment" style="padding-top:10%">
  <div class="balloon" style="width:250px; left:67%; top:22%" >One query to retrive the products</div>
  <div class="balloon" style="width:250px; left:25%; top:47%" >Additional N queries to retrieve the category for each product</div>
  <div class="balloon" style="width:250px; left:70%; top:40%" >Additional N queries to retrieve the supplier for each product</div>

* Imagine we have 100 products in the database
  * That's ~ 201 SQL queries -> `very slow!`
  * We could do the same with a single SQL query
</div>

<!-- attr: { hasScriptWrapper:true } -->
# Solution to the N+1 Query Problem
* Fortunately there is an easy way in EF to avoid the N+1 query problem:
<div style="margin-top:5%"></div>

```cs
foreach (var product in context.Products.
  Include("Supplier").Include("Category"))
{
  Console.WriteLine("Product: {0}; {1}; {2}",
    product.ProductName, product.Supplier.CompanyName,
    product.Category.CategoryName);
}
```

<div class="fragment">
  <div class="balloon" style="width:500px; left:40%; top:40%" >Using `Include(…)` method only one SQL query with join is made to get the related entities</div>
  <div class="balloon" style="width:500px; left:40%; top:80%" >No additional SQL queries are made here for the related entities</div>
</div>

<!-- attr: { class:'slide-section table-of-contents', showInPresentation:true } -->
<!-- # Solution to the N+1 Query Problem -->
## [Demo]()

<!-- section start -->
<!-- attr: { id:'incorrect-use-of-tolist', class:'slide-section', showInPresentation:true } -->
<!-- # Incorrect Use of `ToList()`
## How ToList() Can Significantly Affect the Performance -->

<!-- attr: { style:'font-size:0.95em' } -->
# Incorrect Use of `ToList`
* In EF invoking `ToList()` executes the underlying SQL query in the database
  * Transforms `IQueryable<T>` to `List<T>`
  * Invoke `ToList()` as late as possible, after all filtering, joins and groupings
* Avoid such code:
  * This will cause all order details to come from the database and to be filtered later in the memory

```cs
List<Order_Detail> orderItemsFromTokyo =
  northwindEntities.Order_Details.ToList().
  Where(od => od.Product.Supplier.City == "Tokyo").ToList();
```

<!-- attr: { class:'slide-section table-of-contents', showInPresentation:true } -->
<!-- # Incorrect Use of ToList() -->
## [Demo]()

<!-- attr: {  id:'incorrect-use-of-select-', class:'slide-section table-of-contents', showInPresentation:true } -->
# Incorrect Use of `SELECT *`
## [Demo]()

<!-- section start -->
<!-- attr: { id:'deleting-entities', class:'slide-section', showInPresentation:true } -->
<!-- # Deleting Entities Faster with Native SQL Query -->

<!-- attr: { style:'font-size:0.95em' } -->
# Deleting Entities
* Deleting entities (slower):
  * Executes `SELECT` + `DELETE` commands

```cs
NorthwindEntities northwindEntities = new NorthwindEntities();
var category = northwindEntities.Categories.Find(46);
northwindEntities.Categories.Remove(category);
northwindEntities.SaveChanges();
```

* Deleting entities with native SQL (faster):
  * Executes a single `DELETE` command

```cs
NorthwindEntities northwindEntities = new NorthwindEntities();
northwindEntities.Database.ExecuteSqlCommand(
  "DELETE FROM Categories WHERE CategoryID = {0}", 46);
```

<!-- attr: { class:'slide-section table-of-contents', showInPresentation:true } -->
<!-- # Deleting Entities Faster with Native SQL Query -->
## [Demo]()

<!-- section start -->
<!-- attr: { id:'questions', class:'slide-section', showInPresentation:true } -->
<!-- # Questions
## Databases -->
[link to the forum]()
