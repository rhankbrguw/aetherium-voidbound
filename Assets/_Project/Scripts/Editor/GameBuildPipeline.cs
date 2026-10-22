using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Aetherium.Core.Constants;

public static class GameBuildPipeline
{
    private const string LinuxOutputPath = "Builds/Linux/AetheriumVoidbound.x86_64";
    private const string WindowsOutputPath = "Builds/Windows/AetheriumVoidbound.exe";

    [MenuItem("Tools/Aetherium/Build/Build Linux Standalone")]
    public static void BuildLinux()
    {
        ExecuteBuild(BuildTarget.StandaloneLinux64, LinuxOutputPath);
    }

    [MenuItem("Tools/Aetherium/Build/Build Windows Standalone")]
    public static void BuildWindows()
    {
        ExecuteBuild(BuildTarget.StandaloneWindows64, WindowsOutputPath);
    }

    private static void ExecuteBuild(BuildTarget target, string outputPath)
    {
        EnsureDirectoryExists(outputPath);
        string[] scenes = GetActiveScenePaths();

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = target,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        ProcessBuildResult(report, outputPath);
    }

    private static string[] GetActiveScenePaths()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled && !string.IsNullOrEmpty(s.path))
            .Select(s => s.path)
            .ToArray();

        if (scenes.Length > 0)
        {
            return scenes;
        }

        return Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);
    }

    private static void EnsureDirectoryExists(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private static void ProcessBuildResult(BuildReport report, string outputPath)
    {
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"{StringConstants.MSG_BUILD_SUCCESS} {outputPath} ({summary.totalSize / 1048576} MB - v{AppConfig.APP_VERSION})");
            HandleBatchmodeExit(0);
            return;
        }

        Debug.LogError($"{StringConstants.ERR_BUILD_FAILED} Total errors: {summary.totalErrors}");
        HandleBatchmodeExit(1);
    }

    private static void HandleBatchmodeExit(int exitCode)
    {
        if (Application.isBatchMode)
        {
            EditorApplication.Exit(exitCode);
        }
    }
}
