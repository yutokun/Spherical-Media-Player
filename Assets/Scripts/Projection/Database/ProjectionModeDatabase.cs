using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace yutokun.SphericalMediaPlayer
{
    public class ProjectionModeDatabase : MonoBehaviour
    {
        static string DatabasePath => Path.Combine(Application.persistentDataPath, "ProjectionModeDatabase.json");

        readonly Dictionary<string, ProjectionMode> pairs = new();

        void Awake()
        {
            Load();
        }

        void Load()
        {
            var json = File.ReadAllText(DatabasePath);
            var pairsClasses = JsonUtility.FromJson<ProjectionPair[]>(json);

            pairs.Clear();
            foreach (var pair in pairsClasses)
            {
                pairs.Add(pair.Path, pair.Mode);
            }
        }

        void Save()
        {
            var pairsClasses = pairs.Select(p => new ProjectionPair { Path = p.Key, Mode = p.Value });
            var json = JsonUtility.ToJson(pairsClasses);
            File.WriteAllText(DatabasePath, json);
        }

        public ProjectionMode Get(string path)
        {
            Debug.Log($"読込： {path} -> {pairs.GetValueOrDefault(path, ProjectionMode.Mono360)}");
            return pairs.GetValueOrDefault(path, ProjectionMode.Mono360);
        }

        public void AddOrReplace(string path, ProjectionMode mode)
        {
            if (pairs.ContainsKey(path))
            {
                pairs.Remove(path);
            }

            pairs.Add(path, mode);

            Save();

            Debug.Log($"保存： {path} -> {mode}");
        }
    }
}
