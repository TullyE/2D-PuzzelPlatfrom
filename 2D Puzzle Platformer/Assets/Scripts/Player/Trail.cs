using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//trail by Adam
public class Trail : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        // gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, gameObject.transform.position.z);
    }

    public void setTrailColor(Color32 color)
    {
        TrailRenderer mTR =  gameObject.GetComponent<TrailRenderer>();
        mTR.startColor = color;
        color.a = 0;
        mTR.endColor = color;
    }
}
