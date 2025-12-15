using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BarrelRollPlayer : MonoBehaviour
{
    [SerializeField] private RPGCharacter playerInfo;

    // Minigame related
    [SerializeField] private GameObject fireball;

    private float shootCooldownTimer = 0;
    private float shootInterval = 0.3f;
    private float originalShootInterval;


    private string activeBuff = "";
    private float activeBuffTimer = 0f;

    [SerializeField]
    private BarrelSpawner barrelSpawner;

    private int timeRemaining = 60;
    private float gameDeltaTimeCounter = 0f;


    // Movement
    [SerializeField] private float movementSpeed;
    private float originalMovementSpeed;

    private bool isMoving;

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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInfo = GetComponent<RPGCharacter>();

        // Health
        maxHealth = playerInfo.MaxHealth;
        currentHealth = maxHealth;
        actionCanvasUI.UpdateHealthRemaining(maxHealth);

        // MovementSpeed
        movementSpeed = playerInfo.MovementSpeed;
        originalMovementSpeed = movementSpeed;

        // Shoot
        originalShootInterval = shootInterval;
    }

    void Update()
    {
        if (!hasGameFinished)
        {

            //Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

            controller.Move(move * movementSpeed * Time.deltaTime);

            controller.Move(velocity * Time.deltaTime);

            shootCooldownTimer += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && shootCooldownTimer >= shootInterval)
            {
                ShootFireball();
                shootCooldownTimer = 0;
            }

            CheckForBuffs();
            LimitMovementToBoarders();

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
        }
    }

    private void GameWin()
    {
        actionCanvasUI.showVictoryPanel();
        playerInfo.AddGold(moneyGathered);
        barrelSpawner.DisableBarrelSpawner();
        hasGameFinished = true;
    }

    private void GameLose()
    {
        playerInfo.AddGold(moneyGathered);
        actionCanvasUI.UpdateHealthRemaining(0);
        actionCanvasUI.showGameOverScreen();
        barrelSpawner.DisableBarrelSpawner();

        int boardLifesFromData = playerInfo.SharedPlayerData.lifesOnBoard;
        if (boardLifesFromData - 1 >= 0) playerInfo.SharedPlayerData.lifesOnBoard -= 1;

        hasGameFinished = true;
    }

    private void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CoinPickUp"))
        {
            moneyGathered += 1;
            actionCanvasUI.UpdateCoinCounter(moneyGathered);
            // Debug.Log("Z³apano monetê!");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HurtObject"))
        {
            if (moneyGathered - 2 >= 0)
            {
                moneyGathered -= 2;
                actionCanvasUI.UpdateCoinCounter(moneyGathered);
            }

            currentHealth = currentHealth - ((maxHealth * 20) / 100);

            actionCanvasUI.UpdateHealthRemaining(currentHealth);
            if (currentHealth <= 0)
            {
                GameLose();
            }
        }

        if (other.gameObject.CompareTag("BarrelRollPickUpSpeed"))
        {
            AddBuff("BarrelRollPickUpSpeed");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BarrelRollPickUpTimeSlow"))
        {
            AddBuff("BarrelRollPickUpTimeSlow");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BarrelRollPickUpTripleShots"))
        {
            AddBuff("BarrelRollPickUpTripleShots");
            Destroy(other.gameObject);
        }
    }

    public void ShootFireball()
    {
        Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2); 
        Instantiate(fireball, startingPosition, transform.rotation);
    }

    private void AddBuff(string buff)
    {
        activeBuffTimer = 5f;
        activeBuff = buff;

    }
    private void CheckForBuffs()
    {
        if (activeBuff == "BarrelRollPickUpSpeed")
        {
            movementSpeed = originalMovementSpeed * 1.5f;
            barrelSpawner.CancelSlowDownBarrels();
            shootInterval = originalShootInterval;
            //Debug.Log("Zwiekszam ms do: " + movementSpeed);
        }
        else if (activeBuff == "BarrelRollPickUpTimeSlow")
        {
            barrelSpawner.SlowDownBarrels();
            movementSpeed = originalMovementSpeed;
            shootInterval = originalShootInterval;
        }
        else if (activeBuff == "BarrelRollPickUpTripleShots")
        {
            shootInterval = shootInterval / 2;
            barrelSpawner.CancelSlowDownBarrels();
            movementSpeed = originalMovementSpeed;
        }
        else
        {
            barrelSpawner.CancelSlowDownBarrels();
            movementSpeed = originalMovementSpeed;
            shootInterval = originalShootInterval;
            //Debug.Log("Przywracam ms do: " + movementSpeed);
        }
    }
    private void LimitMovementToBoarders()
    {

        Vector3 pos = transform.position;
        if (pos.x < -30f)
        {
            pos.x = -30f;
            transform.position = pos;
        }
        else if (pos.x > 30f)
        {
            pos.x = 30f;
            transform.position = pos;
        }
        if (pos.z < -15f)
        {
            pos.z = -15f;
            transform.position = pos;
        }
        else if (pos.z > 17f)
        {
            pos.z = 17f;
            transform.position = pos;
        }

    }
}
