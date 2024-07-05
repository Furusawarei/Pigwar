using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 障害物のUIを表示するクラス
/// </summary>
public class BoxUi : MonoBehaviour
{
    // ScoreUp継承
    public ScoreUp scoreUp;

    // svoreUpから持ってきたリストをいれるところ
    private List<GameObject> b_list;    // Player1の生成した障害物を保存しておくためのリスト
    private List<GameObject> b_list2;    // Player2の障害物


    /* 障害物のUI関係 */
    // 変更するイメージを保管するためのリスト
    [SerializeField] protected GameObject[] magentaSpriteArr;
    [SerializeField] protected GameObject[] blueSpriteArr;

    // imageを格納するところ
    //[SerializeField] public GameObject magentaUiImage;
    //[SerializeField] public GameObject blueUiImage;

    /// <summary>
    /// player1の障害物UIを生成する関数
    /// </summary>
    //public void ShowMagentaBoxUi()
    //{
    //    /* SourceImageから変更する */

    //    b_list = scoreUp.boxList;


    //    if (b_list.Count == 0)
    //    {
    //        magentaUiImage.sprite = magentaSpriteArr[0];
    //    }
    //    else if (b_list.Count == 1)
    //    {
    //        magentaUiImage.sprite = magentaSpriteArr[1];
    //    }
    //    else if (b_list.Count == 2)
    //    {
    //        magentaUiImage.sprite = magentaSpriteArr[2];
    //    }
    //}


    public void ShowMagentaBoxUi()
    {
        b_list = scoreUp.boxList;


        if (b_list.Count == 0)
        {
            magentaSpriteArr[0].SetActive(true);
        }
        else if (b_list.Count == 1)
        {
            magentaSpriteArr[1].SetActive(true);
        }
        else if (b_list.Count == 2)
        {
            magentaSpriteArr[2].SetActive(true);
        }

    }
    /// <summary>
    /// player2の障害物UIを生成する関数
    /// </summary>
    public void ShowBlueBoxUi()
    {
        b_list2 = scoreUp.boxList2;


        if (b_list2.Count == 0)
        {
            blueSpriteArr[0].SetActive(true);
        }
        else if (b_list2.Count == 1)
        {
            blueSpriteArr[1].SetActive(true);
        }
        else if (b_list2.Count == 2)
        {
            blueSpriteArr[2].SetActive(true);
        }
    }
}
