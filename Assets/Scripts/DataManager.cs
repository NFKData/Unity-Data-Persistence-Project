using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;
    private string playerName;

    public string PlayerName {get { return playerName; }}

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame(TextMeshProUGUI nameInput) {
        playerName = nameInput.text;
        SceneManager.LoadScene(1);
    }
}
