using UnityEngine;
using System.Collections;
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

    [Header("Npc Tracker")]
    public DialogueSystem npc1;
    public DialogueSystem npc2;
    public DialogueSystem npc3;

    [Header("Quest Tracker")]
    public bool hasItem;
    public bool solvedRiddle;

    [Header("Walking")]
    public AudioClip[] footsteps;
    public float walkSoundDelay = .3f;

    private Coroutine walkingSoundCoroutine;
    private int lastFootstep = -1;
    private Rigidbody2D rb;
    private float horizontalInput;
    private Animator anim;

    private IEnumerator PlayWalkingSound() 
    {
        while (true) 
        {
            PlayRandomFootsteps();
            yield return new WaitForSeconds(walkSoundDelay);
        }
    }

    private void PlayRandomFootsteps() 
    {
        if (footsteps == null || footsteps.Length == 0) 
        {
            return;
        }
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, footsteps.Length);
        } while (footsteps.Length > 1 && randomIndex == lastFootstep);

        lastFootstep = randomIndex;

        SoundEffects.instance.PlaySFX(footsteps[randomIndex], 2);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementLock = false;
        hasItem= false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        torchInteraction();

        bool dialogueIsActive =
            npc1.dialogueActive ||
            npc2.dialogueActive ||
            npc3.dialogueActive;

        if (dialogueIsActive)
        {
            LockMovement();
        }
        else
        {
            movementLock = false;
            playerMovement();
        }
    }

    public void torchInteraction()
    {
        if (torch != null && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Torch Touched");

            torch.ActivateTorch();
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

        if (horizontalInput < 0)
        {
            anim.SetBool("WalkingLeft", true);
            anim.SetBool("WalkingRight", false);
            anim.SetBool("NotWalking", false);

            StartWalkingSound();

        }
        else if (horizontalInput > 0)
        {
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", true);
            anim.SetBool("NotWalking", false);

            StartWalkingSound();
            
        }
        else 
        {
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", false);
            anim.SetBool("NotWalking", true);

            StopWalkingSound();
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

        while (movementLock) 
        {
            movementLock = true;

            // Stop horizontal movement immediately
            horizontalInput = 0;

            // Stop the Rigidbody from continuing to slide
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            // Stop walking animation
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", false);
            anim.SetBool("NotWalking", true);

            // Stop footsteps
            StopWalkingSound();
        }
    }

    private void FixedUpdate()
    {
        if (movementLock)
        {
            rb.linearVelocity = new Vector2(
                0,
                rb.linearVelocity.y
            );

            return;
        }

        rb.linearVelocity = new Vector2(
            horizontalInput * speed,
            rb.linearVelocity.y
        );
    }
    private void StartWalkingSound() 
    {
        if (walkingSoundCoroutine == null) 
        {
            walkingSoundCoroutine = StartCoroutine(PlayWalkingSound());
        }
    }

    private void StopWalkingSound() 
    {
        if (walkingSoundCoroutine != null) 
        {
            StopCoroutine(walkingSoundCoroutine);
            walkingSoundCoroutine = null;
        }
    }

    public void LockMovement()
    {
        movementLock = true;
        horizontalInput = 0;

        rb.linearVelocity = new Vector2(
            0,
            rb.linearVelocity.y
        );

        anim.SetBool("WalkingLeft", false);
        anim.SetBool("WalkingRight", false);
        anim.SetBool("NotWalking", true);

        StopWalkingSound();

    }
}