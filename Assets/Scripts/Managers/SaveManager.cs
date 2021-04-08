using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    private SaveData slot;

    public static SaveManager Instance { get => instance; }
    public SaveData Slot { get => slot; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);
    }

    /// <summary>
    /// Saves a game file.
    /// </summary>
    /// <param name="currentLevel">THe data to save.</param>
    public void Save(SaveData currentLevel)
    {
        slot = currentLevel;

        BinaryFormatter bf = new BinaryFormatter();
        string savePath;
        savePath = Application.persistentDataPath + "/save.dat";
        FileStream file = File.Create(savePath);
        bf.Serialize(file, slot);
        file.Close();
    }

    /// <summary>
    /// Loads a saved game file.
    /// </summary>
    public void Load()
    {
        string savePath;
        savePath = Application.persistentDataPath + "/save.dat";
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            this.slot = (SaveData)bf.Deserialize(file);
            file.Close();
        }
    }

    /// <summary>
    /// Deletes a saved game file.
    /// </summary>
    public void Delete()
    {
        string savePath;
        savePath = Application.persistentDataPath + "/save.dat";
        if (File.Exists(savePath))
            File.Delete(savePath);
        slot = null;
    }
}
