using System.IO;
using Unity.Collections;
using UnityEngine;

public class DataSaver
{
    private string path;
    private string json;

    public void SaveGameData(SaveData saveData)
    {
        GameData gameData = new GameData(saveData);
        json = JsonUtility.ToJson(gameData, true);

#if (UNITY_WEBGL && !UNITY_EDITOR)
        path = System.IO.Path.Combine("idbfs", Application.productName);
        Debug.Log($"{path}");
        if (!Directory.Exists(path)) 
        { 
            Directory.CreateDirectory(path);
        }
        path = System.IO.Path.Combine(path, "saveAntData");
#else
        path = Application.persistentDataPath + "/gameData.json";
#endif
        File.WriteAllText(path, json);
    }

    public GameData LoadGameData()
    {
#if (UNITY_WEBGL && !UNITY_EDITOR)
        path = System.IO.Path.Combine("idbfs", Application.productName);
        if (!Directory.Exists(path)) 
        { 
            Directory.CreateDirectory(path);
        }
        Debug.Log($"{path}");
        path = System.IO.Path.Combine(path, "saveAntData");
#else
        path = Application.persistentDataPath + "/gameData.json";
#endif
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            SaveData newSaveData = new SaveData();
            GameData emptyData = new(newSaveData);
            File.WriteAllText(path, JsonUtility.ToJson(emptyData, true));
            json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
    }

    public void ResetData()
    {
        SaveData newSaveData = new SaveData();
        GameData emptyData = new(newSaveData);
        json = JsonUtility.ToJson(emptyData, true);

#if (UNITY_WEBGL && !UNITY_EDITOR)
        path = System.IO.Path.Combine("idbfs", Application.productName);
        Debug.Log($"{path}");
        if (!Directory.Exists(path)) 
        { 
            Directory.CreateDirectory(path);
        }
        path = System.IO.Path.Combine(path, "saveAntData");
#else
        path = Application.persistentDataPath + "/gameData.json";
#endif
        File.WriteAllText(path, json);
    }
}

[System.Serializable]
public class GameData
{
    public SaveData SaveData;


    public GameData(SaveData saveData)
    {
        SaveData = saveData;
    }
}
