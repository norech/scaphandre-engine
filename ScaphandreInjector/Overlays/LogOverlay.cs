﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreInjector.Overlays
{
    /// <summary>
    /// This overlays shows the game logs when 
    /// </summary>
    public class LogOverlay : MonoBehaviour
    {
        private static List<string> logs = new List<string>();
        public static bool showLogs = false;
        private static Vector2 scrollPos = Vector2.zero;
        private static string searchPattern = "";

        public static void AddLog(string log)
        {
            logs.Add(log);
        }

        GUIStyle textStyle;

        public static void ListenForLogs() {
            Application.logMessageReceived += LogMessageReceived;
        }

        private static void LogMessageReceived(string logString, string stackTrace, LogType type)
        {
            AddLog(type.ToString().ToUpper() + ": " + logString + "\n\n" + stackTrace);
        }

        public void OnEnable()
        {
            scrollPos = new Vector2(scrollPos.x, 30000 - Screen.height + 30);

            textStyle = new GUIStyle()
            {
                alignment = TextAnchor.LowerLeft,
                fontSize = 18,
            };

            textStyle.richText = false;

            textStyle.normal.textColor = new Color(1, 1, 1);
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.F4) && Input.GetKeyDown(KeyCode.L))
            {
                showLogs = !showLogs;
            }
        }

        void OnGUI()
        {
            if (!showLogs) return;

            int width = Screen.width;
            int height = Screen.height;
            
            GUI.Box(new Rect(0, 0, width, height), GUIContent.none);

            scrollPos = GUI.BeginScrollView(new Rect(0, 0, width, height - 31), scrollPos, new Rect(0, 0, width - 10, 30000));
            GUI.Label(new Rect(0, 0, width - 10, 30000), "LOGS - F4 + L\n\n" + string.Join("\n\n",
                logs.Where(log => searchPattern == "" || log.ToLower().Contains(searchPattern.ToLower())).ToArray()
            ), textStyle);

            GUI.EndScrollView();

            searchPattern = GUI.TextField(new Rect(0, height - 30, width, 30), searchPattern);
        }
    }
}