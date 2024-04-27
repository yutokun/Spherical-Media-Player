using System;
using UnityEngine;

namespace yutokun.SphericalMediaPlayer
{
    public class Setting<T>
    {
        readonly string key;

        T value;
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                Save();
            }
        }

        readonly T defaultValue;

        public Setting(string key, T defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;

            Load();
        }

        public static implicit operator T(Setting<T> setting) => setting.value;

        static TypeCode TypeCode => Type.GetTypeCode(typeof(T));

        void Load()
        {
            Value = TypeCode switch
            {
                TypeCode.String => (T)(object)PlayerPrefs.GetString(key, (string)(object)defaultValue),
                _ => throw new InvalidOperationException($"未サポートの型が指定されました: {typeof(T)}"),
            };
        }

        void Save()
        {
            switch (TypeCode)
            {
                case TypeCode.String:
                    PlayerPrefs.SetString(key, (string)(object)Value);
                    break;

                default:
                    throw new InvalidOperationException($"未サポートの型が指定されました: {typeof(T)}");
            }
        }
    }
}
