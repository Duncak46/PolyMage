using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoNotDestroy : MonoBehaviour
{
    public static bool inLevel = false;
    private bool JustOne = true;
    private static DoNotDestroy instance;

    private Slider AudioMuch;
    public static float audioMuch = 1;

    public AudioSource menu;
    public AudioSource game;
    public AudioSource boss;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Najdi slider jednou na za��tku, pokud je ji� ve sc�n�
        AudioMuch = FindObjectOfType<Slider>();

        if (AudioMuch != null)
        {
            AudioMuch.value = audioMuch;
            AudioMuch.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        // Aktualizuj hodnotu jen p�i zm�n� ze slideru
        audioMuch = value;
        game.volume = audioMuch;
        menu.volume = audioMuch;
        boss.volume = audioMuch;
    }

    void Update()
    {
        if (inLevel && !JustOne)
        {
            // P�idat jestli je to boss nebo ne
            game.Play();
            JustOne = true;
            menu.Stop();
        }

        if (!inLevel && JustOne)
        {
            menu.Play();
            JustOne = false;
            game.Stop();
            boss.Stop();
        }
    }
}
