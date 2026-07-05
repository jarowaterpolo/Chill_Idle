using System.IO;
using Unity.Collections;
using UnityEngine;

public class DataSaver
{
    private string path;
    private string json;

    public void SaveGameData(Data data)
    {
        GameData gameData = new GameData(data);
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
            Data newData = new Data();
            GameData emptyData = new(newData);
            File.WriteAllText(path, JsonUtility.ToJson(emptyData, true));
            json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
    }

    public void ResetData()
    {
        Data newData = new Data();
        GameData emptyData = new(newData);
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
    public Data Data;


    public GameData(Data data)
    {
        Data = data;
    }
}
