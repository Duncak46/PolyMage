using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ColliderWithShop : MonoBehaviour
{
    [SerializeField] private Transform OdkudShop;
    [SerializeField] private Transform KamShop;
    [SerializeField] private Transform Shop;

    [SerializeField] private Transform OdkudNakup;
    [SerializeField] private Transform KamNakup;
    [SerializeField] private Transform Nakup;

    [SerializeField] private Transform OdkudText;
    [SerializeField] private Transform KamText;
    [SerializeField] private Transform Text;

    [SerializeField] private GameObject CanvasShop;
    [SerializeField] private GameObject Canvas1;
    [SerializeField] private GameObject Canvas2;

    private bool oteviraSe = false;
    private bool oteviraSe1 = false;
    private bool oteviraSe2 = false;

    private bool zaviraSe = false;
    private bool zaviraSe1 = false;
    private bool zaviraSe2 = false;

    public float speed = 1000f;

    void Start()
    {
        Nakup.position = OdkudNakup.position;
        Shop.position = OdkudShop.position;
        Text.position = OdkudText.position;
        CanvasShop.SetActive(false);
    }
    void Update()
    {
        //EXIT
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            oteviraSe = false;
            oteviraSe1 = false;
            oteviraSe2 = false;
            zaviraSe2 = true;
        }
        //Pohyb
        if (oteviraSe)
        {
            if (Shop != null && OdkudShop != null && KamShop != null)
            {
                // Interpolace pozice mezi transformA a transformB
                Shop.position = Vector3.MoveTowards(Shop.position, KamShop.position, speed * Time.deltaTime);

                // Volitelnì: Pokud chcete zjistit, zda objekt dosáhl cílové pozice
                if (Vector3.Distance(Shop.position, KamShop.position) < 0.01f)
                {
                    Debug.Log("Cíl dosažen");
                    oteviraSe1 = true;
                }
            }
        }
        if (zaviraSe)
        {
            if (Shop != null && KamShop != null && OdkudShop != null)
            {
                // Interpolace pozice mezi transformA a transformB
                Shop.position = Vector3.MoveTowards(Shop.position, OdkudShop.position, speed * Time.deltaTime);

                // Volitelnì: Pokud chcete zjistit, zda objekt dosáhl cílové pozice
                if (Vector3.Distance(Shop.position, OdkudShop.position) < 0.01f)
                {
                    Debug.Log("Cíl dosažen");
                    CanvasShop.SetActive(false);
                    Canvas1.SetActive(true);
                    Canvas2.SetActive(true);
                    Movement.pohyb = true;
                }
            }
        }
        if (oteviraSe1) 
        {
            if (Text != null && OdkudText != null && KamText != null)
            {
                // Interpolace pozice mezi transformA a transformB
                Text.position = Vector3.MoveTowards(Text.position, KamText.position, speed * Time.deltaTime);

                // Volitelnì: Pokud chcete zjistit, zda objekt dosáhl cílové pozice
                if (Vector3.Distance(Text.position, KamText.position) < 0.01f)
                {
                    Debug.Log("Cíl dosažen");
                    oteviraSe2 = true;
                }
            }
        }
        if(zaviraSe1)
        {
            if (Text != null && KamText != null && OdkudText != null)
            {
                // Interpolace pozice mezi transformA a transformB
                Text.position = Vector3.MoveTowards(Text.position, OdkudText.position, speed * Time.deltaTime);

                // Volitelnì: Pokud chcete zjistit, zda objekt dosáhl cílové pozice
                if (Vector3.Distance(Text.position, OdkudText.position) < 0.01f)
                {
                    Debug.Log("Cíl dosažen");
                    zaviraSe = true;
                }
            }
        }
        if (oteviraSe2) 
        {
            if (Nakup != null && OdkudNakup != null && KamNakup != null)
            {
                // Interpolace pozice mezi transformA a transformB
                Nakup.position = Vector3.MoveTowards(Nakup.position, KamNakup.position, speed * Time.deltaTime);

                // Volitelnì: Pokud chcete zjistit, zda objekt dosáhl cílové pozice
                if (Vector3.Distance(Nakup.position, KamNakup.position) < 0.01f)
                {
                    Debug.Log("Cíl dosažen");
                }
            }
        }
        if (zaviraSe2)
        {
            if (Nakup != null && KamNakup != null && OdkudNakup != null)
            {
                // Interpolace pozice mezi transformA a transformB
                Nakup.position = Vector3.MoveTowards(Nakup.position, OdkudNakup.position, speed * Time.deltaTime);

                // Volitelnì: Pokud chcete zjistit, zda objekt dosáhl cílové pozice
                if (Vector3.Distance(Nakup.position, OdkudNakup.position) < 0.01f)
                {
                    Debug.Log("Cíl dosažen");
                    zaviraSe1 = true;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            zaviraSe = false;
            zaviraSe1 = false;
            zaviraSe2 = false;
            Movement.pohyb = false;
            oteviraSe = true;
            Canvas1.SetActive(false);
            Canvas2.SetActive(false);
            CanvasShop.SetActive(true);
        }
    }
}
