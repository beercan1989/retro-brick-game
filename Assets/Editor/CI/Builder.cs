using System;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Editor.CI
{
    /**
     * Credit to:
     * https://support.unity3d.com/hc/en-us/articles/115000368846-How-do-I-support-different-configurations-to-build-specific-players-by-command-line-or-auto-build-system
     * and
     * https://github.com/Unity-CI/unity3d-ci-example/blob/master/Assets/Scripts/Editor/BuildCommand.cs
     */
    class Builder
    {
        public static void Build()
        {

            Console.WriteLine(":: Performing build");
            
            // Find and get the BuildTarget
            var target = GetBuildTarget();
            
            // Get build name
            var name = GetArgument("buildName");

            // Build the target
            var report = BuildTarget(target, name);
            
            // TODO - Print out the report?

            // Check if the build was successful
            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception($"Build ended with {report.summary.result} status");

            // Check if we've run any build steps.
            if (report.steps.Length <= 0)
                throw new Exception("Build ended with no steps executed");
            
            // Check if we've output any files
            if (report.files.Length <= 0)
                throw new Exception($"Build ended with no output files.");

            Console.WriteLine(":: Done with build");
        }

        /**
         * Lets build the target with a custom output location, this is so we can support building
         * multiple platforms.
         */
        private static BuildReport BuildTarget(BuildTarget buildTarget, string buildName)
        {
            return BuildPipeline.BuildPlayer(
                EditorBuildSettings.scenes,
                $"Build/{buildTarget}/{buildName}",
                buildTarget,
                BuildOptions.None
            );
        }

        /**
         * Lets try and get the BuildTarget from the normal Unity CLI argument.
         */
        private static BuildTarget GetBuildTarget()
        {
            var argument = GetArgument("buildTarget");
            var target = Enum.Parse(typeof(BuildTarget), argument, true);
            return (BuildTarget) target;
        }

        private static string GetArgument(string name)
        {
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains(name))
                {
                    return args[i + 1];
                }
            }
            return null;
        }
    }
}