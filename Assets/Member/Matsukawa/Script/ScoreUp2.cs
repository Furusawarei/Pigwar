using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUp2 : MonoBehaviour
{
    public ScoreUp scoreUp;

    // Update is called once per frame
    void Update()
    {
        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (scoreUp.score2 <= 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKey(KeyCode.X))
        {
            scoreUp.GeneratePrefabs2();
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
