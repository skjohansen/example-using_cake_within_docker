var target = Argument("target", "Publish API");

Task("Prebuild Clean").Does(()=> {
    DotNetClean(".");
});

Task("Restore NuGet packages").IsDependentOn("Prebuild Clean").Does(()=>{
    DotNetRestore();
});

Task("Build API").IsDependentOn("Restore NuGet packages").Does(() => {
    var buildSettings = new DotNetBuildSettings(){
        NoRestore = true
    };
    DotNetBuild("WeatherApi.csproj", buildSettings);
});

Task("Publish API").IsDependentOn("Build API").Does(()=>{
    var publishSettings = new DotNetPublishSettings(){
        NoBuild = true,
        NoRestore = true
    };
    DotNetPublish("WeatherApi.csproj",publishSettings);
});

RunTarget(target);