using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
[InitializeOnLoad]
public class DependencyCheck {

    private static Dictionary<string, string> DEPENDENCIES =
        new Dictionary<string, string> () { { "AsyncAwaitUtil", "https://assetstore.unity.com/packages/tools/integration/async-await-support-101056" },
         { "LeanTween", "https://assetstore.unity.com/packages/tools/animation/leantween-3595" },
          { "TextMesh Pro", "Window/Package Manager" }
        };

    static DependencyCheck () {
        List<string> directories = new List<string> ();
        SearchDependencies (Application.dataPath, directories);
        foreach (string dependencyKey in DEPENDENCIES.Keys) {
            string foundDependency = directories.Find ((string dir) => {
                return dir.Contains (dependencyKey);
            });
            if (foundDependency == null) {
                Debug.LogErrorFormat ("You are missing dependency {0}. Download it here: {1}", dependencyKey, DEPENDENCIES[dependencyKey]);

            }
        }
    }

    static void SearchDependencies (string aDir, List<string> directories) {
        string[] subDirs = Directory.GetDirectories (aDir);
        foreach (string subDir in subDirs) {
            directories.Add (subDir);
            SearchDependencies (subDir, directories);
        }
    }
}