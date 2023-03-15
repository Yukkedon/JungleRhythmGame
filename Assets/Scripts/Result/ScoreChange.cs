using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreChange : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI point;
    [SerializeField] TextMeshProUGUI perfect;
    [SerializeField] TextMeshProUGUI great;
    [SerializeField] TextMeshProUGUI bad;
    [SerializeField] TextMeshProUGUI miss;
    [SerializeField] TextMeshProUGUI rating;


    // Start is called before the first frame update
    void Start()
    {
        point.text      = "SCORE:"  + GameManager.Instance.point.ToString("d5");
        perfect.text    = "PERFECT:"+ GameManager.Instance.perfect.ToString("d5"); ;
        great.text      = "GREAT:"  + GameManager.Instance.great.ToString("d5"); ;
        bad.text        = "BAD:"    + GameManager.Instance.bad.ToString("d5"); ;
        miss.text       = "MISS:"   + GameManager.Instance.miss.ToString("d5"); ;
        rating.text     = "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
