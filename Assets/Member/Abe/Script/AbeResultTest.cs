using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultTest : MonoBehaviour
{
    [SerializeField, Header("勝った人に当たるライト")] private GameObject _winerLight;
    private Transform _winerLightTransfrom;
    private Image _winerLightImage;

    [SerializeField,Header("勝ち負けを表示するtext")] private List<GameObject> _winOrLose;
    private List<TextMeshProUGUI> _winOrLoseText;

    [SerializeField,Header("プレイヤー")]private List<GameObject> _player;
    private List<Transform> _playerScale;

    [SerializeField, Header("何秒後に発表するか")] private float _waitTime;


    private void Awake()
    {
        _winerLightTransfrom = _winerLight.GetComponent<Transform>();
        _winerLightImage = _winerLight.GetComponent<Image>();

        _winOrLoseText[0] = _winOrLose[0].GetComponent<TextMeshProUGUI>();
        _winOrLoseText[1] = _winOrLose[1].GetComponent<TextMeshProUGUI>();

        _playerScale[0] = _player[0].GetComponent<Transform>();
        _playerScale[1] = _player[1].GetComponent<Transform>();
    }
    void Update() { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scoremaneger.Instance().ScoreRandomSwitch();
        }
        
    }


    private void judge()
    {
        _winOrLoseText[0].color = Color.red;
    }
}
