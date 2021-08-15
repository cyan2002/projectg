using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour
{
    public P1DragDownBlock[] movingStones = new P1DragDownBlock[4];
    public P1Switch[] levers = new P1Switch[4];
    public Vector3[,] endPoints = new Vector3[4, 4];
    private bool[] onOff = new bool[4];
    int[] counters = new int[4];

    private Vector3 tempVector;

    private bool canMove = true;

    //lever 1 - green
    //lever 2 - magenta
    //lever 3 - gold
    //lever titantium

    void Start()
    {
        tempVector = new Vector3(-14f, 5f, 0f);

        for (int i = 0; i < 4; i++)
        {
            endPoints[0, i] = tempVector;
            tempVector = new Vector3(tempVector.x, tempVector.y - 1, tempVector.z);
        }

        tempVector = new Vector3(tempVector.x, 4, tempVector.z);

        for (int a = 1; a < 4; a++)
        {
            for (int j = 0; j < 4; j++)
            {
                endPoints[a, j] = tempVector;
                tempVector = new Vector3(tempVector.x, tempVector.y - 1, tempVector.z);
            }
            tempVector = new Vector3(tempVector.x, 4, tempVector.z);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (levers[0].counter > 0)
            {
                //0,3 ---> counter = 0
                //1,3 ---> counter = 1
                //2,1 ---> counter = 2
                //3,1 ---> counter = 3
                if (counters[0] > 3)
                {
                    counters[0] = 0;
                }


                if (counters[0] == 0)//move green in first chain set
                {
                    findDirection(0, 0, 3);
                }
                else if (counters[0] == 1)
                {
                    findDirection(1, 1, 3);
                }
                else if (counters[0] == 2)
                {
                    findDirection(2, 2, 1);
                }
                else if (counters[0] == 3)
                {
                    findDirection(3, 3, 1);
                }
                else
                {

                }

                counters[0]++;
                levers[0].counter--;

            }
            else if (levers[1].counter > 0)
            {
                //0,2
                //1,0
                //2,2
                //3,3

                if (counters[1] > 3)
                {
                    counters[1] = 0;
                }


                if (counters[1] == 0)//move green in first chain set
                {
                    findDirection(0, 0, 2);
                }
                else if (counters[1] == 1)
                {
                    findDirection(1, 1, 0);
                }
                else if (counters[1] == 2)
                {
                    findDirection(2, 2, 2);
                }
                else if (counters[1] == 3)
                {
                    findDirection(3, 3, 3);
                }
                else
                {

                }



                counters[1]++;
                levers[1].counter--;
            }
            else if (levers[2].counter > 0)
            {
                //0,1
                //1,1
                //2,0
                //3,2

                if (counters[2] > 3)
                {
                    counters[2] = 0;
                }


                if (counters[2] == 0)//move green in first chain set
                {
                    findDirection(0, 0, 1);
                }
                else if (counters[2] == 1)
                {
                    findDirection(1, 1, 1);
                }
                else if (counters[2] == 2)
                {
                    findDirection(2, 2, 0);
                }
                else if (counters[2] == 3)
                {
                    findDirection(3, 3, 2);
                }
                else
                {

                }
                counters[2]++;
                levers[2].counter--;

            }
            else if (levers[3].counter > 0)
            {
                //0,0
                //1,2
                //2,3
                //3,0

                if (counters[3] > 3)
                {
                    counters[3] = 0;
                }


                if (counters[3] == 0)//move green in first chain set
                {
                    findDirection(0, 0, 0);
                }
                else if (counters[3] == 1)
                {
                    findDirection(1, 1, 2);
                }
                else if (counters[3] == 2)
                {
                    findDirection(2, 2, 3);
                }
                else if (counters[3] == 3)
                {
                    findDirection(3, 3, 0);
                }
                else
                {

                }
                counters[3]++;
                levers[3].counter--;
            }
            else
            {

            }
        }
    }

    void findDirection(int count, int col, int row)
    {
        if (movingStones[count].transform.position.y != endPoints[col, row].y)
        {

            canMove = false;
            if (movingStones[count].transform.position.y - 40 > endPoints[col, row].y)
            {
                //move down
                StartCoroutine(moveBlocksDown(endPoints[col, row].y, count, col, row));
            }
            else
            {
                //move up
                StartCoroutine(moveBlocksUp(endPoints[col, row].y, count, col, row));
            }
        }
    }

    IEnumerator moveBlocksUp(float end, int movingBlockNumber, int col, int row)
    {

        movingStones[movingBlockNumber].on = true;
        movingStones[movingBlockNumber].up = true;
        movingStones[movingBlockNumber].endPoint = endPoints[col, row];

        Debug.Log(movingStones[movingBlockNumber].transform.position.y - 40);
        Debug.Log(end);

        while (movingStones[movingBlockNumber].transform.position.y - 40 < end)
        {
            yield return null;
        }

        movingStones[movingBlockNumber].on = false;
        canMove = true;
        Debug.Log("Here!");

    }

    IEnumerator moveBlocksDown(float end, int movingBlockNumber, int col, int row)
    {
        movingStones[movingBlockNumber].on = true;
        movingStones[movingBlockNumber].up = false;
        movingStones[movingBlockNumber].endPoint = endPoints[col, row];


        while (movingStones[movingBlockNumber].transform.position.y - 40 > end)
        {
            yield return null;
        }

        movingStones[movingBlockNumber].on = false;
        canMove = true;
    }
}
