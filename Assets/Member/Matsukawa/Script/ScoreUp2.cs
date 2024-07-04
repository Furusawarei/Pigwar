using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreUp2 : ScoreUp
{
    [SerializeField] private ActionControl _actionControl;
    public ScoreUp scoreUp;

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }


    // Update is called once per frame
    public void Generate()
    {
        // あべさんの所から来たスコア
        scoreManager.PlayerScore[1] = score2;

        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (score2 < 2) return;
        if (_actionControl.Player1.Summon.triggered)
        {
            scoreUp.GeneratePrefabs();
        }
﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ScoreUp2 : ScoreUp

//{
//    // Update is called once per frame
//    public void Generate()
//    {
//        // あべさんの所から来たスコア
//        scoreManager.PlayerScore[1] = score2;

//        //✕ボタン（Xキー）を押したときスコアが２以上なら
//        //障害物をプレイヤー２の目の前に生成する   
//        if (score2 < 2) return;
//        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.X))
//        {
//            GeneratePrefabs();
//        }

//        // Player２の障害物の数
//        int count2 = GameObject.FindGameObjectsWithTag("boxPrefab2").Length;

     //   if (count2 <= 8) return;
     //   if (count2 == 8)
        {
            Destroy(boxPrefab2);
        }

    }
}
//        if (count2 <= 8) return;
//        if (count2 == 8)
//        {
//            Destroy(boxPrefab2);
//        }

//    }
//}
