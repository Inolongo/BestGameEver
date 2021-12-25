using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;


public class MushroomController : MonoBehaviour
{
    [SerializeField] private float mushroomSpeed;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float followOffset;
    public bool IsFollowingPlayer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _heath = 100f;
   

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (IsFollowingPlayer)
        {
           // FollowPlayer();
        }
    }
    
    
    

   
}