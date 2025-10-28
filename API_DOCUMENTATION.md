# RomCom API Documentation

## Overview
RomCom API is a social media API similar to Discord, Amino, and Instagram. It provides comprehensive functionality for user management, posts, groups, and dashboard features.

## Project Structure

### Controllers
- **AuthController**: Handles authentication, login, logout, and password management
- **UserController**: Manages user profiles, following/followers, and user search
- **PostController**: Handles post creation, likes, comments, and feed management
- **GroupController**: Manages groups, group membership, and group posts
- **DashboardController**: Provides dashboard overview, notifications, and statistics

### Services
- **LoginService**: Authentication and user management
- **UserService**: User profile and social features
- **PostService**: Post management and social interactions
- **GroupService**: Group management and membership
- **DashboardService**: Dashboard data and analytics

### Repositories
- **AuthRepository**: Database operations for authentication
- **UserRepository**: User profile and social data operations
- **PostRepository**: Post and media data operations
- **GroupRepository**: Group and membership data operations
- **DashboardRepository**: Dashboard and analytics data operations

## API Endpoints

### Authentication
- `POST /api/Auth/Login` - User login
- `POST /api/Auth/RefreshToken` - Refresh JWT token
- `POST /api/Auth/ChangePassword` - Change user password
- `POST /api/Auth/Logout` - User logout
- `GET /api/Auth/GetCurrentUser` - Get current user info
- `GET /api/Auth/health/database` - Database health check

### User Management
- `GET /api/User/GetProfile` - Get current user profile
- `GET /api/User/{userId}` - Get user profile by ID
- `PUT /api/User/UpdateProfile` - Update user profile
- `POST /api/User/CreateUser` - Create new user account
- `GET /api/User/SearchUsers` - Search users
- `GET /api/User/{userId}/followers` - Get user followers
- `GET /api/User/{userId}/following` - Get user following
- `POST /api/User/{userId}/follow` - Follow a user
- `DELETE /api/User/{userId}/follow` - Unfollow a user

### Post Management
- `POST /api/Post/CreatePost` - Create a new post
- `GET /api/Post/{postId}` - Get post by ID
- `GET /api/Post/user/{userId}` - Get user posts
- `GET /api/Post/feed` - Get user feed
- `GET /api/Post/group/{groupId}` - Get group posts
- `POST /api/Post/{postId}/like` - Like a post
- `DELETE /api/Post/{postId}/like` - Unlike a post
- `DELETE /api/Post/{postId}` - Delete a post
- `GET /api/Post/trending` - Get trending posts

### Group Management
- `POST /api/Group/CreateGroup` - Create a new group
- `GET /api/Group/{groupId}` - Get group by ID
- `GET /api/Group/my-groups` - Get user's groups
- `GET /api/Group/search` - Search groups
- `POST /api/Group/{groupId}/join` - Join a group
- `DELETE /api/Group/{groupId}/leave` - Leave a group
- `GET /api/Group/{groupId}/members` - Get group members
- `PUT /api/Group/{groupId}` - Update group
- `DELETE /api/Group/{groupId}` - Delete group
- `GET /api/Group/trending` - Get trending groups

### Dashboard
- `GET /api/Dashboard/GetOverview` - Get dashboard overview
- `GET /api/Dashboard/activity` - Get activity feed
- `GET /api/Dashboard/notifications` - Get notifications
- `PUT /api/Dashboard/notifications/{notificationId}/read` - Mark notification as read
- `PUT /api/Dashboard/notifications/read-all` - Mark all notifications as read
- `GET /api/Dashboard/statistics` - Get user statistics
- `GET /api/Dashboard/recent-activity` - Get recent activity

## Database Structure

### Master Database (RomComMaster)
- **auth.tbl_users**: User accounts and authentication
- **auth.tbl_roles**: User roles and permissions
- **auth.tbl_refreshTokens**: JWT refresh tokens

### Main Database (RomComMain)
- **user.tbl_userProfiles**: User profile information
- **user.tbl_userSettings**: User preferences and settings
- **post.tbl_posts**: Social media posts
- **post.tbl_postMedia**: Post media attachments
- **post.tbl_postLikes**: Post likes
- **comment.tbl_comments**: Post comments
- **comment.tbl_commentLikes**: Comment likes
- **group.tbl_groups**: Groups/communities
- **group.tbl_groupMembers**: Group membership
- **friendship.tbl_friendships**: User relationships
- **notification.tbl_notifications**: User notifications
- **message.tbl_messages**: Direct messages
- **message.tbl_messageThreads**: Message threads

## Getting Started

### Prerequisites
- .NET 6.0 or later
- SQL Server
- Visual Studio or VS Code

### Setup
1. Clone the repository
2. Update connection strings in `appsettings.json`
3. Run the database scripts to create tables and stored procedures
4. Build and run the API project
5. Use the provided `TestEndpoints.http` file to test the API

### Configuration
Update the following settings in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string"
  },
  "AppSettings": {
    "JWTSecret": "Your JWT secret key (minimum 32 characters)",
    "TokenExpiryHours": 24
  }
}
```

## Testing
Use the provided `TestEndpoints.http` file to test the API endpoints. Make sure to:
1. First login to get a JWT token
2. Use the token in the Authorization header for protected endpoints
3. Test the database health check endpoint to verify connectivity

## Features Implemented
- ✅ User authentication and JWT tokens
- ✅ User profile management
- ✅ Post creation and management
- ✅ Group/community management
- ✅ Following/followers system
- ✅ Dashboard with statistics
- ✅ Notifications system
- ✅ Search functionality
- ✅ Trending content
- ✅ Activity feeds
- ✅ Database health monitoring

## Next Steps
- Implement real-time notifications using SignalR
- Add file upload functionality for media
- Implement advanced search with filters
- Add message/chat functionality
- Implement admin panel
- Add analytics and reporting
- Implement content moderation
- Add mobile app support
