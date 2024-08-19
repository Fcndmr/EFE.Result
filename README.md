# TS.Result NuGet Package

## Overview
The EFE.Result package is a .NET library designed to encapsulate the result of operations in a consistent manner. It provides a structured approach to handling success and failure states, including support for detailed error reporting. This package is ideal for improving error handling and response consistency across various application layers, particularly in API development.

## Features
- **Generic Result Type**: Facilitates strong typing of operation outcomes, accommodating any data type.
- **Detailed Error Handling**: Supports capturing multiple error messages, including error codes and optional targets.
- **HTTP Status Code Integration**: Aligns operation results with HTTP standards using the ResultStatus enum.
- **Implicit Conversions**: Streamlines result creation through implicit conversion operators.
- **Logging**: Offers a method to log errors directly to the console.

## Installation
You can install the EFE.Result package via the NuGet Package Manager, .NET CLI, or by editing your project file.

### .NET CLI
```csharp
dotnet add package EFE.Result
```

### Package Manager
```csharp
Install-Package EFE.Result
```

## Usage

### Success Cases

- **Successful Operation with Data:**
```csharp
var successResult = new Result<string>("Operation successful.");
```

- **Implicit Conversion for Success:**
```csharp
Result<string> result = "Operation successful.";
```

- **Using the Succeed Method:**
```csharp
Result<string> result = Result<string>.Succeed("Operation successful.");
```

### Failure Cases

- **Failure with Status Code and Multiple Errors:**
```csharp
var errorDetails = new List<ErrorDetail>
{
    new ErrorDetail("Error 1", "400", "Field1"),
    new ErrorDetail("Error 2", "400", "Field2")
};
var errorResult = new Result<string>(ResultStatus.BadRequest, errorDetails);
```

- **Implicit Conversion for Failure:**
```csharp
Result<string> result = (ResultStatus.BadRequest, errorDetails);
```

- **Failure with Single Error Message:**
```csharp
Result<string> result = (ResultStatus.NotFound, new ErrorDetail("Item not found", "404"));
```

- **Using the Failure Method with One Error:**
```csharp
Result<string> result = Result<string>.Failure(ResultStatus.InternalServerError, new ErrorDetail("An error occurred.", "500"));
```

- **Using the Failure Method with Multiple Errors:**
```csharp
var errorDetails = new List<ErrorDetail>
{
    new ErrorDetail("Error 1", "500"),
    new ErrorDetail("Error 2", "500")
};
Result<string> result = Result<string>.Failure(ResultStatus.InternalServerError, errorDetails.ToArray());
```

- **Failure Method with Default 500 Status Code:**
```csharp
Result<string> result = Result<string>.Failure(new ErrorDetail("An unexpected error occurred.", "500"));
```

## Logging Errors
You can log errors using the LogErrors method:
```csharp
if (!result.IsSuccessful)
{
    result.LogErrors();
}
```

## JSON Examples

- **Success Result:**
```csharp
{
  "data": "Operation successful.",
  "errorMessages": null,
  "isSuccessful": true
}
```

- **Error Result:**
```csharp
{
  "data": null,
  "errorMessages": [
    {
      "message": "Username must be at least 3 characters",
      "code": "400",
      "target": "username"
    }
  ],
  "isSuccessful": false
}
```

## Contributing
We welcome contributions! Feel free to open an issue or submit a pull request on our GitHub repository for any suggestions or improvements.

## License
`EFE.Result` is licensed under the MIT License. See the LICENSE file in the source repository for full details