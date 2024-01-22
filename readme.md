# Server-side Giraffe HTML with Fable sample
## Introduction
This project shows how to create a "conventional" server-side-rendered HTML app with client-side JavaScript using F# for both server and client. This approach may fit for projects using technologies such as HTMX.

## File structure
* **Client** - Contains client-side specific F#.
* **Server** - Contains ASP .NET server code using Giraffe + Saturn.
* **Shared** - Contains shared F# to be used on both client and server.

## Step-by-step guide

### Infrastructure
1. Add a [package.json file](package.json) and install to create the required [package-lock.json](package-lock.json) file. This file contains Vite, as well as some configuration recommended in the official [Vite docs](https://vitejs.dev/guide/build.html#library-mode) for "library mode".
1. Add the Fable tool (see [dotnet-tools.json](.config/dotnet-tools.json)).

### Client
This folder contains a standard F# console application with the following modifications:

1. Add the following node to the `fsproj` file:
    ```xml
    <DefineConstants>FABLE_COMPILER</DefineConstants>
    ```
1. You can compile the project using:
    ```bash
    dotnet fable --cwd src/client -s -o transpiled
    ```
    This will compile the project and put the raw JS outputs into the `transpiled` folder.
1. Configure Vite using the appropriate [configuration file](/src/Client/vite.config.js). This file instructs vite to process the `transpiled` folder and put the bundled + minified outputs in the `src/server/dist` folder. You can run vite with:
    ```bash
    cd src/client
    npx vite build
    ```
1. You can run both Fable and Vite together:
    ```bash
    dotnet fable --cwd src/client -s -o transpiled --run npx vite build
    ```

### Server
This folder contains a standard console application and uses **Giraffe.ViewEngine** to dynamically build HTML on the server. Key elements required for accessing the Fable-rendered JS are:

#### 1. Serve up Fable-rendered JS
```fsharp
use_static "dist"
```
#### 2. Import Fable-rendered JS
```fsharp
head [] [ script [ _type "module"; _src "app.js" ] [] ]
```

### Putting it all together - dev mode
1. Build client assets and store results in Server static files folder:
    ```bash
    dotnet fable watch --cwd src/client -s -o transpiled --run npx vite build --watch
    ```
    > Note the uses of `watch` and `--watch`, which will rebuild and rebundle the JS whenever client F# changes. You will still to reload the browser though!
1. Run the server (ideally in a separate terminal):
    ```bash
    dotnet watch run
    ```
1. Your client-side F# will now be available in HTML rendered by Giraffe e.g.
    ```fsharp
    document.getElementById("btnClick").onclick <- (fun _ -> window.alert (Shared.Say.hello "isaac"))
    ```