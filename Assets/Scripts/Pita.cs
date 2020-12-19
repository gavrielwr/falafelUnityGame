using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pita : MonoBehaviour
{
    private const int MAX_DIE = 3;
    private int NUM_OF_INGREDIANTS_VICTORY = 3;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D pitaRigidbody2D;
    private float moveSpeed = 50f;
    private int pitaDiedCounter;
    private State state;

    private Dictionary<string, int> ORDERED_INGRADIANTS_AMOUNT = new Dictionary<string, int>()
    {
        {"pfFalafel(Clone)",1 },
        {"pfSalad(Clone)",1 },
        {"pfFries(Clone)", 1 }
    };

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    public static Pita instance;
    public static Pita GetInstance(){
        return instance;
    }

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }


    void Awake()
    {
        pitaDiedCounter = 0;
        pitaRigidbody2D = GetComponent<Rigidbody2D>();
        state = State.WaitingToStart;
        instance = this;
    }
    
    void Start()
    {
        
        
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (TestInput()=="right")
                {
                    // Start playing
                    state = State.Playing;
                    pitaRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    moveRight();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                if (TestInput()=="left")
                {
                    // Start playing
                    state = State.Playing;
                    pitaRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    moveLeft();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (TestInput()=="right")
                {
                    moveRight();
                }
                if (TestInput()=="left")
                {
                    moveLeft();
                }
                break;
            case State.Dead:
                break;
        }

        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     pitaRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
            
        // }
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     pitaRigidbody2D.velocity = new Vector2(-moveSpeed, 0f); 
        // }

    }

    private string TestInput()
    {
        if (Input.GetKey(KeyCode.RightArrow)){
            return "right";
        } 
        if(Input.GetKey(KeyCode.LeftArrow)){
            return "left";
        } 
        return "none";
    }

    private void moveRight(){
        pitaRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    private void moveLeft(){
        pitaRigidbody2D.velocity = new Vector2(-moveSpeed, 0f); 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.Log("collision");
        //Debug.Log(collider.name);
        //if (collider.name == "pfFalafel(Clone)") {
        //    Debug.Log("touched pfFalafeg!!!!!!!!! shitttt brooooo");
        //}
        
        HandleCollision(collider);
    }

    private void HandleCollision(Collider2D collider)
    {
        if (ORDERED_INGRADIANTS_AMOUNT.ContainsKey(collider.name))
        {
            Debug.Log("toched " + collider.name);
            ORDERED_INGRADIANTS_AMOUNT[collider.name]--;
            Debug.Log("counter is" + ORDERED_INGRADIANTS_AMOUNT[collider.name]);
            NUM_OF_INGREDIANTS_VICTORY--;
            if (ORDERED_INGRADIANTS_AMOUNT[collider.name] < 0)
            {
                Debug.Log("Too Many Ingrediants collected");
                pitaRigidbody2D.bodyType = RigidbodyType2D.Static;
                if (OnDied != null) OnDied(this, EventArgs.Empty);
            }
            if (NUM_OF_INGREDIANTS_VICTORY == 0)
            {
                Debug.Log("VICTORY!!!");
                pitaRigidbody2D.bodyType = RigidbodyType2D.Static;
                if (OnDied != null) OnDied(this, EventArgs.Empty);
            }
           
        }
        else
        {
            Debug.Log("Collected somthing not requested");
            pitaDiedCounter++;
        }

        if (pitaDiedCounter == MAX_DIE)
        {
            
            pitaRigidbody2D.bodyType = RigidbodyType2D.Static;
            if (OnDied != null) OnDied(this, EventArgs.Empty);
        }

    }

}
