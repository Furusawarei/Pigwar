using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Q����UI��\������N���X
/// </summary>
public class BoxUi : MonoBehaviour
{
    public ScoreUp scoreUp;

    /* ��Q����UI�֌W */

    // �ύX����C���[�W��ۊǂ��邽�߂̃��X�g
    [SerializeField] protected Sprite[] magentaSpriteArr;
    [SerializeField] protected Sprite[] blueSpriteArr;

    // image���i�[����Ƃ���
    [SerializeField] protected Image magentaUiImage;
    [SerializeField] protected Image blueUiImage;

    private List<GameObject> b_list;    // Player1�̐���������Q����ۑ����Ă������߂̃��X�g
    private List<GameObject> b_list2;    // Player2�̏�Q��

    /// <summary>
    /// player1�̏�Q��UI�𐶐�����֐�
    /// </summary>
    public void ShowMagentaBoxUi()
    {
        b_list = scoreUp.boxList;

        //magentaUiImage = GetComponent<Image>();

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
    /// player2�̏�Q��UI�𐶐�����֐�
    /// </summary>
    public void ShowBlueBoxUi()
    {
        // 
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
