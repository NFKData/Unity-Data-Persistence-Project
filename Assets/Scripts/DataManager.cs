using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;
    private string playerName;
    private string bestScorePlayerName;
    private int bestScore;
    private string saveDataPath;

    public string PlayerName { get => playerName; }
    public string BestScorePlayerName { get => bestScorePlayerName; }
    public int BestScore { get => bestScore; }

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        saveDataPath = $"{Application.persistentDataPath}/savefile.json";
        LoadSaveData();
    }

    public void StartGame(TextMeshProUGUI nameInput) {
        playerName = nameInput.text;
        SceneManager.LoadScene(1);
    }

    private void LoadSaveData() {
        if (!File.Exists(saveDataPath)) {
            return;
        }
        string saveDataJson = File.ReadAllText(saveDataPath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataJson);
        bestScorePlayerName = saveData.playerName;
        bestScore = saveData.bestScore;
    }

    public void SaveBestScoreIfNeeded(int score) {
        if (score < bestScore) {
            return;
        }
        SaveData saveData = new()
        {
            playerName = playerName,
            bestScore = score
        };
        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveDataPath, saveDataJson);
        bestScore = score;
        bestScorePlayerName = playerName;
    }

    [System.Serializable]
    private class SaveData {

        public string playerName;
        public int bestScore;

    }
}
