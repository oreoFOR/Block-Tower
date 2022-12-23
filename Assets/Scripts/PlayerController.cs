using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement movement;
    bool controllingPlayer;

    Vector3 startMousePos;
    bool draggingFinger;
    float distTravelled;
    [SerializeField] float swipeDist;
    private void Start()
    {
        movement = GetComponent<Movement>();
        GameEvents.instance.onGameOver += DisableControls;
        controllingPlayer = true;
        StartCoroutine(SwipeControls2());
    }
    void DisableControls()
    {
        controllingPlayer = false;
    }
    void Update()
    {
        if (controllingPlayer)
        {
            #region arrow keys
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveBlock(Vector3.right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveBlock(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveBlock(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveBlock(Vector3.back);
            }
            #endregion
        }
    }
    void MoveBlock(Vector3 direction)
    {
        GameEvents.instance.StartGame();
        StartCoroutine(movement.Roll(direction));
    }
    #region swipeControls
    IEnumerator SwipControls()
    {
        while (controllingPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePos = Input.GetTouch(0).position;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector3 endPos = Input.GetTouch(0).position;
                float yChange = endPos.y - startMousePos.y;
                float xChange = endPos.x - startMousePos.x;
                print("x change: " + xChange);
                print("y change: " + yChange);
                if (MOD(xChange) > MOD(yChange))
                {
                    if (xChange > swipeDist)
                    {
                        MoveBlock(Vector3.right);
                    }
                    else if (xChange < -swipeDist)
                    {
                        MoveBlock(Vector3.left);
                    }
                }
                else
                {
                    if(yChange > swipeDist)
                    {
                        MoveBlock(Vector3.forward);
                    }
                    else if(yChange < -swipeDist)
                    {
                        MoveBlock(Vector3.back);
                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator SwipeControls2()
    {
        while (controllingPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePos = Input.GetTouch(0).position;
                draggingFinger = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                draggingFinger = false;
            }
            if (draggingFinger)
            {
                Vector3 endPos = Input.GetTouch(0).position;
                float yChange = endPos.y - startMousePos.y;
                float xChange = endPos.x - startMousePos.x;
                if(MOD(xChange) > swipeDist)
                {
                    draggingFinger = false;
                    if(xChange > swipeDist)
                    {
                        MoveBlock(Vector3.right);
                    }
                    else
                    {
                        MoveBlock(Vector3.left);
                    }
                }
                else if(MOD(yChange) > swipeDist)
                {
                    draggingFinger = false;
                    if (yChange > swipeDist)
                    {
                        MoveBlock(Vector3.forward);
                    }
                    else
                    {
                        MoveBlock(Vector3.back);
                    }
                }
            }
            yield return null;
        }
    }
    #endregion
    float MOD(float a)
    {
        return a > 0 ? a : -a;
    }
}
