using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TorchSystem : MonoBehaviour
{
    
    public int HowMuch;
    public int iHave;
    public GameObject VFX;
    public TMP_Text HowMuchT;
    public TMP_Text iHaveT;
    // Start is called before the first frame update
    void Start()
    {
        HowMuch = gameObject.transform.childCount - 1;
        iHave = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (HowMuch < 10)
        {
            HowMuchT.text = "/0" + HowMuch;
        }
        else 
        {
            HowMuchT.text = "/" + HowMuch;
        }

        if (iHave < 10)
        {
            iHaveT.text = "0"+iHave.ToString();
        }
        else
        {
            iHaveT.text = iHave.ToString();
        }
        if (iHave == HowMuch)
        {
            VFX.transform.localScale = Vector3.Lerp(VFX.transform.localScale, new Vector3(0.25f,0.25f,0.25f), Time.deltaTime * 5);
        }
    }
 
}
