# OperationResultLibrary


---
## Table of Contents

- [Features](#features)
- [How It Works](#how-it-works)
- [License](#license)
## Features
---
- Defines a unified response system with `OperationResult`, `OperationResult<T>` and multi-payload overloads `(T1, T2, T3)`.
- `C# 14` Ready: Built to leverage Extension Static Members for seamless, type-safe custom status codes.
- HTTP-Aligned Status: Predefined `ResponseValue` objects based on standard HTTP codes:
| Status | Code | Description |
| :--- | :--- | :--- |
| **Success** | 200 | Operation completed successfully. |
| **Warning** | 202 | Accepted or completed with warnings. |
| **Invalid** | 400 | Validation or business logic error. |
| **Unauthorized** | 401 | Authentication is required. |
| **Forbidden** | 403 | Permissions issue or access denied. |
| **NotFound** | 404 | The requested resource was not found. |
| **Failed** | 500 | General internal or database failure. |

---
## How It Works

- **Overview**
    The `OperationResult` base class encapsulates the outcome of an operation, including a `ResponseValue` that indicates the status of the operation. The generic version, `OperationResult<T>`, extends this functionality by allowing the inclusion of data of type `T` when the operation is successful. This allows for a consistent way to handle results across different operations, whether they return data or not.
    ``` csharp
        public OperationResult<User> GetUser(int id)
        {
            var user = _db.Users.Find(id);
    
            if (user is null)
                return OperationResult<User>.Failure(ResponseValue.NotFound, "User not found.");

            return OperationResult<User>.Success(user);
        }
    ```
- **Extension Blocks**
  The ResponseValue class defines a `DefineCustomResponse` method designed to be used with the new Extension Members in C# 14. This allows you to add domain-specific error codes directly to the ResponseValue type without modifying the library source.
  Use the extension keyword to add new static properties to the existing `ResponseValue` class. The isSuccessOverride parameter allows you to explicitly set whether the response is considered successful or not, regardless of the status code.
``` csharp
 public static extension class ResponseValueExtensions
{
    // Define a custom code and explicitly mark it as a success
    public static ResponseValue PartialData => 
        ResponseValue.DefineCustom(206, "PartialData", isSuccessOverride: true);

    // Define a custom error code
    public static ResponseValue CustomDatabaseError => 
        ResponseValue.DefineCustom(1001, "DatabaseConnectionLost", isSuccessOverride: false);
}
```
   -The field `bool? isSuccessOverride =null` allows you to override the default success logic based on the status code. The `IsSuccessCode` field in `ResponseValue` checks a range of codes to determine success (200-299). By providing a value for `isSuccessOverride`, you can explicitly set whether the response is considered successful or not, regardless of the status code.

- **Multiple Payloads & Empty Checks**
Designed for complex data scenarios—such as Dapper queries returning multiple result sets—the library provides overloads for up to three generic types `(T1, T2, T3)`.
- The operation was successful (`IsSuccess` is true).
- The payload (or all payloads in multi-generic versions) is either `null` or an empty `IEnumerable`.

```csharp
// Example: Returns a User and a list of their RecentOrders
public OperationResult<User, IEnumerable<Order>> GetDashboard(int id) 
{ 
    // ... logic to fetch multiple datasets (e.g. Dapper QueryMultiple) ...
    return OperationResult<User, IEnumerable<Order>>.Success(user, orders);
}

var result = GetDashboard(1);

if (result.IsNullOrEmpty)
{
    // This triggers only if the query succeeded but returned NO User AND NO Orders.
}
```

## License

This project is licensed under the **MIT License**.  
See the [LICENSE](LICENSE) file for details.
