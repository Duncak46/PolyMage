using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public static bool inLevel = false;
    private bool JustOne = true;
    private static DoNotDestroy instance;

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
    }
    void Update()
    {
        if (inLevel == true && !JustOne)
        {
            //Pøidat jestli je to boss nebo ne
            game.Play();
            JustOne = true;
            menu.Stop();
        }
        if (!inLevel && JustOne)
        {
            menu.Play();
            JustOne= false;
            game.Stop();
            boss.Stop();
        }
    }
}
