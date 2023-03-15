using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    //Variables
    
    //Public
    public float spawnTimeLow = 1f, spawnTimeHigh = 3f;
    
    //Private
    [SerializeField] 
    private GameObject[] _colletables;
    private BoxCollider2D _col;
    
    
    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        StartCoroutine(SpawnCollectable(Random.Range(spawnTimeLow, spawnTimeHigh))); //Start spawning
    }

    IEnumerator SpawnCollectable(float time)
    {
        yield return new WaitForSecondsRealtime(time); //Wait for random time
        
        //Get bounds
        float x1 = transform.position.x - _col.bounds.size.x / 2f;
        float x2 = transform.position.x + _col.bounds.size.x / 2f;

        //Select random x position to spawn collectable
        Vector3 pos = transform.position;
        pos.x = Random.Range(x1, x2);
        Instantiate(_colletables[Random.Range(0, _colletables.Length)], pos, Quaternion.identity); //Spawn

        StartCoroutine(SpawnCollectable(Random.Range(spawnTimeLow, spawnTimeHigh))); //Restart spawn timer
    }
}
