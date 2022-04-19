using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make the Camera foloow the player should be changed *I THINK* for each level depending on the width
//not too sure yet -TullyE
public class CameraScript : MonoBehaviour
{
    public GameObject player; //object for the camera to follow

    //the parameters for the level may change per level ****
    public float xMin = -500;

    public float xMax = 500f;

    public float yMin = 0;

    public float yMax = 101;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position =
            new Vector3(x, y, gameObject.transform.position.z);
    }
}
