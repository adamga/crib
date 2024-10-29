# crib

## Real-time Updates with SignalR

To handle real-time updates for both players, we use SignalR, a library for ASP.NET that simplifies adding real-time web functionality.

### SignalR Features

* Allows the server to push updates to the clients instantly.
* Manages communication between the server and the clients through a SignalR hub.
* Broadcasts updates to both players when a player performs an action, ensuring both players see the same game state in real-time.

### SignalR Setup

1. Add SignalR services in the `ConfigureServices` method in `Startup.cs`.
2. Add SignalR middleware in the `Configure` method in `Startup.cs`.
3. Create a SignalR hub class named `GameHub` to manage communication between the server and clients.
4. Add methods in `GameHub` for broadcasting game updates to clients.

## Video Chat with WebRTC

To add a video chat element to the game, we use WebRTC, a free, open-source project that provides web browsers and mobile applications with real-time communication via simple application programming interfaces (APIs).

### WebRTC Features

* Handles video and audio streaming.
* Integrates into the .NET Core application.
* Manages the signaling process for establishing WebRTC connections between the players using SignalR.

### WebRTC Setup

1. Add JavaScript code to handle WebRTC connections in `wwwroot/js/webrtc.js`.
2. Use SignalR for the signaling process in `wwwroot/js/webrtc.js`.
3. Create a new component for the video chat interface and embed it into the main game screen in `Views/Game/Index.cshtml`.
