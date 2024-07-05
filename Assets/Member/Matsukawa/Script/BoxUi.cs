using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// áŠQ•¨‚ÌUI‚ğ•\¦‚·‚éƒNƒ‰ƒX
/// </summary>
public class BoxUi : MonoBehaviour
{
    // ScoreUpŒp³
    public ScoreUp scoreUp;

    // svoreUp‚©‚ç‚Á‚Ä‚«‚½ƒŠƒXƒg‚ğ‚¢‚ê‚é‚Æ‚±‚ë
    private List<GameObject> b_list;    // Player1‚Ì¶¬‚µ‚½áŠQ•¨‚ğ•Û‘¶‚µ‚Ä‚¨‚­‚½‚ß‚ÌƒŠƒXƒg
    private List<GameObject> b_list2;    // Player2‚ÌáŠQ•¨


    /* áŠQ•¨‚ÌUIŠÖŒW */
    // •ÏX‚·‚éƒCƒ[ƒW‚ğ•ÛŠÇ‚·‚é‚½‚ß‚ÌƒŠƒXƒg
    [SerializeField] protected GameObject[] magentaSpriteArr;
    [SerializeField] protected GameObject[] blueSpriteArr;

    // image‚ğŠi”[‚·‚é‚Æ‚±‚ë
    //[SerializeField] public GameObject magentaUiImage;
    //[SerializeField] public GameObject blueUiImage;

    /// <summary>
    /// player1‚ÌáŠQ•¨UI‚ğ¶¬‚·‚éŠÖ”
    /// </summary>
    //public void ShowMagentaBoxUi()
    //{
    //    /* SourceImage‚©‚ç•ÏX‚·‚é */

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
    /// player2‚ÌáŠQ•¨UI‚ğ¶¬‚·‚éŠÖ”
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
