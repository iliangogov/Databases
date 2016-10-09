<!-- section start -->

<!-- attr: {id: 'title', class: 'slide-title', hasScriptWrapper: true} -->
# JSON in .NET
## Parsing JSON with .NET'

<div class="signature">
    <p class="signature-course">Databases</p>
    <p class="signature-initiative">Telerik Software Academy</p>
    <a href="http://academy.telerik.com" class="signature-link">http://academy.telerik.com</a>
</div>

<!-- section start -->

<!-- attr: {id: 'table-of-contents', class:'table-of-contents'} -->
# Table of Contents

*   The JSON data format
    *   Rules and features
    *   Usage
*   JSON.NET Overview
    *   Installation and usage
    *   LINQ-to-JSON
    *   JSON to XML and XML to JSON

<!-- section start -->

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   The JSON Data Format
##   What is JSON? -->

#   The JSON Data Format

*   JSON (JavaScript Object Notation) is a lightweight data format
    *   Human and machine-readable
    *   Based on the way to create objects in JS
    *   Platform independent - can be used with any programming language

<!-- attr: {style: 'font-size:40px'} -->
#   JSON Rules

*   The JSON format follows  the rules of object literals in JS

*   **Strings**, **numbers** and **Booleans** are valid JSON

```javascript
"this is string and is valid JSON"
```

*   **Arrays** are valid JSON

```javascript
[5, 'string', true] 
```

*   **Objects** are valid JSON

```javascript
{ 
  "firstname": "Doncho",
  "lastname": "Minkov",
  "occupation": "Technical trainer"
}

```

<!-- section start -->

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   Processing JSON in .NET
##   How to parse JSON in .NET? -->

#   Built-in JSON Serializers

*   .NET has **built-in JSON serializer**:
    *   The `JavaScriptSerializer` class, contained in `System.Web.Extensions` assembly
*   `JavaScriptSerializer` has parsing from object to JSON string and vice versa:

```cs
var place = new Place(…);
var serializer = new JavaScriptSerializer();

var jsonPlace = serializer.Serialize(place);
var objPlace = serializer.Deserialize<Place>(jsonPlace);
```

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   Simple Parsing with JavaScript Serializer -->
##   Demo

<!-- section start -->

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   JSON.NET Overview
##   Better JSON parsing than with `JavaScriptSerializer` -->

#   What is JSON.NET?

*   JSON.NET is a library for parsing JSON in .NET
    *   Has better performance than the `JavaScriptSerializer`
    *   Provides **LINQ-to-JSON**
    *   Has an out-of-the-box support for parsing between JSON and XML

<!-- attr: {hasScriptWrapper: true} -->
#   Installing JSON.NET

*   To install JSON.NET run in the Package Manager Console:

```bash
$ Install-Package Newtonsoft.Json
```

*   JSON.NET has two primary methods:
    *   Serialize an object:

    ```cs
    var jsonObj = JsonConvert.SerializeObject(obj);
    ```

    *   Deserialize an object:
    
    ```cs
    var copy = JsonConvert.DeserializeObject<ObjType>(jsonObj);
    ```

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   Serializing and Deserializing Objects with JSON.NET -->
##   Demo

#   JSON.NET Features

*   JSON.NET can be configured to:
    *   **Indent** the output JSON string
    *   To convert JSON to **anonymous types**
    *   To control the **casing** and **properties** to parse
    *   To **skip errors**
*   JSON.NET also supports:
    *   **LINQ-to-JSON**
    *   Direct parse between XML and JSON

<!-- attr: {style: "font-size:40px"} -->
#   Configuring JSON.NET

*   To indent the output string use `Formattingg.Indented`:

```cs
JsonConvert.SerializeObject(place, Formatting.Indented);
```

*   Deserializing to anonymous types:

```cs
var json = @"{ 
               ""fname"": ""Doncho"",
               ""lname"": ""Minkov"",
               ""occupation"": ""Technical Trainer"" 
             }";

var template = new 
{ FName  = "", LName = "", Occupation = "" };
var person = JsonConvert.DeserializeAnonymousType(json, template);
```

*   Must provide a template Object 

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   Configuring JSON.NET -->
##   Demo

<!-- attr: {style: "font-size:40px"} -->
#   JSON.NET Parsing of Objects
*   By default JSON.NET takes **each Property/Field** from the **public interface** of a class and parses it
    *   This can be controlled using attributes:

```cs
public class User
{
  [JsonProperty("user")]
  public string Username { get; set; }

  [JsonIgnore]
  public string Password { get; set; }
}

```

*   `[JsonProperty(...)]` tells the parser that `Username` is `user` in the JSON
*   `[JsonIgnore]` tells the parser to skip the property `Password`


<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   JSON.NET Parsing of Objects -->
##   Demo

<!-- section start -->

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #  LINQ-to-JSON
## Easier way to query JSON objects -->

#  LINQ-to-JSON

*   JSON.NET has a support for **LINQ-to-JSON**

```cs
var jsonObj = JObject.Parse(json);
Console.WriteLine("Places in {0}:", jsonObj["name"]);

jsonObj["places"].Select(
   pl => 
       string.Format("{0}) {1} ({2})",
                     index++,
                     pl["name"],
                     string.Join(", ",  
                             pl["categories"].Select(
                               cat => cat["name"]))))
                 .Print();
```

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   LINQ-to-JSON -->
##   Demo

<!-- section start -->

<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   XML to JSON and JSON to XML
##   Made easy -->

#   XML to JSON and JSON to XML

*   Conversions from JSON to XML are done using two methods:
  *   XML to JSON

```cs
string jsonFromXml = JsonConvert.SerializeXNode(doc);
```

  *   JSON to XML

```cs
XDocument xmlFromJson = JsonConvert.DeserializeXNode(json);
```


<!-- attr: {class: 'slide-section', showInPresentation: true} -->
<!-- #   XML to JSON and JSON to XML -->
##   Demo

<!-- section start -->

<!-- attr: {id: 'questions', class: 'slide-questions', showInPresentation: true} -->
# JSON in .NET
## Questions
