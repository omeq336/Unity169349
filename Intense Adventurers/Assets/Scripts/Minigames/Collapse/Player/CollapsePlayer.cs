using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class CollapsePlayer : MonoBehaviour
{
    [SerializeField] private RPGCharacter playerInfo;

    // Minigame related
    private string activeBuff = "";
    private float activeBuffTimer = 0f;

    private int timeRemaining = 60;
    private float gameDeltaTimeCounter = 0f;


    // Movement
    [SerializeField] private float movementSpeed;
    private float originalMovementSpeed;

    private float gravity = -18f;
    private bool isGrounded;
    private bool isMoving;
    private float jumpStrength;

    //MISC
    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 moveInput;

    // Money
    private int moneyGathered = 0;
    private int maxHealth;
    private int currentHealth;

    // Ui integrations
    [SerializeField] private MinigameUI actionCanvasUI;

    // GameHalt
    private bool hasGameFinished = false;

    // Getters
    public bool HasGameFinished => hasGameFinished;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Health
        maxHealth = playerInfo.MaxHealth;
        currentHealth = maxHealth;
        actionCanvasUI.UpdateHealthRemaining(maxHealth);

        // MovementSpeed
        movementSpeed = playerInfo.MovementSpeed / 1.5f;
        originalMovementSpeed = movementSpeed;
        jumpStrength = playerInfo.JumpStrength / 2;
    }

    void Update()
    {
        if (!hasGameFinished)
        {
            //Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

            controller.Move(move * movementSpeed * Time.deltaTime);

            controller.Move(velocity * Time.deltaTime);


            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            controller.Move(move * movementSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            if (velocity.y < 0)
            {
                velocity.y += gravity * Time.deltaTime * jumpStrength / 2;
            }

            controller.Move(velocity * Time.deltaTime);


            // CheckForBuffs();
            LimitMovementToBoarders();
            CheckForWinLose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MinigameGoldBarPickUp"))
        {
            moneyGathered += 10;
            actionCanvasUI.UpdateCoinCounter(moneyGathered);
            // Debug.Log("Z³apano monetê!");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("MinigameHealingPotion"))
        {
            currentHealth = currentHealth + ((maxHealth * 20) / 100);
            if (currentHealth > maxHealth) currentHealth = maxHealth;

            actionCanvasUI.UpdateHealthRemaining(currentHealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                actionCanvasUI.UpdateHealthRemaining(0);
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HurtObject"))
        {
            currentHealth = currentHealth - ((maxHealth * 30) / 100);
            
            if(moneyGathered - 5 >= 0)
            {
                moneyGathered -= 5;
                actionCanvasUI.UpdateCoinCounter(moneyGathered);
            }

            actionCanvasUI.UpdateHealthRemaining(currentHealth);
            if (currentHealth <= 0)
            {
                GameLose();
            }
        }
    }

    private void GameWin()
    {
        actionCanvasUI.showVictoryPanel();
        playerInfo.AddGold(moneyGathered);
        hasGameFinished = true;
    }

    private void GameLose()
    {
        playerInfo.AddGold(moneyGathered);
        actionCanvasUI.UpdateHealthRemaining(0);
        actionCanvasUI.showGameOverScreen();

        int boardLifesFromData = playerInfo.SharedPlayerData.lifesOnBoard;
        if (boardLifesFromData - 1 >= 0) playerInfo.SharedPlayerData.lifesOnBoard -= 1;

        hasGameFinished = true;
    }
    
    /*
    private void CheckForBuffs()
    {

    }
    */

    private void LimitMovementToBoarders()
    {

        Vector3 pos = transform.position;
        if (pos.x < -11f)
        {
            pos.x = -11f;
            transform.position = pos;
        }
        else if (pos.x > 11f)
        {
            pos.x = 11f;
            transform.position = pos;
        }
        if (pos.z < 0f || pos.z > 0f)
        {
            pos.z = 0;
            transform.position = pos;
        }

    }

    private void CheckForWinLose()
    {
        gameDeltaTimeCounter += Time.deltaTime;
        if (gameDeltaTimeCounter >= 1f)
        {
            if (timeRemaining - 1 <= 0)
            {
                GameWin();
            }
            timeRemaining -= 1;
            if (activeBuffTimer > 0)
            {
                activeBuffTimer -= 1;
                Debug.Log("Buffa zosta³o: " + activeBuffTimer);
                if (activeBuffTimer <= 0)
                {
                    activeBuff = "";
                    activeBuffTimer = 0;
                }
            }
            gameDeltaTimeCounter = 0f;
            actionCanvasUI.UpdateMinigameTimeRemaining(timeRemaining);
        }
        if (transform.position.y <= -10)
        {
            GameLose();
        }
    }

    private void OnJump(InputValue movementValue)
    {
        if (isGrounded && !hasGameFinished)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        if (!hasGameFinished)
        {
            moveInput = movementValue.Get<Vector2>() * -1f;
            float defaultYRotation = 180;

            Quaternion newRotation = gameObject.transform.rotation;

            if (moveInput.x > 0)
            {
                newRotation = Quaternion.Euler(0, 220, 0);
            }
            else if (moveInput.x < 0)
            {
                newRotation = Quaternion.Euler(0, 140, 0);
            }

            gameObject.transform.rotation = newRotation;

            isMoving = true;
        }
    }
}
