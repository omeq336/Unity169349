using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Quaternion test;
    [SerializeField] private List<Quaternion> rotationList = new List<Quaternion>();
    private int currentRotationIndex = 0;
    private bool isDiceLocked = false;

    private float timeDeltaCounter = 0f;
    private float rotationTime = 0.1f;

    [SerializeField] private BoardPlayer currentPlayer;

    void Start()
    {
        rotationList.Add(Quaternion.Euler(90, 0, 0));
        rotationList.Add(Quaternion.Euler(0, 0, 0));
        rotationList.Add(Quaternion.Euler(0, 0, 90));
        rotationList.Add(Quaternion.Euler(0, 0, 270));
        rotationList.Add(Quaternion.Euler(0, 0, 180));
        rotationList.Add(Quaternion.Euler(270, 0, 0));

    }

    void Update()
    {
        if (!currentPlayer.StillHasMovesLeft)
        {
            timeDeltaCounter += Time.deltaTime;

            if (timeDeltaCounter >= rotationTime)
            {
                transform.rotation = rotationList[currentRotationIndex];
                test = rotationList[currentRotationIndex];
                currentRotationIndex++;
                if (currentRotationIndex > 5) currentRotationIndex = 0;

                timeDeltaCounter = 0f;
            }

        }

    }

    private int RollDice()
    {
        int rolledValue = Random.Range(1, 7);
        transform.rotation = rotationList[rolledValue - 1];

        return rolledValue;
    }

    public void OnDiceRolled()
    {
        if (!currentPlayer.StillHasMovesLeft)
        {
            int rolledValue = RollDice();
            // Debug.Log("Klikniêto kostkê! Wyrzucono: " + rolledValue);

            currentPlayer.SetRolledAmount(rolledValue);
        }
    }
}
