using System;
using System.IO;
using UnityEngine;

public class BugReport : MonoBehaviour
{
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            SaveReport(logString, stackTrace);
        }
    }

    void SaveReport(string log, string stack)
    {
        string path = Path.Combine(
            Application.persistentDataPath,
            $"bugreport_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
        );

        string report =
            "===== SYSTEM =====\n" +
            $"Unity: {Application.unityVersion}\n" +
            $"Platform: {Application.platform}\n\n" +

            "===== ERROR =====\n" +
            log + "\n\n" +
            stack + "\n\n" +

            "===== LOGS =====\n" +
            RuntimeLogBuffer.Instance.GetLogs();

        File.WriteAllText(path, report, System.Text.Encoding.UTF8);

        Debug.Log($"[BugReport] Saved: {path}");
    }
}