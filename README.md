# DaveDemo
Dave wanted to store a bunch of data. He wanted something free. 

This is a demo project to show how that could be done.

## Make it all work
This assumes you're using VSCode - anything else should work too
[https://code.visualstudio.com/](https://code.visualstudio.com/)

When in VSCode click on the extensions button (bottom on the left, square icon)

It might list Recommended extensions, if not the elipisis gives you that option, C# should be on the list, Install it if it's not already.

The project also uses .NET Core - you can install it from here
[https://www.microsoft.com/net/core](https://www.microsoft.com/net/core)

If you're on Windows make sure to download and install the one titled ".NET Core SDK for Windows", if you're on another platform follow the instructions.

After you install .NET Core restart VSCode so it knows about .NET Core

Once back in VSCode with the project open go to View -> Integrated Terminal and run the following two commands

```shell
dotnet restore
dotnet run
```

dotnet restore downloads and fetches everything you need to get compiled

dotnet run builds and runs your application

The code as is takes about five minutes to run and you'll end up with a data.db file around 450 MB

Good Luck