using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) //Destroys collectibles that enter trigger
    {
        if(other.tag == "Fish" || other.tag == "Bomb" )
            Destroy(other.gameObject); //Destroy Collectable
    }
}
