using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad
{
    private static Unlockable[] _unlockeables;
    private static string path = Application.persistentDataPath + "/colectables.data";
    private static BinaryFormatter formatter;
    private static FileStream create;
    private static FileStream open;

    public static Unlockable[] Unlockeables { get => _unlockeables; set => _unlockeables = value; }

    /// <summary>
    /// Save Colectables Unlocked to a file.
    /// </summary>
    /// <param name="arrays"></param>
    public static void SaveColectables(ColectablesArrays arrays, Unlockable[] unlockables)
    {
        _unlockeables = unlockables;
        formatter = new BinaryFormatter();
        create = new FileStream(path, FileMode.Create);
        ColectableData data = new ColectableData(arrays);
        formatter.Serialize(create, data);
        create.Close();
        Debug.Log("Game Saved");
    }

    /// <summary>
    /// Loads Colectables Unlocked.
    /// </summary>
    /// <returns></returns>
    public static ColectableData LoadColectables()
    {
        ColectableData data;
        if (File.Exists(path))
        {
            formatter = new BinaryFormatter();
            open = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(open) as ColectableData;
            open.Close();
            Debug.Log("Loaded Game");
            return data;
        }
        else
        {
            Debug.LogWarning("File not found" + path);   
            return null;
        }
    }
} 
    

