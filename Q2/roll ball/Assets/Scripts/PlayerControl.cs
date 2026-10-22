using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;

    public float speed = 0f;
    public float jumpForce = 0f;

    private bool isGrounded = false;
    private bool jumpQueued = false;

    private int count = 0;
    private int totalPickups = 0;

    // (c) Start button
    public GameObject startButton;
    private bool gameStarted = false;

    // (e) Power-up
    public GameObject powerUp;
    public GameObject powerUpText;
    public Color powerUpColor = Color.yellow;
    public float powerUpSpeedMultiplier = 2f;
    private bool isPoweredUp = false;

    // (g) Win
    public GameObject winText;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();
        totalPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;
        rb.useGravity = false;
    }

    void Update()
    {
        if (!gameStarted) return;
        if (isGrounded && jumpAction.WasPressedThisFrame())
        {
            jumpQueued = true;
        }
    }

    private void FixedUpdate()
    {
        if (!gameStarted) return;
        if (jumpQueued)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpQueued = false;
        }

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveValue.x, 0f, moveValue.y);

        rb.AddForce(movement * speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        // (f) Breakable wall: only breaks when powered up
        if (collision.gameObject.CompareTag("BreakableWall") && isPoweredUp)
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // (b) Collectibles
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            Debug.Log("Pickups collected: " + count + "/" + totalPickups);

            // (e) Power-up appears after all cubes are collected
            if (count >= totalPickups)
            {
                powerUp.SetActive(true);
            }
        }

        // (e) Touching the power-up: new color, more speed, on-screen text
        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            isPoweredUp = true;
            speed *= powerUpSpeedMultiplier;
            GetComponent<Renderer>().material.color = powerUpColor;
            powerUpText.SetActive(true);
        }

        // (g) Goal zone: hide the player and show "You win!"
        if (other.gameObject.CompareTag("Goal"))
        {
            powerUpText.SetActive(false);
            winText.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        rb.useGravity = true;
        startButton.SetActive(false);
    }
}