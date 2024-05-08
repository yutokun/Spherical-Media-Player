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
            var jsonClass = JsonUtility.FromJson<ProjectionPairJson>(json);

            if (jsonClass.Pairs is null) return;

            pairs.Clear();
            foreach (var pair in jsonClass.Pairs)
            {
                pairs.Add(pair.Path, pair.Mode);
            }
        }

        void Save()
        {
            var jsonClass = new ProjectionPairJson();
            jsonClass.Pairs = pairs.Select(p => new ProjectionPair { Path = p.Key, Mode = p.Value }).ToArray();
            var json = JsonUtility.ToJson(jsonClass);
            File.WriteAllText(DatabasePath, json);
            Debug.Log(json);
        }

        public ProjectionMode Get(string path)
        {
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
        }
    }
}
