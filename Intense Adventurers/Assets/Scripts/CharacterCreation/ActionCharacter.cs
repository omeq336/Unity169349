using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionCharacter : MonoBehaviour
{
    [SerializeField] private RPGCharacter playerInfo;

    // Movement
    [SerializeField] private float movementSpeed;
    public float lookSpeed = 2f;

    private float jumpStrength;
    private bool isJumping;
    private float fallMul;

    private bool isMoving;

    //MISC
    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -18f;
    private bool isGrounded;
    private Vector2 moveInput;
    private Vector2 lookInput;

    // Money
    private int moneyGathered = 0;
    private int maxHealth;
    private int currentHealth;

    // Ui integrations
    [SerializeField] private ActionCanvas actionCanvasUI;

    // GameHalt
    private bool hasGameFinished = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInfo = GetComponent<RPGCharacter>();

        // Health
        maxHealth = playerInfo.MaxHealth;
        currentHealth = maxHealth;
        actionCanvasUI.UpdateHealthRemaining(maxHealth);
    }

    void Update()
    {
        if(!hasGameFinished) PlayerMovement();
    }

    private void PlayerMovement()
    {
        movementSpeed = playerInfo.MovementSpeed;
        jumpStrength = playerInfo.JumpStrength;

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * movementSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up * lookInput.x * lookSpeed);

        velocity.y += gravity * Time.deltaTime;

        if (velocity.y < 0)
        {
            velocity.y += gravity * Time.deltaTime * jumpStrength / 2;
        }

        controller.Move(velocity * Time.deltaTime);

    }

    private void GameWin()
    {
        moneyGathered += 50;
        actionCanvasUI.UpdateCoinCounter(moneyGathered);
        playerInfo.AddGold(moneyGathered);
        actionCanvasUI.ShowWinPanel();
        hasGameFinished = true;
    }

    private void GameLose()
    {
        playerInfo.AddGold(moneyGathered);
        actionCanvasUI.UpdateHealthRemaining(0);
        actionCanvasUI.ShowGameOverPanel();

        int boardLifesFromData = playerInfo.SharedPlayerData.lifesOnBoard;
        if (boardLifesFromData - 1 >= 0) playerInfo.SharedPlayerData.lifesOnBoard -= 1;

        hasGameFinished = true;
    }

    private void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
        isMoving = true;
    }

    private void OnJump(InputValue movementValue)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
            isJumping = true;
        }
    }

    private void OnLook(InputValue lookValue)
    {
        lookInput = lookValue.Get<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CoinPickUp"))
        {
            other.gameObject.SetActive(false);
            moneyGathered += 1;
            actionCanvasUI.UpdateCoinCounter(moneyGathered);
            Debug.Log("Z³apano monetê!");
        }
        else if (other.gameObject.CompareTag("WinningPickUp"))
        {
            GameWin();
            // Debug.Log("Wygra³es!");
        }
        else if (other.gameObject.CompareTag("HurtObject"))
        {
            if (moneyGathered - 5 >= 0)
            {
                moneyGathered -= 5;
                actionCanvasUI.UpdateCoinCounter(moneyGathered);
            }

            currentHealth = currentHealth - ((maxHealth * 50) / 100);

            actionCanvasUI.UpdateHealthRemaining(currentHealth);
            if (currentHealth <= 0)
            {
                GameLose();
            }
        }
    }
}
