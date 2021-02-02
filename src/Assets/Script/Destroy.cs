using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private ParticleSystem particle;
    private SpriteRenderer sr;




    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}


