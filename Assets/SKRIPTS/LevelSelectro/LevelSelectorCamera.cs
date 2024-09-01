using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectorCamera : MonoBehaviour
{
    private int levelNum = 1;
    private Vector3 level1;
    private Vector3 level2;
    private Vector3 level3;
    private Vector3 level4;
    private Vector3 level5;

    public Transform level1T;
    public Transform level2T;
    public Transform level3T;
    public Transform level4T;
    public Transform level5T;

    private Vector3 target;
    private float speed = 10.0f;

    public Transform player;
    void Start()
    {
        level1 = level1T.position;
        level2 = level2T.position;
        level3 = level3T.position;
        level4 = level4T.position;
        level5 = level5T.position;

        levelNum = LevelManager.level;
        if (levelNum == 1) { target = level1; }
        if (levelNum == 2) { target = level2; }
        if (levelNum == 3) { target = level3; }
        if (levelNum == 4) { target = level4; }
        if (levelNum == 5) { target = level5; }
        transform.position = new Vector3(transform.position.x,transform.position.y,target.z);
    }

    // Update is called once per frame
    void Update()
    {
        switch (levelNum)
        {
            case 1: target = level1; break;
            case 2: target = level2; break;
            case 3: target = level3; break;
            case 4: target = level4; break;
            case 5: target = level5; break;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (levelNum < 5 )
            {
                levelNum++;
            }
        }

        // Ovládání rotace doleva
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (levelNum > 1)
            {
                levelNum--;
            }
        }
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }
}
