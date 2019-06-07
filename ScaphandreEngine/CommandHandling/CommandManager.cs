using ScaphandreEngine.CommandHandling;
using ScaphandreEngine.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ScaphandreEngine
{
    public static class CommandManager
    {
        /// <summary>
        /// This FieldInfo represents the DevConsole.commands field
        /// </summary>
        internal static FieldInfo commandsField = typeof(DevConsole).GetField("commands", BindingFlags.NonPublic | BindingFlags.Static);

        /// <summary>
        /// This property contains a reference to the private nested type DevConsole.CommandData
        /// </summary>
        internal static Type commandDataType = typeof(DevConsole).GetNestedType("CommandData", BindingFlags.NonPublic);
        public static Dictionary<string, /*DevConsole.CommandData*/object> UnmanagedCommands => (Dictionary<string, /*DevConsole.CommandData*/object>)commandsField.GetValue(null);

        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();

        private static bool IsScaphandreCommand(string commandName)
        {
            return Commands.ContainsKey(commandName);
        }

        private static Command GetCommand(string commandName)
        {
            return Commands.GetOrDefault(commandName, null);
        }

        internal static void RegisterCommand(Mod mod, Command command)
        {
            command.Setup(mod);

            var commandName = command.GetName();
            var modName = mod.Info.id;

            if(!new Regex("^[A-Za-z0-9_]+$").IsMatch(commandName))
            {
                throw new Exception("The name '" + commandName + "' is invalid for a command.");

            }

            var successfullyRegisteredNames = new List<string>();

            var registeredNames = new string[]
               {
                   commandName,
                   modName + ":" + commandName
               };

            foreach (var name in registeredNames)
            {
                if (!Commands.ContainsKey(name))
                {
                    Commands.Add(name, command);
                    successfullyRegisteredNames.Add(name);
                }
                else
                {
                    Debug.LogError("A command named '" + name + "' is already registered. Ignoring.");
                }
            }

            if(successfullyRegisteredNames.Count > 0)
            {
                Debug.Log("Successfully registered '" + string.Join("', '", successfullyRegisteredNames.ToArray()) + "' commands for " + command.GetType().Name + " class.");
            }
        }

        public static bool ResolveCommand(Component console, string value)
        {
            var separator = new char[]
            {
                ' ',
                '\t'
            };

            var text = value.Trim();
            var array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 0)
            {
                return false;
            }

            var commandName = array[0];

            if (!IsScaphandreCommand(commandName))
            {
                return false;
            }

            bool caseSensitive = false;
            bool combineArgs   = false;

            string[] args = null;
            if (combineArgs)
            {
                text = text.Substring(commandName.Length).Trim();
                if (!caseSensitive)
                {
                    text = text.ToLower();
                }
                if (text.Length > 0)
                {
                    args = new string[]
                    {
                        text
                    };
                }
            }
            else if (array.Length > 1)
            {
                args = array;
            }

            var command = Commands.GetOrDefault(commandName, null);

            try
            {
                if (args != null)
                {
                    command.Execute(args);
                }
                else
                {
                    command.Execute(new string[] { });
                }
            }
            catch (Exception ex)
            {
                command.Mod.Logger.Error(ex);
                return false;
            }
            return true;
        }

        private static void ExecuteCommand(string command)
        {
            DevConsole.SendConsoleCommand(command);
        }
    }
}
