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

    private void Start()
    {
        startPos = transform.position;
        state = State.Idle;
        isPinMoving = false;
        SetFaceState("Idle");
        StartCoroutine(ChangeFaceAfterTime(2f));
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
                    SetFaceState("Action");
                    Debug.Log("si");
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
            Debug.Log("ontrigger");
            StartCoroutine(WinSequence());
        }
    }
    private IEnumerator WinSequence()
    {
        SetFaceState("Collision");

        yield return new WaitForSeconds(2f);
        winPanel.SetActive(true);
        enabled = false;
    }
    private IEnumerator ChangeFaceAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetFaceState("Action");
    }

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
            case "Action":
                worriedEyes.SetActive(true);
                worriedMouth.SetActive(true);
                break;
            case "Collision":
                sadEyes.SetActive(true);
                sadMouth.SetActive(true);
                break;
        }
    }
}
