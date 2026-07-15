using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetNotationType : MonoBehaviour
{
    private DataSaver dataSaver = new();
    [HideInInspector] public Data data = new Data();

    [SerializeField] private GameManager manager;
    [SerializeField] private TMP_Dropdown dropdown;

    private Scene currentScene; 

    private void Start()
    {
        SaveData saveData = dataSaver.LoadGameData().SaveData;
        data = DataConverter.FromSave(saveData);

        currentScene = SceneManager.GetActiveScene();

        dropdown.value = (int)data.currentNotation;

        if (manager == null && currentScene.name == "Game")
        {
            manager = FindAnyObjectByType<GameManager>();
        }
    }

    public void SetType(notationType type)
    {
        if (manager != null && currentScene.name == "Game")
        {
            manager.data.currentNotation = type;
            manager.FormatChangeTrigger();
            Debug.Log($"manager current notation to {type} and try and save");

            Debug.Log(manager.data.currentNotation);

            SaveData saveData = DataConverter.ToSave(manager.data);
            dataSaver.SaveGameData(saveData);
        }
        else
        {
            data.currentNotation = type;
            Debug.Log($"notation type is not set to {data.currentNotation}");
            Save();
        }
    }

    public void OnDropdownChange(int value)
    {
        switch (value)
        {
            case 0:
                SetType(notationType.normal);
                break;

            case 1:
                SetType(notationType.shortend);
                break;

            case 2:
                SetType(notationType.scientific);
                break;

        }
    }

    public void SayValue(int value)
    {
        Debug.Log(value);
    }

    private void Save()
    {
        SaveData saveData = DataConverter.ToSave(data);
        dataSaver.SaveGameData(saveData);
    }
}
