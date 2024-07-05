using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Q����UI��\������N���X
/// </summary>
public class BoxUi : MonoBehaviour
{
    // ScoreUp�p��
    public ScoreUp scoreUp;

    // svoreUp���玝���Ă������X�g�������Ƃ���
    private List<GameObject> b_list;    // Player1�̐���������Q����ۑ����Ă������߂̃��X�g
    private List<GameObject> b_list2;    // Player2�̏�Q��


    /* ��Q����UI�֌W */
    // �ύX����C���[�W��ۊǂ��邽�߂̃��X�g
    [SerializeField] protected GameObject[] magentaSpriteArr;
    [SerializeField] protected GameObject[] blueSpriteArr;

    // image���i�[����Ƃ���
    //[SerializeField] public GameObject magentaUiImage;
    //[SerializeField] public GameObject blueUiImage;

    /// <summary>
    /// player1�̏�Q��UI�𐶐�����֐�
    /// </summary>
    //public void ShowMagentaBoxUi()
    //{
    //    /* SourceImage����ύX���� */

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
    /// player2�̏�Q��UI�𐶐�����֐�
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
