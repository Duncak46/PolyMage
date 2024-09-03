using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelector : MonoBehaviour
{
    private int levelNum = 1;
    private Vector3 level1;
    private Vector3 level2;
    private Vector3 level3;

    public Transform level1T;
    public Transform level2T;
    public Transform level3T;

    private Vector3 target;
    private float speed = 10.0f;

    public Transform player;
    void Start()
    {
        level1 = level1T.position;
        level2 = level2T.position;
        level3 = level3T.position;
        target = level1; 
        levelNum = 1;
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
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (levelNum < 3 )
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
