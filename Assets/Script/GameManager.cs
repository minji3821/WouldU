using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Inst => instance;

    private int currenScore = 0;
    public int currentLevel = 1;
    public int currentObjectCount = 3;
    public bool isGaming = false;

    [SerializeField]
    private int[] levelUpScores;
    //이동하는데 걸리는 시간이라 적을수록 빠름
    [SerializeField]
    public float[] objectSpeedsPerLevel;

    private GameObject obj;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private UIManager uiManager;
    public UIManager UIManager
    {
        get
        {
            if (uiManager == null)
            {
                obj = GameObject.Find("UIManager");
                if (!obj.TryGetComponent<UIManager>(out uiManager))
                    Debug.Log("GameManager.cs - uiManager 참조 실패");
            }
            return uiManager;
        }
    }

    private ObjectSpawner objectSpawner;
    public ObjectSpawner ObjectSpawner
    {
        get
        {
            if (objectSpawner == null)
            {
                obj = GameObject.Find("ObjSpawner");
                if (!obj.TryGetComponent<ObjectSpawner>(out objectSpawner))
                    Debug.Log("GameManager.cs - objectSpawner 참조 실패");
            }
            return objectSpawner;
        }
    }

    public void AddScore(int score)
    {
        currenScore += score;
        UIManager.UpdateScoreUI(currenScore);
        Debug.Log(currenScore);

        if (currentLevel < 3 && currenScore >= levelUpScores[currentLevel - 1])
            LevelUp();
    }

    public void LevelUp()
    {
        currentLevel++;
        currentObjectCount++;
        UpdateSpawnerSettings();
        UIManager.ChangeBG(currentLevel);

        Debug.Log("난이도 상승"); 
    }

    void Update()  // 임시
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(100);
        }
    }

    public void UpdateSpawnerSettings()
    {
        // ObjectSpawner에 레벨 기반 설정 전달
        float currentSpeed = objectSpeedsPerLevel[currentLevel - 1];
        ObjectSpawner.UpdateSettings(currentObjectCount, currentSpeed);
    }

    public void OnObjectFixed()
    {
        // 다음 오브젝트 생성
        ObjectSpawner.SpawnObject();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UIManager.ShowResult(currenScore);
    }
}
