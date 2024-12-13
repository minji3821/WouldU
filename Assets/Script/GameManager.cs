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
    //�̵��ϴµ� �ɸ��� �ð��̶� �������� ����
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
                    Debug.Log("GameManager.cs - uiManager ���� ����");
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
                    Debug.Log("GameManager.cs - objectSpawner ���� ����");
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

        Debug.Log("���̵� ���"); 
    }

    void Update()  // �ӽ�
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(100);
        }
    }

    public void UpdateSpawnerSettings()
    {
        // ObjectSpawner�� ���� ��� ���� ����
        float currentSpeed = objectSpeedsPerLevel[currentLevel - 1];
        ObjectSpawner.UpdateSettings(currentObjectCount, currentSpeed);
    }

    public void OnObjectFixed()
    {
        // ���� ������Ʈ ����
        ObjectSpawner.SpawnObject();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UIManager.ShowResult(currenScore);
    }
}
