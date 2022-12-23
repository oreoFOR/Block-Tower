using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon : MonoBehaviour
{
    float speed =0;
    [SerializeField] Vector2 minMaxSpeed = new Vector2(1,3);
    [SerializeField] float speedUpTime = 15;
    [SerializeField] Transform playerCube;
    [SerializeField] float maxDist;
    [SerializeField] float killDist =2;
    public float temp;
    bool gameOver;
    bool moving;
    private void Start()
    {
        GameEvents.instance.onPlatformGenerate += MoveCamera;
        GameEvents.instance.onGameOver += PlayerFallenOff;
        speed = 0;
    }
    void MoveCamera()
    {
        if (!moving)
        {
            LeanTween.value(minMaxSpeed.x, minMaxSpeed.y, speedUpTime).setOnUpdate((float f) => { speed = f; });
            moving = true;
        }
    }
    void PlayerFallenOff()
    {
        gameOver = true;
    }
    private void Update()
    {
        temp = (playerCube.position.y - transform.position.y);
        if ((playerCube.position.y - transform.position.y) > killDist && !gameOver)
        {
            gameOver = true;
            GameEvents.instance.GameOver();
        }
        else if(!gameOver)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            if((transform.position.y - playerCube.position.y)> maxDist)
            {
                transform.position = new Vector3(transform.position.x, playerCube.position.y + maxDist, transform.position.z);
            }
        }
    }
}
