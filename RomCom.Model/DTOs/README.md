# DTOs Organization Guide

This directory contains all Data Transfer Objects (DTOs) for the RomCom API, organized by feature/domain.

## 📁 Structure

DTOs are organized using a **feature-based approach** with **Request/Response separation**:

```
DTOs/
├── Auth/                      # Authentication & Authorization
│   ├── Requests/              # Incoming data (login, change password, etc.)
│   └── Responses/             # Outgoing data (tokens, user info, etc.)
├── User/                      # User Management (future)
│   ├── Requests/
│   └── Responses/
├── Post/                      # Posts & Content (future)
│   ├── Requests/
│   └── Responses/
├── Friend/                    # Friend System (future)
│   ├── Requests/
│   └── Responses/
└── Message/                   # Messaging (future)
    ├── Requests/
    └── Responses/
```

## 🎯 Naming Conventions

### Request DTOs
- **Format**: `{Action}{Feature}RequestDto`
- **Examples**:
  - `LoginRequestDto.cs`
  - `CreatePostRequestDto.cs`
  - `UpdateUserProfileRequestDto.cs`
  - `SendMessageRequestDto.cs`

### Response DTOs
- **Format**: `{Entity}Dto` or `{Action}{Feature}ResponseDto`
- **Examples**:
  - `AuthDto.cs` - User authentication data
  - `TokenResponseDto.cs` - Token response
  - `UserProfileDto.cs` - User profile data
  - `PostDto.cs` - Post data

## ➕ Adding New Features

When adding a new feature (e.g., Posts, Comments, Friends):

1. **Create feature folder**: `DTOs/{Feature}/`
2. **Create subfolders**: `Requests/` and `Responses/`
3. **Add request DTOs**: For data coming FROM the client
4. **Add response DTOs**: For data going TO the client
5. **Update namespaces**: Use `RomCom.Model.DTOs.{Feature}.{Requests|Responses}`

### Example: Adding Friend System

```csharp
// DTOs/Friend/Requests/SendFriendRequestDto.cs
namespace RomCom.Model.DTOs.Friend.Requests
{
    public class SendFriendRequestDto
    {
        [Required]
        public required int TargetUserId { get; set; }
        public string? Message { get; set; }
    }
}

// DTOs/Friend/Responses/FriendDto.cs
namespace RomCom.Model.DTOs.Friend.Responses
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTimeOffset FriendsSince { get; set; }
    }
}
```

### Example: Adding Post Feature

```
DTOs/Post/
├── Requests/
│   ├── CreatePostRequestDto.cs
│   ├── UpdatePostRequestDto.cs
│   └── DeletePostRequestDto.cs
└── Responses/
    ├── PostDto.cs
    ├── PostListResponseDto.cs
    └── PostDetailResponseDto.cs
```

## ✅ Best Practices

1. **Keep DTOs simple** - Only properties, no logic
2. **Use validation attributes** - `[Required]`, `[MinLength]`, `[EmailAddress]`, etc.
3. **Separate concerns** - Request and Response DTOs should be separate classes
4. **Use nullable types** - For optional properties use `?`
5. **Use `required` keyword** - For mandatory properties (C# 12)
6. **Consistent naming** - Follow the naming conventions above
7. **Document complex DTOs** - Add XML comments for clarity
8. **Avoid circular references** - Don't create circular dependencies between DTOs

## 🚫 What NOT to Include

- ❌ Business logic or methods
- ❌ Data access code
- ❌ Complex validation logic (beyond attributes)
- ❌ Entity/domain models (those go in `RomCom.Abstractions`)
- ❌ Database-specific attributes (like `[Column]`, `[Table]`)

## 📚 Related Layers

- **Controllers** (`RomCom.Api`) - Use Request DTOs for `[FromBody]` parameters
- **Services** (`RomCom.Service`) - Transform between DTOs and domain entities
- **Repository** (`RomCom.Repository`) - May return Response DTOs from queries
- **Entities** (`RomCom.Abstractions`) - Database models (separate from DTOs)

## 🔄 DTO vs Entity

| Aspect | DTOs | Entities |
|--------|------|----------|
| **Purpose** | Data transfer (API contracts) | Domain/database models |
| **Location** | `RomCom.Model` | `RomCom.Abstractions` |
| **Validation** | API validation attributes | Database constraints |
| **Changes** | Can change for API versions | Tied to database schema |
| **Serialization** | Always serializable | May have complex relationships |

## 📖 Examples from Current Codebase

### Authentication Request
```csharp
// RomCom.Model.DTOs.Auth.Requests
public class LoginRequestDto
{
    [Required(ErrorMessage = "Username or Email is required")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}
```

### Authentication Response
```csharp
// RomCom.Model.DTOs.Auth.Responses
public class TokenResponseDto
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTimeOffset ExpiresAt { get; set; }
    public required AuthDto User { get; set; }
}
```

## 🏗️ Future Feature Folders

Ready-to-use folder structure for common features:

- ✅ **Auth/** - Authentication & Authorization (Implemented)
- 📦 **User/** - User profiles, settings, management
- 📦 **Post/** - Posts, articles, content creation
- 📦 **Comment/** - Comments on posts
- 📦 **Friend/** - Friend requests, friendships
- 📦 **Message/** - Direct messaging, chat
- 📦 **Notification/** - Push notifications, alerts
- 📦 **Media/** - File uploads, images, videos
- 📦 **Search/** - Search requests and results

---

**Last Updated**: October 27, 2025  
**Maintainer**: RomCom Development Team  
**Pattern**: Feature-Based DTOs with Request/Response Separation

