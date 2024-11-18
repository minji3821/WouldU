using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currenScore = 0;
    private int currentLevel = 1;
    private int currentObjectCount = 3;

    [SerializeField]
    private int[] levelUpScores;

    private GameObject obj;
    private UIManager uiManager;

    private void Start()
    {
        obj = GameObject.Find("UIManager");
        if (!obj.TryGetComponent<UIManager>(out uiManager))
            Debug.Log("GameManager.cs - uiManager 참조 실패");
    }

    public void ObjectSpawn()  // To do: 스크립트 하나 빼주기
    {
        int randomNum = Random.Range(1, currentObjectCount + 1);
        Debug.Log("오브젝트" + randomNum + "생성");
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

        Debug.Log("난이도 상승"); 
    }

    void Update()  // 임시
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(100);
            ObjectSpawn();
        }
    }
}
