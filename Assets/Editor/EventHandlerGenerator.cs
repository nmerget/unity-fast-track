using System.Collections.Generic;
using System.IO;
using UnityEditor;
[InitializeOnLoad]
public class EventHandlerGenerator {

    static EventHandlerGenerator () {
        var actions = ReadEventHandler ();
        WriteEventHandlerController (actions);
    }

    static List<string> ReadEventHandler () {
        string path = "Assets/Scripts/Utils/EventHandler.cs";
        StreamReader reader = new StreamReader (path);

        string line;
        List<string> actions = new List<string> ();
        while ((line = reader.ReadLine ()) != null) {
            if (line.Contains ("public static Action")) {
                actions.Add (
                    line
                    .Replace ("public static Action", "")
                    .Replace (";", "")
                    .Trim ()
                );
            }
        }

        reader.Close ();
        return actions;
    }

    static void WriteEventHandlerController (List<string> actions) {
        string path = "Assets/Scripts/Utils/EventHandlerController.cs";
        string content = "/**\n* THIS SCRIPT IS GENERATED! DON'T MODIFY IT. \n*/\n\nusing UnityEngine;\n\npublic class EventHandlerController : MonoBehaviour\n{\n";
        foreach (var action in actions) {
            string actionOnly = action;
            string param = "";
            if (action.Contains ("<")) {
                int pFrom = action.IndexOf ("<") + 1;
                int pTo = action.LastIndexOf (">");
                string test = $"bla {pFrom}";
                string actionType = action.Substring (pFrom, pTo - pFrom);
                actionOnly = action.Replace ($"<{actionType}>", "").Trim ();
                param = $"{actionType} param";
            }

            string actionFunctionName = char.ToUpper (actionOnly[0]) + actionOnly.Substring (1);
            content += $"public void Invoke{actionFunctionName}({param})" + "{\n";
            content += $"EventHandler.{actionOnly}?.Invoke(";
            if (param != "") {
                content += "param";
            }
            content += ");\n}\n";
        }
        content += "}";

        File.WriteAllText (path, content);
    }

}