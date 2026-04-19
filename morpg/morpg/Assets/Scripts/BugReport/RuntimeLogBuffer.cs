using System;
using System.Text;
using UnityEngine;

public class RuntimeLogBuffer : MonoBehaviour
{
    public static RuntimeLogBuffer Instance;

    private StringBuilder logBuilder = new StringBuilder();
    private const int MaxLength = 10000; // 너무 길어지는 것 방지

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Application.logMessageReceived += HandleLog;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logBuilder.AppendLine($"[{type}] {logString}");

        if (type == LogType.Error || type == LogType.Exception)
        {
            logBuilder.AppendLine(stackTrace);
        }

        // 너무 길어지면 앞부분 자르기
        if (logBuilder.Length > MaxLength)
        {
            logBuilder.Remove(0, logBuilder.Length - MaxLength);
        }
    }

    public string GetLogs()
    {
        return logBuilder.ToString();
    }
}