using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticles : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.4f, gameObject.transform.position.z);
    }
    public void setParticleColor(Color32 color)
    {
        ParticleSystem mPS = gameObject.GetComponent<ParticleSystem>();
        var col = mPS.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 0), new GradientColorKey(color, 1) }, new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0, 1) });

        col.color = grad;
    }
}
