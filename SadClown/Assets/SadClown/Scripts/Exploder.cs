using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Exploder : MonoBehaviour
{
    public Transform balloon;
    public GameObject winPanel;
    public float speed = 10f;

    private enum State { Idle, Moving }
    private State state = State.Idle;

    private Vector2 startPos;
    public float idleDistance = 10f;
    public float idleSpeed = 2f;
    private PlayerInput playerInput;
    bool isPinMoving;

    public GameObject idleEyes;
    public GameObject idleMouth;
    public GameObject worriedEyes;
    public GameObject worriedMouth;
    public GameObject sadEyes;
    public GameObject sadMouth;
    public GameObject confetti;
    public GameObject popOnomatopeya;
    public GameObject explodedBalloon;
    public GameObject balloonAsset;

    private void Start()
    {
        startPos = transform.position;
        state = State.Idle;
        isPinMoving = false;
        SetFaceState("Idle");
        //StartCoroutine(ChangeFaceAfterTime(2f));
    }
    void Update()
    {
        IdleState();
        if (isPinMoving) 
        { 
            transform.position = Vector2.MoveTowards(transform.position, balloon.position, speed * Time.deltaTime); 
        }
    }

    private void IdleState()
    {
        if (state == State.Idle) 
        {
            float xOffset = Mathf.PingPong(Time.time * idleSpeed, idleDistance) - (idleDistance / 2);
            transform.position = new Vector2(startPos.x + xOffset, startPos.y);
        }
    }

    public void PinMovement(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed) 
        {
            if (state == State.Idle)
            {
                if (balloon != null)
                {
                    state = State.Moving;
                    isPinMoving = true;
                    SetFaceState("Worried");
                    Debug.Log("se mueve");
                }
            }
        }
        
    }

    private void StateUpdate()
    {
        if (state == State.Idle)
        {
            state = State.Moving;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloon"))
        {
            Debug.Log("on trigger tag");
            speed = 0f;
            StartCoroutine(WinSequence());
        }
    }
    private IEnumerator WinSequence()
    {
        SetFaceState("Sad");
        WinningAssets();

        yield return new WaitForSeconds(1f);
        winPanel.SetActive(true);
        enabled = false;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
    }

    //private IEnumerator ChangeFaceAfterTime(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SetFaceState("Worried");
    //}

    private void SetFaceState(string state)
    {
        idleEyes.SetActive(false);
        idleMouth.SetActive(false);
        worriedEyes.SetActive(false);
        worriedMouth.SetActive(false);
        sadEyes.SetActive(false);
        sadMouth.SetActive(false);

        switch (state)
        {
            case "Idle":
                idleEyes.SetActive(true);
                idleMouth.SetActive(true);
                break;
            case "Worried":
                worriedEyes.SetActive(true);
                worriedMouth.SetActive(true);
                break;
            case "Sad":
                sadEyes.SetActive(true);
                sadMouth.SetActive(true);
                break;
        }
    }
    private void WinningAssets()
    {
        if (balloonAsset != null)  balloonAsset.SetActive(false);
        if (confetti != null)  confetti.SetActive(true);
        if (popOnomatopeya != null)  popOnomatopeya.SetActive(true);
        if (explodedBalloon != null)  explodedBalloon.SetActive(true);
    }
}
