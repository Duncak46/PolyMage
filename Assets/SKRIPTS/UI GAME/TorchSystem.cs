using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TorchSystem : MonoBehaviour
{
    
    public int HowMuch;
    public int iHave;
    public GameObject VFX;
    public TMP_Text HowMuchT;
    public TMP_Text iHaveT;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        HowMuch = gameObject.transform.childCount - 1;
        iHave = 0;
        sceneName = SceneManager.GetActiveScene().name;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (sceneName != "Level15" && sceneName != "Level25" && sceneName != "Level35")
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
                iHaveT.text = "0" + iHave.ToString();
            }
            else
            {
                iHaveT.text = iHave.ToString();
            }
            if (iHave == HowMuch)
            {
                VFX.transform.localScale = Vector3.Lerp(VFX.transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), Time.deltaTime * 5);
            }
        }
        else if(sceneName == "Level15")
        {
            HowMuchT.text = "";
            iHaveT.text = "   BOSS";
            HowMuch = 1;
            if (GameObject.Find("BossMega") == null)
            {
                iHave = 1;
                VFX.transform.localScale = Vector3.Lerp(VFX.transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), Time.deltaTime * 5);
            }
        }
        else if(sceneName == "Level25")
        {
            HowMuchT.text = "";
            iHaveT.text = "   BOSS";
            HowMuch = 1;
            if (GameObject.Find("Vosa") == null)
            {
                iHave = 1;
                VFX.transform.localScale = Vector3.Lerp(VFX.transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), Time.deltaTime * 5);
            }
        }
        else if (sceneName == "Level35")
        {
            HowMuchT.text = "";
            iHaveT.text = "   BOSS";
            HowMuch = 1;
            if (GameObject.Find("Carodej") == null)
            {
                iHave = 1;
                VFX.transform.localScale = Vector3.Lerp(VFX.transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), Time.deltaTime * 5);
            }
        }
    }
 
}
