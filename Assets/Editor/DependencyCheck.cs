using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class DependencyCheck
    {
        private static readonly Dictionary<string, string> Dependencies =
            new()
            {
                {
                    "AsyncAwaitUtil",
                    "https://assetstore.unity.com/packages/tools/integration/async-await-support-101056"
                },
                { "LeanTween", "https://assetstore.unity.com/packages/tools/animation/leantween-3595" },
                { "TextMesh Pro", "Window/Package Manager" }
            };

        static DependencyCheck()
        {
            var directories = new List<string>();
            SearchDependencies(Application.dataPath, directories);
            foreach (var dependencyKey in from dependencyKey in Dependencies.Keys
                     let foundDependency = directories.Find(dir => dir.Contains(dependencyKey))
                     where foundDependency == null
                     select dependencyKey)
            {
                Debug.LogErrorFormat("You are missing dependency {0}. Download it here: {1}", dependencyKey,
                    Dependencies[dependencyKey]);
            }
        }

        private static void SearchDependencies(string aDir, List<string> directories)
        {
            var subDirs = Directory.GetDirectories(aDir);
            foreach (var subDir in subDirs)
            {
                directories.Add(subDir);
                SearchDependencies(subDir, directories);
            }
        }
    }
}