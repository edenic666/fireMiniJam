using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Jump Settings")]
    public bool grounded;
    [SerializeField] public int jumpforce;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = .2f;

    [Header("Movement Settings")]
    public int speed;
    public bool movementLock;

    //combine this behaviour (+ quest tracker) into a single event manager script after gamejam when i have more time lol
    [Header("Torch Detection")]
    public bool torchDetection;
    [SerializeField] public Transform torchCheck;
    [SerializeField] public LayerMask torchLayer;
    public TorchBehavior torch;


    [Header("Quest Tracker")]
    public bool hasItem;

    private Rigidbody2D rb;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementLock = false;
        hasItem= false;
    }

    void Update()
    {
        playerMovement();
        torchInteraction();
    }

    public void torchInteraction()
    {
        if (torch != null && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Torch Touched");

            torch.activated = true;
        }
    }

    public void playerMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        grounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

        while (movementLock) 
        {
            speed = 0;
            jumpforce = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontalInput * speed,
            rb.linearVelocity.y
        );
    }

    public bool lockMovement(bool locked)
    {
        if (movementLock == true)
        {
            speed = 0;
        }

        return movementLock;
    }
}