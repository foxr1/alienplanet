using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    // Adapted from code found at https://forum.unity.com/threads/move-gameobject-along-a-given-path.455195/
    public GameObject[] PathNode;
    public float MoveSpeed;
    private float Timer;
    private static Vector3 CurrentPositionHolder;
    private int CurrentNode;
    private Vector3 startPosition;

    void Start()
    {
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

            // Adapted from answer at https://answers.unity.com/questions/1329163/how-to-use-quaternionslerp-with-transformlookat.html
            Quaternion lookOnLook = Quaternion.LookRotation(CurrentPositionHolder - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
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
