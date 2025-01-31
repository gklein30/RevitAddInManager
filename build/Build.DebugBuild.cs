using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using System.Diagnostics;
using static Nuke.Common.IO.FileSystemTasks;

internal partial class Build
{
    private Target DebugBuild => _ => _
         .Executes(() =>
         {
             EnsureCleanDirectory(ArtifactsDirectory);

             Debugger.Launch();

             foreach (var projectName in Projects.Where(x => x.Equals("RevitAddinManager")))
             {
                 var project = BuilderExtensions.GetProject(Solution, projectName);
                 var binDirectory = ((AbsolutePath)new DirectoryInfo(project.GetBinDirectory()).FullName) / $"AddIn 2025 Debug R25";

                 FileSystemTasks.CopyDirectoryRecursively(binDirectory, (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) / "Autodesk" / "Revit" / "Addins" / "2025"
                     ,DirectoryExistsPolicy.Merge, FileExistsPolicy.Overwrite);
             }
         });
}