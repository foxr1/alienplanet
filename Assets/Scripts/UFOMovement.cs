using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    // Adapted from code found at https://forum.unity.com/threads/move-gameobject-along-a-given-path.455195/

    public GameObject[] PathNode;
    public float MoveSpeed;
    float Timer;
    static Vector3 CurrentPositionHolder;
    int CurrentNode;
    private Vector3 startPosition;


    // Use this for initialization 
    void Start()
    {
        //PathNode = GetComponentInChildren<>();
        CheckNode();
    }

    void CheckNode()
    {
        Timer = 0;
        startPosition = transform.position;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Timer += Time.deltaTime * MoveSpeed;

        if (transform.position != CurrentPositionHolder)
        {
            transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
            transform.LookAt(CurrentPositionHolder);
        }
        else
        {
            if (CurrentNode < PathNode.Length - 1)
            {
                CurrentNode++;
            }
            else if (CurrentNode == PathNode.Length - 1)
            {
                CurrentNode = 0;
            }
            CheckNode();
        }
    }
}
