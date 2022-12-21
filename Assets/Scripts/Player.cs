using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour

{

    private Rigidbody2D rb2D;
    private Vector2 _direction;
    private Vector2 _respawnPoint;


    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;

    private float initialSpeed;
    private bool isJumping;
    private bool _isRunning;
    private bool _isDead;
    private float moveHorizontal;
    private float moveVertical;


    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public Vector2 respawnPoint
    {
        get { return _respawnPoint; }
        set { _respawnPoint = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 1f;
        runSpeed = 4f;
        jumpForce = 30f;
        isJumping = false;
        initialSpeed = moveSpeed;
        respawnPoint = transform.position;
        OnReload();
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        OnRun();

    }

    private void FixedUpdate()
    {
        OnMove();
    }

    void OnReload()
    {

        if (GameValues.scene == 1)
        {
            if (GameValues.playerPositionScreen1.SqrMagnitude() != 0)
            {
                transform.position = GameValues.playerPositionScreen1;
            }
        }


        if (GameValues.scene == 2)
        {
            if (GameValues.playerPositionScreen2.SqrMagnitude() != 0)
            {
                transform.position = GameValues.playerPositionScreen2;
            }
           
        }
    }

    void OnInput()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

    }

    void OnMove()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            _direction = new Vector2(moveHorizontal * moveSpeed, 0f);
            rb2D.AddForce(_direction, ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            _direction = new Vector2(0f, moveVertical * jumpForce);
            rb2D.AddForce(_direction, ForceMode2D.Impulse);
        }
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
            _isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = initialSpeed;
            _isRunning = false;
        }

    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "NoFloor")
        {
            _isDead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }

        if (collision.gameObject.tag == "NoFloor")
        {
            _isDead = false;
        }

    }
}
