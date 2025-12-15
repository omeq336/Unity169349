using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private enum TileType
    {
        Regular,
        Minigame,
        TinyAdventure,
        Treasure,
        FinishLine,
    }

    [SerializeField] private TileType tileType;
    private string tileId;
    [SerializeField] private List<GameObject> nextTiles = new List<GameObject>();
    private bool isChoosing = true;

    // Ui integrations
    [SerializeField] private BoardUi boardCanvas;

    // Getters
    public List<GameObject> NextTiles => nextTiles;
    public string TileId => tileId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject interactingPlayerObject = other.gameObject;
            BoardPlayer interactingPlayerInfo = interactingPlayerObject.GetComponent<BoardPlayer>();

            if (!interactingPlayerInfo.StillHasMovesLeft && interactingPlayerInfo.WasTileEventResolved)
            {
                DetermineEvent();
                interactingPlayerInfo.LockPlayerInTileEvent();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(boardCanvas != null) boardCanvas.CloseMinigamePanel();
    }

    private void DetermineEvent()
    {
        switch (tileType)
        {
            case TileType.Minigame:
                boardCanvas.ShowMinigamePanel();
                break;

            case TileType.TinyAdventure:

                break;

            case TileType.Treasure:

                break;

            case TileType.FinishLine:
                boardCanvas.ShowVictoryPanel();
                break;
        }
    }

    public void InitializeTileId(string givenId)
    {
        tileId = givenId;
        gameObject.name = givenId;
    }

}
