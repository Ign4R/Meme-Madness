using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput = 0;
    public float xLimit = 8.5f;
    public float speed = 10f;
    private Rigidbody2D _rb;
    private Animator _animator;
    public GameObject rigby;
    public GameObject mordecai;
    public GameObject currentCharacter;

    private float timerStep = 0.5f;

    private void Awake()
    {
        currentCharacter = rigby;
        horizontalInput = 0;
        _animator = currentCharacter.GetComponent<Animator>();
    }

    private void Update()
    {
        
        Move();
        Flip();
        timerStep -= Time.deltaTime;
        if (horizontalInput !=0f && timerStep <= 0.0f)
        {
            AudioManager.AudioInstance.PlaySFX("walk");
            timerStep = 0.3f;
            
            if(currentCharacter == rigby)
                _animator.SetBool("WalkRigby", true);
            else if(currentCharacter == mordecai)
                _animator.SetBool("WalkMordecai", true);
        }
        
        if(horizontalInput == 0)
        {
            if(currentCharacter == rigby)
                _animator.SetBool("WalkRigby", false);
            else if(currentCharacter == mordecai)
                _animator.SetBool("WalkMordecai", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Q)) SwitchCharacter();
    }

    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        
        if (transform.position.x < -xLimit)
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        else if (transform.position.x > xLimit)
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
    }
    
    private void SwitchCharacter()
    {
        if(currentCharacter == rigby)
        {
            rigby.SetActive(false);
            mordecai.SetActive(true);
            _animator = GetComponentInChildren<Animator>();
            currentCharacter = mordecai;
        }
        else if (currentCharacter == mordecai)
        {
            mordecai.SetActive(false);
            rigby.SetActive(true);
            _animator = GetComponentInChildren<Animator>();
            currentCharacter = rigby;
        }
    }

    private void Flip()
    {
        if (horizontalInput < 0) 
            transform.localScale = new Vector3(-1, 1,1);
        else
            transform.localScale = new Vector3(1, 1,1);
    }
}
