using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : MonoBehaviour
{
    [SerializeField] private int lifesOnBoard;
    private int gold;

    [SerializeField] private GameObject currentTile;
    [SerializeField] private Tile currentTileInfo;
    private GameObject nextTile;
    [SerializeField] private GameObject possibleRoadIndicator;

    private bool possibleRoadsSpawned = false;
    private List<GameObject> arrowsToClear = new List<GameObject>();


    private int rolledAmount = 0;
    private bool stillHasMovesLeft = false;

    private float timeDeltaCounter = 0f;
    private float movementOnBoardInterval = 0.5f;

    private bool isMoving = false;
    private bool isChoosingCrossroad = false;

    private bool wasTileEventResolved = false;

    private bool didGameEndForPlayer = false;

    // Playerinfo
    private RPGCharacter rpgCharacter;
    private PlayerData playerData;

    // Getters
    public bool StillHasMovesLeft => stillHasMovesLeft;
    public bool IsChoosingCrossroad => isChoosingCrossroad;
    public bool WasTileEventResolved => wasTileEventResolved;
    public bool DidGameEndForPlayer => didGameEndForPlayer;
    public GameObject CurrentTile => currentTile;

    // Ui
    [SerializeField] private BoardUi boardUI;

    void Start()
    {
        rpgCharacter = GetComponent<RPGCharacter>();
        playerData = rpgCharacter.SharedPlayerData;

        currentTileInfo = currentTile.GetComponent<Tile>();
        string idToMoveTo = playerData.currentTileId;
        Debug.Log(idToMoveTo);
        if(!string.IsNullOrEmpty(idToMoveTo))
        {
            GameObject resumedTile = GameObject.Find(idToMoveTo);
            currentTile = resumedTile;
            Vector3 newPosition = new Vector3(currentTile.transform.position.x, 4.5f, currentTile.transform.position.z + 2f);
            transform.position = currentTile.transform.position;
        }

        SetLifesLeft();

    }

    void Update()
    {
        if (!didGameEndForPlayer)
        {
            MoveOnBoard();

            if (lifesOnBoard <= 0)
            {
                GameLose();
            }
            gold = playerData.chosenCharacterInfo.Gold;
            boardUI.UpdateCoinCounter(gold);

        }
    }

    private void MoveOnBoard()
    {
        timeDeltaCounter += Time.deltaTime;
        if (movementOnBoardInterval <= timeDeltaCounter && !isMoving && rolledAmount > 0)
        {
            currentTileInfo = currentTile.GetComponent<Tile>();

            if (currentTileInfo.NextTiles.Count > 1)
            {
                isChoosingCrossroad = true;
                ShowOptionsAhead();
            }
            else if(currentTileInfo.NextTiles.Count == 1)
            {
                nextTile = currentTileInfo.NextTiles[0];
            }

            if (!isChoosingCrossroad)
            {
                possibleRoadsSpawned = false;
                isMoving = true;
                rolledAmount--;

                timeDeltaCounter = 0f;
            }
        }

        if (isMoving && !isChoosingCrossroad)
        {
            Vector3 nextTilePosition = new Vector3(nextTile.transform.position.x , nextTile.transform.position.y + 4.5f, nextTile.transform.position.z + 2f);
            transform.position = Vector3.Lerp(transform.position, nextTilePosition, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, nextTilePosition) < 0.1f)
            {
                transform.position = nextTilePosition;
                currentTile = nextTile;
                currentTileInfo = currentTile.GetComponent<Tile>();
                isMoving = false;
            }
        }

        if (rolledAmount <= 0)
        {
            stillHasMovesLeft = false;
        }
        else
        {
            stillHasMovesLeft = true;
        }
    }

    private void ShowOptionsAhead()
    {
        Vector3 middlePosition;
        List<GameObject> possibleChoices = currentTileInfo.NextTiles;


        if (!possibleRoadsSpawned)
        {
            foreach (GameObject tile in possibleChoices)
            {

                middlePosition = (gameObject.transform.position + tile.transform.position) / 2f;
                middlePosition.y = 10f;

                Vector3 direction = tile.transform.position - middlePosition;
                direction.y = 0f;
                // --- ten fragment byl wygenerowany chatem gpt
                Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f, 0f);
                // ----
                GameObject arrowInstance= Instantiate(possibleRoadIndicator, middlePosition, targetRotation);

                CrossroadDecision roadIndicator = arrowInstance.GetComponent<CrossroadDecision>();
                roadIndicator.SetUpTheOption(tile, gameObject.GetComponent<BoardPlayer>());
                arrowsToClear.Add(arrowInstance);

            }
        }
        possibleRoadsSpawned = true;
    }

    private void SetLifesLeft()
    {
        Debug.Log("Obecne id: " + currentTileInfo.TileId);
        string tileDataId = playerData.currentTileId;
        if (tileDataId == "tile1" || string.IsNullOrEmpty(tileDataId))
        {
            lifesOnBoard = 5;
            playerData.lifesOnBoard = lifesOnBoard;
        }
        else
        {
            lifesOnBoard = playerData.lifesOnBoard;
        }
        boardUI.UpdateHealthRemaining(lifesOnBoard);
    }

    private void GameLose()
    {
        Debug.Log("Przegrales, koniec gry!");
        boardUI.ShowGameOverScreen();
        didGameEndForPlayer = true;
        ResetPlayerData();
    }

    public void ResetPlayerData()
    {
        playerData.ResetPlayerData();
    }

    public void SetRolledAmount(int amount)
    {
        rolledAmount = amount;
        wasTileEventResolved = true;
        playerData.wasTileEventResolved = wasTileEventResolved;
    }

    public void DetermineRoad(GameObject tile)
    {
        nextTile = tile;
        isChoosingCrossroad = false;

        foreach (GameObject arrow in arrowsToClear)
        {
            Destroy(arrow);
        }

        arrowsToClear.Clear();

        if (rolledAmount > 0)
        {
            isMoving = true;
            rolledAmount--;
            timeDeltaCounter = 0f;
        }
    }

    public void ResetCurrentTile()
    {
        playerData.currentTileId = null;
    }

    public void LockPlayerInTileEvent()
    {
        wasTileEventResolved = false;
        playerData.wasTileEventResolved = wasTileEventResolved;
    }

    public void SendCurrentTileId()
    {
        playerData.currentTileId = currentTileInfo.TileId;
    }
}
