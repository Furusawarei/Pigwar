using TMPro;
using UnityEngine;

public class ResultTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreborad = new TextMeshProUGUI[2];
    // Start is called before the first frame update
    void Start()
    {
        _scoreborad[0].text = Scoremaneger.Instance().PlayerScore[0].ToString();
        _scoreborad[1].text = Scoremaneger.Instance().PlayerScore[1].ToString();
    }
}
