using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveProvider<T>
{
    private readonly BinaryFormatter _binaryFormater;
    private const string _defaultExtension = "gm";
    private const string _defaultFolder = "";
    public SaveProvider()
        => _binaryFormater = new BinaryFormatter();

    public T TryGetSave(string name, string extension = _defaultExtension, string folder = _defaultFolder)
    {
        string path = GetFullFilePath(name, extension, folder);
        return TryGetSave(path);
    }

    private T TryGetSave(string path)
    {
        if (!File.Exists(path))
            return default;

        var fileStream = new FileStream(path, FileMode.Open);
        fileStream.Position = 0;
        T save = (T)_binaryFormater.Deserialize(fileStream);
        fileStream.Close();
        return save;
    }

    public IEnumerable<FileInfo> GetAllSaves(string folder = _defaultFolder, string extension = _defaultExtension)
    {
        var saves = new List<FileInfo>();
        foreach (var file in Directory.GetFiles(GetFullFilePath(folder)))
        {
            if (!Path.HasExtension(file) || Path.GetExtension(file) != extension)
                continue;

            var info = new FileInfo(file);
            saves.Add(info);
        }
        return saves;
    }

    public void UpdateSave(T save, string name, string extension = _defaultExtension, string folder = _defaultFolder)
    {
        string path = GetFullFilePath(name, extension, folder);
        var fileStream = new FileStream(path, FileMode.Create);
        _binaryFormater.Serialize(fileStream, save);
        fileStream.Close();
    }

    private string GetFullFilePath(string name, string extension = _defaultExtension, string folder = _defaultFolder)
    {
        return Application.persistentDataPath 
            + (folder == "" ? "" : "/" + folder)
            + "/" + name + "." + extension;
    }
}