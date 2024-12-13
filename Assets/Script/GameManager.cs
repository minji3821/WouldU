using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Inst => instance;

    [SerializeField]
    private ObjectSpawner objectSpawner;

    private int currenScore = 0;
    private int currentLevel = 1;
    private int currentObjectCount = 3;

    [SerializeField]
    private int[] levelUpScores;
    //이동하는데 걸리는 시간이라 적을수록 빠름
    [SerializeField]
    private float[] objectSpeedsPerLevel;

    private GameObject obj;
    private UIManager uiManager;

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

    private void Start()
    {
        obj = GameObject.Find("UIManager");
        if (!obj.TryGetComponent<UIManager>(out uiManager))
            Debug.Log("GameManager.cs - uiManager 참조 실패");

        UpdateSpawnerSettings();
        objectSpawner.SpawnObject();
    }

    public void AddScore(int score)
    {
        currenScore += score;
        uiManager.UpdateScoreUI(currenScore);
        Debug.Log(currenScore);

        if (currentLevel <= 3 && currenScore >= levelUpScores[currentLevel - 1])
            LevelUp();
    }

    public void LevelUp()
    {
        currentLevel++;
        currentObjectCount++;
        UpdateSpawnerSettings();

        Debug.Log("난이도 상승"); 
    }

    void Update()  // 임시
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(100);
            //ObjectSpawn();
        }
    }

    private void UpdateSpawnerSettings()
    {
        // ObjectSpawner에 레벨 기반 설정 전달
        float currentSpeed = objectSpeedsPerLevel[currentLevel - 1];
        objectSpawner.UpdateSettings(currentObjectCount, currentSpeed);
    }

    public void OnObjectFixed()
    {
        // 다음 오브젝트 생성
        objectSpawner.SpawnObject();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        uiManager.ShowResult(currenScore);
    }
}
