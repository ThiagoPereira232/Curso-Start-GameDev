using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private Rigidbody2D rig;

    private float initialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool canRoll = true;

    private Vector2 _direction;

    private int handlingObj;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool isCutting { get => _isCutting; set => _isCutting = value; }
    public bool isDigging { get => _isDigging; set => _isDigging = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    // utilizado para capturar inputs, logicas que n�o envolvam fisicas
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            handlingObj = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handlingObj = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            handlingObj = 2;
        }

        OnInput();
        OnRun();
        OnRolling();
        OnCutting();
        OnDig();
    }

    // utilizado para coisas relacionadas a fisica
    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement

    void OnDig()
    {
        if(handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
    }

    void OnCutting()
    {
        if(handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    void OnRolling()
    {
        // 1 = direito, 0 = esquerdo
        if(canRoll)
        {
            if (Input.GetMouseButtonDown(1))
            {
                canRoll = false;
                speed = runSpeed;
                _isRolling = true;
                Invoke("FinishRolling", 0.02f);
                Invoke("CanNewRolling", 0.8f);
            }
        }   
        /*if (Input.GetMouseButtonUp(1))
        {
            speed = initialSpeed;
            _isRolling = false;
        }*/
    }

    private void FinishRolling()
    {
        speed = initialSpeed;
        _isRolling = false;
    }
    private void CanNewRolling()
    {
        canRoll = true;
    }

    #endregion
}
