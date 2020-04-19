using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObjectSize : MonoBehaviour
{

    public GameObject gameObjectGO;
    public GameObject moveObject;
    public int position;

    // Update is called once per frame
    void Update()
    {
        if (gameObjectGO == null)
            return;

        //Debug.Log(gameObjectGO.GetComponent<Renderer>().bounds.size.x);
    }

    public void MoveObjectToPosition()
    {
        int availablePositions = 0;
        availablePositions = (int)Mathf.Floor(gameObjectGO.GetComponent<Renderer>().bounds.size.x);
        int tempCurrentPosition = this.position < availablePositions ? this.position : availablePositions - 1;
        tempCurrentPosition = this.position <= 0 ? 0 : tempCurrentPosition;

        int sideMultiplier = 1;

        if((int)Mathf.Floor(availablePositions / 2) < this.position) {
            sideMultiplier /= -1;
        }
        float extraXPos = (((Mathf.Abs((availablePositions / 2) - tempCurrentPosition)) - (availablePositions % 2 == 0 ? 0.5f : 0)) * sideMultiplier);
        float x = gameObjectGO.transform.position.x + extraXPos;
        float y = gameObjectGO.transform.position.y;
        float z = gameObjectGO.transform.position.z + 1;

        Vector3 finalPosition = new Vector3(x, y, z);
        moveObject.transform.position = finalPosition;
    }
}
