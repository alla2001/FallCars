using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.ExceptionServices;
using System;
using ProcessWithOutput;

public class TerminalWindow : EditorWindow

{
    private string myString = "Command";

    public static List<string> commands = new List<string>();

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools1515/Terminal Window")]
    private static void Init()
    {
        // Get existing open window or if none, make a new one:
        TerminalWindow window = (TerminalWindow)EditorWindow.GetWindow(typeof(TerminalWindow));
        StartUpCommand();
        window.Show();
    }

    private bool cmdRunnding = false;
    private string prevtext;

    private void OnGUI()
    {
        if (!IsCommandRunning()) StartUpCommand();

        GUILayout.Label("Terminal", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(Directory.GetCurrentDirectory(), EditorStyles.boldLabel);

        string textentered = EditorGUILayout.TextField("", GUILayout.Height(100));
        EditorGUILayout.EndHorizontal();

        if (textentered != "")
        {
            prevtext = textentered;
        }
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return)
        {
            RunCommand(prevtext);

            prevtext = "";
        }

        foreach (string text in commands)
        {
            GUILayout.Label(text);
        }
    }

    private static StreamReader commandReader;
    private static StreamWriter commandWriter;
    private static ProcessStartInfo ProcessInfo;
    private static Process _Process;
    private static ProcessWrapper pw;
    private System.Threading.Thread ReadThreadThread;

    public static async void StartUpCommand()
    {
        UnityEngine.Debug.Log("start CMD...");

        ProcessInfo = new ProcessStartInfo("cmd.exe", "/k");
        ProcessInfo.CreateNoWindow = true;
        ProcessInfo.UseShellExecute = false;
        ProcessInfo.RedirectStandardOutput = true;
        ProcessInfo.RedirectStandardInput = true;

        _Process = new Process();
        _Process.OutputDataReceived += (sender, args) =>
        {
            UnityEngine.Debug.Log("received output: {0}");
        };
        _Process.StartInfo = ProcessInfo;
        _Process.Start();

        commandWriter = _Process.StandardInput;
        commandReader = _Process.StandardOutput;
    }

    public static void RunCommand(string command)
    {
        commandWriter.WriteLine(command);
        commandWriter.Flush();
        GetOut();
    }

    public static bool IsCommandRunning()
    {
        if (_Process == null)
            return false;
        return true;
    }

    public static void GetOut()
    {
        UnityEngine.Debug.Log(commandReader.ReadToEnd());
        _Process.WaitForExit();
        UnityEngine.Debug.Log("[CMD Closed ...]");
    }
}