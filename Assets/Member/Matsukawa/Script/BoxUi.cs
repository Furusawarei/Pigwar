using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 障害物のUIを管理するクラス
/// </summary>
public class BoxUi : MonoBehaviour
{
    public ScoreUp scoreUp;

    /* 障害物のUI表示 */

    // 変更するイメージを格納するためのリスト
    [SerializeField] protected Sprite[] magentaSpriteArr;
    [SerializeField] protected Sprite[] blueSpriteArr;

    // imageコンポーネントを格納する
    [SerializeField] protected Image magentaUiImage;
    [SerializeField] protected Image blueUiImage;

    private List<GameObject> b_list;    // Player1の生成された障害物を保持するためのリスト
    private List<GameObject> b_list2;   // Player2の障害物

    /// <summary>
    /// Player1の障害物UIを更新する関数
    /// </summary>
    public void ShowMagentaBoxUi()
    {
        b_list = scoreUp.boxList;

        if (b_list.Count == 0)
        {
            magentaUiImage.sprite = magentaSpriteArr[0];
        }
        else if (b_list.Count == 1)
        {
            magentaUiImage.sprite = magentaSpriteArr[1];
        }
        else if (b_list.Count == 2)
        {
            magentaUiImage.sprite = magentaSpriteArr[2];
        }
    }

    /// <summary>
    /// Player2の障害物UIを更新する関数
    /// </summary>
    public void ShowBlueBoxUi()
    {
        if (blueUiImage == null) blueUiImage = GetComponent<Image>();

        b_list2 = scoreUp.boxList2;

        if (b_list2.Count == 0)
        {
            blueUiImage.sprite = blueSpriteArr[0];
        }
        else if (b_list2.Count == 1)
        {
            blueUiImage.sprite = blueSpriteArr[1];
        }
        else if (b_list2.Count == 2)
        {
            blueUiImage.sprite = blueSpriteArr[2];
        }
    }
}
