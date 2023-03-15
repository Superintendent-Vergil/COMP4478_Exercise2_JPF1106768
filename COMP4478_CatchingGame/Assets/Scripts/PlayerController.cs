using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables
    
    //Public
    public float speed = 10f;
    public float boundsAdjust = 0f;
    public AudioSource collectFish, collectBomb;

    //Private
    [SerializeField] private TMP_Text _scoreText;

    private Rigidbody2D _rb;
    private float minX, maxX; //Bounds

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        //Set bounds
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = -bounds.x + boundsAdjust;
        maxX = bounds.x - boundsAdjust;
    }
    
    void FixedUpdate()
    {
        //Movement
        Vector2 vel = _rb.velocity;
        vel.x = Input.GetAxis("Horizontal") * speed; //Get horizontal input, multiply by speed.
        _rb.velocity = vel; //Set velocity
        
        //Bounds
        var pos = transform.position; //Get position
        if (pos.x < minX) //Too far left
            pos.x = minX;
        else if (pos.x > maxX) //Too far right
            pos.x = maxX;
        transform.position = pos; //Set position
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            //Destroy bomb
            Destroy(other.gameObject);
            //Game over
            GameManager.Instance.UpdateGameState(GameState.GameOver);
            //Play collect sound
            if(collectBomb) collectBomb.PlayOneShot(collectBomb.clip);
        }
        else if (other.tag == "Fish")
        {
            //Kill fish
            Destroy(other.gameObject);
            //Increment Score
            GameManager.score++;
            _scoreText.text = "Score: " + GameManager.score;
            //Play collect sound
            if(collectFish) collectFish.PlayOneShot(collectFish.clip);
        }
    }
}
