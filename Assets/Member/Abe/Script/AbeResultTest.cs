using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultTest : MonoBehaviour
{
    [SerializeField, Header("勝った人に当たるライト")] private GameObject _winerLight;
    private Transform _winerLightTransfrom;//ライトの位置
    private Image _winerLightImage;//ライトの本体image
    private float _lightPower = 0;//光の大きさ

    [SerializeField, Header("紙吹雪")] private GameObject _kamifubuki;
    private Transform _kamiTransform;//紙吹雪の位置

    [SerializeField, Header("勝ち負けを表示するtext")] private List<GameObject> _winOrLoseText;
    private List<TextMeshProUGUI> _winOrLoseTMP=new List<TextMeshProUGUI>();//勝ち負けを表示するtext
    private List<Transform> _winOrLoseTransform=new List<Transform>();//勝ち負けを表示するtextの位置

    [SerializeField, Header("プレイヤー")] private List<Transform> _playerScale;
    private Vector3 _winerPos = Vector3.zero;//勝ったプレイヤーの場所
    private Vector3 _defScale;//最初のプレイヤースケール
    private Vector3 _scale;//プレイヤースケールの大きさ
    private float _f_scale;//floatのスケール

    [SerializeField, Header("何秒後に発表するか")] private float _waitTime;
    private float _timer;
    private bool _isSettled = true;//決着がついたか
    private bool _resulted = false;//結果発表済みか
    private bool _isScaleUp = false;//スケール変更用フラグ

    private Coroutine _scaleCoroutine;//プレイヤースケールを変えるコルーチン
    #region 初期化 start(){
    private void Awake()
    {

        _winerLightTransfrom = _winerLight.GetComponent<Transform>();
        _winerLightImage = _winerLight.GetComponent<Image>();

        for (int i = 0; i < _winOrLoseText.Count; i++)
        {
            _winOrLoseTMP.Add(_winOrLoseText[i].GetComponent<TextMeshProUGUI>());
            _winOrLoseTransform.Add(_winOrLoseText[i].GetComponent<Transform>());
        }

        _kamiTransform = _kamifubuki.GetComponent<Transform>();

        _winerPos = _winerLightTransfrom.position;//勝った人に当たるライト
        _defScale = _playerScale[0].lossyScale;
    }
    void Start() {
       
        _timer = 0;
        _resulted = false;
        _lightPower = 0;
        _winerLightImage.fillAmount = _lightPower;//ライトを消す
        _f_scale = 0.5f;//スケールを1:1に
        _scale.x = _f_scale;
        _scale.y = _f_scale; 
        _scale.z = _f_scale;//スケール適用
        _kamifubuki.SetActive(false);
        #endregion
        _scaleCoroutine = StartCoroutine(PlayerScaleChenge());
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>=_waitTime && ! _resulted)
        {
            StopCoroutine(_scaleCoroutine);//スケール変え止め
            judge();
            if(_isSettled)StartCoroutine(LightOn());
            _resulted = true;
        }
    }

    /// <summary>
    /// 勝敗判定周り
    /// </summary>
    private void judge()
    {
        Scoremaneger.Instance().ScoreRandomSwitch();//ランダム止め
        _isSettled=Scoremaneger.Instance().Judge(out int winer,out int loser);//プレイヤー番号
        //決着がついていないなら
        if (!_isSettled)
        {
            //引き分け処理
            DrawText(_winOrLoseTMP[0]);
            DrawText(_winOrLoseTMP[1]);
            _playerScale[0].localScale = _defScale * 0.5f;
            _playerScale[1].localScale = _defScale * 0.5f;
            return;
        }

        _winerPos.x= _winOrLoseTransform[winer].position.x;//勝った方の位置をxだけ保存
        _winerLightTransfrom.position = _winerPos;//上から来る光を↑で保存した位置へ移動
        _kamiTransform.position = new Vector3(_winerPos.x, _kamiTransform.position.y, _kamiTransform.position.z);//紙吹雪も移動
        _kamifubuki.SetActive(true);//紙吹雪オン


        //勝ち負けの処理
        WinText(_winOrLoseTMP[winer]);
        LoseText(_winOrLoseTMP[loser]);
        _playerScale[winer].localScale = _defScale * 1f;
        _playerScale[loser].localScale = _defScale * 0.5f;
        
    }
    #region　テキスト変更関数
    /// <summary>
    /// テキスト変更勝ち側
    /// </summary>
    /// <param name="Text"></param>
    private void WinText(TextMeshProUGUI Text) 
    {
        Text.color= Color.red;
        Text.text = "WIN";
    }

    /// <summary>
    /// テキスト変更負け側
    /// </summary>
    /// <param name="Text"></param>
    private void LoseText(TextMeshProUGUI Text)
    {
        Text.color = Color.blue;
        Text.text = "LOSE";
    }
    /// <summary>
    /// テキスト変更引き分け
    /// </summary>
    /// <param name="Text"></param>
    private void DrawText(TextMeshProUGUI Text)
    {
        Text.color = Color.white;
        Text.text = "DRAW";
    }
    #endregion
    /// <summary>
    /// プレイヤースケールを変える
    /// </summary>
    /// <returns>1フレ待機</returns>
    IEnumerator PlayerScaleChenge()
    {
        while (true) {
            if(_isScaleUp) 
            {
                _f_scale += 0.0025f;
                if(_f_scale >0.8)_isScaleUp = false;
            }
            else
            {
                _f_scale -= 0.0025f;
                if( _f_scale <0.3)_isScaleUp=true;
            }
            //スケールサイズ変更
            _scale.x = _defScale.x * _f_scale;
            _scale.y = _defScale.y * _f_scale; 
            _scale.z = _defScale.z * _f_scale;
            //スケール適用
            _playerScale[0].localScale = _scale;
            //反対のスケールサイズ変更
            _scale.x = _defScale.x * (1 - _f_scale);
            _scale.y = _defScale.y * (1 - _f_scale);
            _scale.z = _defScale.z * (1 - _f_scale);
            //反対のスケール適用
            _playerScale[1].localScale = _scale;
            yield return null;
        }
    }

    /// <summary>
    /// 上から光を降ろす
    /// </summary>
    /// <returns>1フレ待機</returns>
    IEnumerator LightOn()
    {
        for(int i = 0; i < 200; i++) 
        {
            _winerLightImage.fillAmount = _lightPower;//上から光が来るやつ0~1
            _lightPower += 0.005f;
            yield return null; 
        }
    }
}
