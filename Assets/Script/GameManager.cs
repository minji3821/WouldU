using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ObjectSpawner objectSpawner;

    private int currenScore = 0;
    private int currentLevel = 1;
    private int currentObjectCount = 3;

    [SerializeField]
    private int[] levelUpScores;
    //�̵��ϴµ� �ɸ��� �ð��̶� �������� ����
    [SerializeField]
    private float[] objectSpeedsPerLevel;

    private GameObject obj;
    private UIManager uiManager;

    private void Start()
    {
        obj = GameObject.Find("UIManager");
        if (!obj.TryGetComponent<UIManager>(out uiManager))
            Debug.Log("GameManager.cs - uiManager ���� ����");

        UpdateSpawnerSettings();
        objectSpawner.SpawnObject();
    }

    //public void ObjectSpawn()  // To do: ��ũ��Ʈ �ϳ� ���ֱ�
    //{
    //    int randomNum = Random.Range(1, currentObjectCount + 1);
    //    Debug.Log("������Ʈ" + randomNum + "����");
    //}

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

        Debug.Log("���̵� ���"); 
    }

    void Update()  // �ӽ�
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(100);
            //ObjectSpawn();
        }
    }

    private void UpdateSpawnerSettings()
    {
        // ObjectSpawner�� ���� ��� ���� ����
        float currentSpeed = objectSpeedsPerLevel[currentLevel - 1];
        objectSpawner.UpdateSettings(currentObjectCount, currentSpeed);
    }

    public void OnObjectFixed()
    {
        // ���� ������Ʈ ����
        objectSpawner.SpawnObject();
    }
}
