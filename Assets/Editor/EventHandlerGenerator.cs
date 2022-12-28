using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public class EventHandlerGenerator
    {
        static EventHandlerGenerator()
        {
            var actions = ReadEventHandler();
            WriteEventHandlerController(actions);
        }

        private static List<string> ReadEventHandler()
        {
            const string path = "Assets/Scripts/Utils/EventHandler.cs";
            var reader = new StreamReader(path);

            var actions = new List<string>();
            while (reader.ReadLine() is { } line)
            {
                if (line.Contains("public static Action"))
                {
                    actions.Add(
                        line
                            .Replace("public static Action", "")
                            .Replace(";", "")
                            .Trim()
                    );
                }
            }

            reader.Close();
            return actions;
        }

        private static void WriteEventHandlerController(List<string> actions)
        {
            const string path = "Assets/Scripts/Utils/EventHandlerController.cs";
            var content =
                "/**\n* THIS SCRIPT IS GENERATED! DON'T MODIFY IT. \n*/\n\nusing UnityEngine;\nnamespace Utils\n{\n\npublic class EventHandlerController : MonoBehaviour\n{\n";
            foreach (var action in actions)
            {
                var actionOnly = action;
                var param = "";
                if (action.Contains("<"))
                {
                    var pFrom = action.IndexOf("<", StringComparison.Ordinal) + 1;
                    var pTo = action.LastIndexOf(">", StringComparison.Ordinal);
                    var actionType = action.Substring(pFrom, pTo - pFrom);
                    actionOnly = action.Replace($"<{actionType}>", "").Trim();
                    param = $"{actionType} param";
                }

                var actionFunctionName = char.ToUpper(actionOnly[0]) + actionOnly.Substring(1);
                content += $"public void Invoke{actionFunctionName}({param})" + "{\n";
                content += $"EventHandler.{actionOnly}?.Invoke(";
                if (param != "")
                {
                    content += "param";
                }

                content += ");\n";
                content += "if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log(\"Invoked event: " + actionOnly + "\");\n";
                content += "}\n";
            }

            content += "}\n}";

            File.WriteAllText(path, content);
        }
    }
}