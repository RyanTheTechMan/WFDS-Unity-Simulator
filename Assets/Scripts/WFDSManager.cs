using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System;

public static class WFDSManager
{
    public static string streamingAssetsPath = null;
    public static string persistentDataPath = null;
    public static bool wfds_running = false;

    public static void callWFDS()
    {
        wfds_running = true;

        Thread wfds_thread = new Thread(startWFDS);
        wfds_thread.Start();
    }

    public static void startWFDS()
    {
        Process wfds_process = new Process();

        wfds_process.StartInfo.FileName = streamingAssetsPath + @"/wfds9977_win_64.exe";
        wfds_process.StartInfo.Arguments = "input.fds";
        wfds_process.StartInfo.WorkingDirectory = persistentDataPath;
        wfds_process.StartInfo.UseShellExecute = false;
        wfds_process.StartInfo.RedirectStandardOutput = true;
        wfds_process.StartInfo.RedirectStandardError = true;
        wfds_process.StartInfo.CreateNoWindow = true;

        // Set up redirected output to be displayed in the Unity console
        wfds_process.OutputDataReceived += (sender, e) =>
        {
            logMessage(e.Data);
        };

        wfds_process.ErrorDataReceived += (sender, e) =>
        {
            logMessage(e.Data);
        };

        // Start the process
        wfds_process.Start();

        // Start the asynchronous read of the streams
        wfds_process.BeginOutputReadLine();
        wfds_process.BeginErrorReadLine();

        wfds_process.WaitForExit();

        wfds_running = false;
    }

    public static void logMessage(string message)
    {
        if (!String.IsNullOrEmpty(message))
        {
            UnityEngine.Debug.Log("WFDS: " + message);
        }
    }

    public static void stopWFDS()
    {
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + @"\" + "input" + ".stop")) { }
    }
}
