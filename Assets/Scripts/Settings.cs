using System;

public static class Settings
{
    public static Setting<string> LastPath { get; } = new("LastPath", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
}
