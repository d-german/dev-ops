var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./DevOps/DevOps.sln";

Task("Restore")
    .Does(() =>
    {
        Information("Restoring packages");
        DotNetCoreRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    //.IsDependentOn("Version")
        .Does(() =>
    {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            NoRestore = true
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true
        });
    });

RunTarget(target); // similar to Main() in C# program