#addin nuget:?package=Cake.Docker&version=1.1.2
var target = Argument("target", "Docker information");
var dockerfile = Argument("dockerfile", "dockerfile");
string [] dockerTags = new string[]  {  $"api_local"};
string containerPort = "4567";

Task("Build Docker Image")
.Does(() => {
    Information(dockerfile);
    var settings = new DockerImageBuildSettings { 
        Tag=dockerTags,
        File= dockerfile
    };
    DockerBuild(settings, ".");
});

Task("Docker information").IsDependentOn("Build Docker Image").Does(()=>{
    Information($"Run the image by running:");
    foreach (var dockerTag in dockerTags)
    {
        Information($"\tdocker run -it -p {containerPort}:80 {dockerTag}");
    }    
});

RunTarget(target);