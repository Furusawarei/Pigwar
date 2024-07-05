using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Scoremaneger scoreManager;

    protected int score1;
    protected int score2;
    private TextMeshProUGUI scoreTextList;

    [SerializeField] private Transform boxGeneratePos;
    [SerializeField] private Transform boxGeneratePos2;

    [SerializeField] private GameObject boxPrefab;
    [SerializeField] protected GameObject boxPrefab2;

    [Header("Player1の障害物"), SerializeField] List<GameObject> boxList = new List<GameObject>();
    [Header("Player2の障害物"), SerializeField] List<GameObject> boxList2 = new List<GameObject>();

    private int countPrefabs;

    [SerializeField] List<GameObject> pigUiList = new List<GameObject>();
    [SerializeField] List<GameObject> pigUiList2 = new List<GameObject>();

    [SerializeField] private Transform magentaBoxUiPos;
    [SerializeField] private Transform blueboxUiPos;

    [SerializeField] private GameObject parentObj;

    private void Start()
    {
        ShowMagentaBoxUi();
        ShowBlueBoxUi();
    }

    public void Update()
    {
        GeneratePrefabs();
        GeneratePrefabs2();
    }

    /// <summary>
    /// Player1の障害物を管理する関数
    /// </summary>
    public void GeneratePrefabs()
    {
        scoreManager.PlayerScore[0] = score1;

        if (score1 < 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Z))
        {
            score1 = scoreManager.PlayerScore[0];
            TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

            var obj = Instantiate(boxPrefab, boxGeneratePos.transform.position, Quaternion.identity);

            score1 -= 2;
            scoreTextList[0].text = string.Format("Player1:{0}", score1);
            Debug.Log("player1:" + score1);

            obj.name = "cube" + countPrefabs;
            countPrefabs++;
            boxList.Add(obj);

            if (boxList.Count > 8)
            {
                Destroy(boxList[0]);
                boxList.RemoveAt(0);
            }

            ShowMagentaBoxUi();
        }
    }

    /// <summary>
    /// Player2の障害物を管理する
    /// </summary>
    public void GeneratePrefabs2()
    {
        scoreManager.PlayerScore[1] = score2;

        if (score2 < 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.X))
        {
            score2 = scoreManager.PlayerScore[1];
            TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

            var obj = Instantiate(boxPrefab2, boxGeneratePos2.transform.position, Quaternion.identity);

            score2 -= 2;
            scoreTextList[1].text = string.Format("Player2:{0}", score2);
            Debug.Log("player2:" + score2);

            obj.name = "cube" + countPrefabs;
            countPrefabs++;
            boxList2.Add(obj);

            if (boxList2.Count > 8)
            {
                Destroy(boxList2[0]);
                boxList2.RemoveAt(0);
            }

            ShowBlueBoxUi();
        }
    }

    public void ClickScoreUp()
    {
        TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

        score1++;
        scoreTextList[0].text = string.Format("Player1:{0}", score1);

        Debug.Log("player1:" + score1);
    }

    public void ClickScoreUp2()
    {
        TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

        score2++;
        scoreTextList[1].text = string.Format("Player2:{0}", score2);

        Debug.Log("player2:" + score2);
    }

    public void ShowMagentaBoxUi()
    {
        foreach (Transform child in magentaBoxUiPos)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < boxList.Count; i++)
        {
            Instantiate(pigUiList[i], magentaBoxUiPos.position, Quaternion.identity, parentObj.transform);
        }
    }

    public void ShowBlueBoxUi()
    {
        foreach (Transform child in blueboxUiPos)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < boxList2.Count; i++)
        {
            Instantiate(pigUiList2[i], blueboxUiPos.position, Quaternion.identity, parentObj.transform);
        }
    }
}