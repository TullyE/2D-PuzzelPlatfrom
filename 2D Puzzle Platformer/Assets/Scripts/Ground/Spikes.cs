using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : Ground
{
    public Material shader;
    public Material noShader;
    // private Tilemap spikeSprite;

    // void Start()
    // {
    //     this.sprite = GetComponent<Tilemap>();
    // }

    public override void updateState()
    {
        base.updateState();
        var render = gameObject.GetComponent<Renderer>();
        if(this.groundNum == gameLogic.getLvl())
        {
            // print(render);
            if(render!=null)
            {
                render.material = shader;   
            }
        }
        else
        {
            render.material = noShader;
        }
    }
}
