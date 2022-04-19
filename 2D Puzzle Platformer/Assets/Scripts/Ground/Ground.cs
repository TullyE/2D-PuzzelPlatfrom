using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//all of the grounds can use this same script
//TullyE
public class Ground : MonoBehaviour
{
    public GameLogic gameLogic;

    public TilemapCollider2D mC;

    private Tilemap sprite;

    //define this gournd number.
    public int groundNum;

    void Start()
    {
        this.sprite = GetComponent<Tilemap>();
        updateState();
    }

    // Update is called once per frame
    void Update()
    {
        //access this gournd's collider

    }

    public virtual void updateState()
    {
        mC = GetComponent<TilemapCollider2D>();

        //set the opacity based on the ground lvl number
        sprite.color =
            gameLogic.getLvl() == this.groundNum
                ? new Color(1, 1, 1, 1)
                : new Color(1, 1, 1, 0.2f);

        //if the ground num is this num then enable this collider.
        mC.enabled = gameLogic.getLvl() == this.groundNum;
    }
}
