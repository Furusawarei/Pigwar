using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreUp2 : MonoBehaviour
{
    [SerializeField] ActionControl _actionControl;

    public ScoreUp scoreUp;
    public GameObject boxPrefab; // 出現させるプレハブ
    public Transform spawnPoint;

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }

    // Update is called once per frame
    public void Generate()
    {
        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (scoreUp.score2 < 2) return;
        if (_actionControl.Player1.Summon.triggered)
        {
         Instantiate(boxPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Player２の障害物の数
        int count2 = GameObject.FindGameObjectsWithTag("boxPrefab2").Length;

        if (count2 <= 8) return;
        if (count2 == 8)
        {
            Destroy(scoreUp.boxPrefab2);
        }

    }
}