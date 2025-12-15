using UnityEngine;

public class CrossroadDecision : MonoBehaviour
{
    private GameObject targetTile;
    private BoardPlayer player;

    void OnMouseDown()
    {
        player.DetermineRoad(targetTile);
    }

    public void SetUpTheOption(GameObject tile, BoardPlayer chosenPlayer)
    {
        targetTile = tile;
        player = chosenPlayer;
    }
}
