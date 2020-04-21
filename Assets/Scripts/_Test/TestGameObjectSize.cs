using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObjectSize : MonoBehaviour
{

    public GameObject gameObjectGO;
    public GameObject moveObject;
    public int position;
    public HideOutOrientation orientation;

    // Update is called once per frame
    void Update()
    {
        if (gameObjectGO == null)
            return;

        //Debug.Log(gameObjectGO.GetComponent<Renderer>().bounds.size.x);
    }

    public void MoveObjectToPositionWithScale() ///Same code, just changed render bound for scale
    {
        int availablePositions = 0;
        if (this.orientation == HideOutOrientation.ZAxis) {
            availablePositions = (int)Mathf.Floor(gameObjectGO.transform.localScale.z);
        } else if (this.orientation == HideOutOrientation.XAxis) {
            availablePositions = (int)Mathf.Floor(gameObjectGO.transform.localScale.x);
        }
        int tempCurrentPosition = this.position < availablePositions ? this.position : availablePositions - 1;
        tempCurrentPosition = this.position <= 0 ? 0 : tempCurrentPosition;

        int sideMultiplier = 1;

        if ((int)Mathf.Floor(availablePositions / 2) < this.position) {
            sideMultiplier /= -1;
        }
        float extraXPos = 0;
        float extraZPos = 0;

        if (this.orientation == HideOutOrientation.ZAxis) {
            extraZPos = (((Mathf.Abs((availablePositions / 2) - tempCurrentPosition)) - (availablePositions % 2 == 0 ? 0.5f : 0)) * sideMultiplier);
            extraXPos = ((gameObjectGO.transform.localScale.x / 2) + 1);
        } else if (this.orientation == HideOutOrientation.XAxis) {
            extraXPos = (((Mathf.Abs((availablePositions / 2) - tempCurrentPosition)) - (availablePositions % 2 == 0 ? 0.5f : 0)) * sideMultiplier);
            extraZPos = ((gameObjectGO.transform.localScale.z / 2) + 1);
        }

        float x = gameObjectGO.transform.position.x + extraXPos;
        float y = gameObjectGO.transform.position.y;
        float z = gameObjectGO.transform.position.z + extraZPos;

        Vector3 finalPosition = new Vector3(x, y, z);
        moveObject.transform.position = finalPosition;
    }

    public void MoveObjectToPosition()
    {
        int availablePositions = 0;
        if (this.orientation == HideOutOrientation.ZAxis) {
            availablePositions = (int)Mathf.Floor(gameObjectGO.GetComponent<Renderer>().bounds.size.z);
        } else if (this.orientation == HideOutOrientation.XAxis) {
            availablePositions = (int)Mathf.Floor(gameObjectGO.GetComponent<Renderer>().bounds.size.x);
        }
            int tempCurrentPosition = this.position < availablePositions ? this.position : availablePositions - 1;
        tempCurrentPosition = this.position <= 0 ? 0 : tempCurrentPosition;

        int sideMultiplier = 1;

        if((int)Mathf.Floor(availablePositions / 2) < this.position) {
            sideMultiplier /= -1;
        }
        float extraXPos = 0;
        float extraZPos = 0;

        if(this.orientation == HideOutOrientation.ZAxis) {
            extraZPos = (((Mathf.Abs((availablePositions / 2) - tempCurrentPosition)) - (availablePositions % 2 == 0 ? 0.5f : 0)) * sideMultiplier);
            extraXPos = (gameObjectGO.GetComponent<Renderer>().bounds.extents.x + 1);
        } else if (this.orientation == HideOutOrientation.XAxis) {
            extraXPos = (((Mathf.Abs((availablePositions / 2) - tempCurrentPosition)) - (availablePositions % 2 == 0 ? 0.5f : 0)) * sideMultiplier);
            extraZPos = (gameObjectGO.GetComponent<Renderer>().bounds.extents.z + 1);
        }

        float x = gameObjectGO.transform.position.x + extraXPos;
        float y = gameObjectGO.transform.position.y;
        float z = gameObjectGO.transform.position.z + extraZPos;

        Vector3 finalPosition = new Vector3(x, y, z);
        moveObject.transform.position = finalPosition;
    }
}
