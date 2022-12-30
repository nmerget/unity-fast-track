using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public class ActionHandlerGenerator
    {
        static ActionHandlerGenerator()
        {
            var actions = ReadActionHandler();
            WriteActionHandlerController(actions);
        }

        private static List<string> ReadActionHandler()
        {
            const string path = "Assets/Scripts/Utils/ActionHandler.cs";
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
        
        private static readonly List<string> Dependencies = new() { "UnityEngine" };

        private static void WriteActionHandlerController(List<string> actions)
        {
            const string path = "Assets/Scripts/Utils/ActionHandlerController.cs";
            var content = "/**\n* THIS SCRIPT IS GENERATED! DON'T MODIFY IT. \n*/\n";

            foreach (var dependency in Dependencies)
            {
                content += $"\nusing {dependency};";
            }

            content += "namespace Utils\n{\n\n" +
                       "public class ActionHandlerController : MonoBehaviour\n{\n";

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
                content += $"ActionHandler.{actionOnly}?.Invoke(";
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