using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerList = new List<GameObject>();
    [SerializeField] private GameObject firstTile;
    [SerializeField] private GameObject lastTile;
    [SerializeField] private GameObject hierarchyTiles;
    private List<Tile> listOfTiles = new List<Tile>();

    private bool hasGameFinished = false;

    // Ui
    [SerializeField] private BoardUi boardUI;

    void Start()
    {
        foreach (GameObject player in playerList)
        {
            Vector3 firstTilePosition = new Vector3(firstTile.transform.position.x, 4.5f, firstTile.transform.position.z);
            player.transform.position = firstTilePosition;
            //BoardPlayer boardPlayer = player.GetComponent<BoardPlayer>();
            //boardPlayer.SetStartingTile(firstTile);
        }

        //---- Ten fragment by³ wygenerowany chatem gpt
        for (int i = 0; i < hierarchyTiles.transform.childCount; i++)
        {
            GameObject hierarchyTile = hierarchyTiles.transform.GetChild(i).gameObject;
            Debug.Log(hierarchyTile.name);
        //-----

            Tile childTileComponent = hierarchyTile.GetComponent<Tile>();
            childTileComponent.InitializeTileId("tile" + (i+1));

            listOfTiles.Add(childTileComponent);
        }
    }

    void Update()
    {
        if (!hasGameFinished)
        {
            foreach (GameObject player in playerList)
            {
                BoardPlayer currentPlayer = player.GetComponent<BoardPlayer>();
                if (currentPlayer.CurrentTile == lastTile)
                {
                    boardUI.ShowVictoryPanel();
                    currentPlayer.ResetPlayerData();
                    hasGameFinished = true;
                }
            }
        }
    }
}
