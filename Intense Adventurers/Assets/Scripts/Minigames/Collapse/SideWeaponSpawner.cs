using System.Collections.Generic;
using UnityEngine;

public class SideWeaponSpawner : MonoBehaviour
{
    [SerializeField] private List<SideSpinningItem> weaponProjectiles = new List<SideSpinningItem>();

    [SerializeField] private bool shootRight;

    private float weaponThrowInterval = 4f;
    private float timeDeltaPassed = 0f;

    [SerializeField] private CollapsePlayer collapsePlayer;

    void Update()
    {
        if (!collapsePlayer.HasGameFinished) ThrowWeapon();
    }

    private void ThrowWeapon()
    {
        timeDeltaPassed += Time.deltaTime;

        if (timeDeltaPassed >= weaponThrowInterval)
        {

            SideSpinningItem thrownProjectile = GetRandomWeapon();

            SideSpinningItem thrownProjectileInstance = Instantiate(thrownProjectile, GetRandomSpawnLocation(), thrownProjectile.transform.rotation);

            thrownProjectileInstance.SetShootDirection(shootRight);


            if (weaponThrowInterval > 2) weaponThrowInterval -= 0.1f;
            timeDeltaPassed = 0f;
        }
    }

    private SideSpinningItem GetRandomWeapon()
    {
        int randomWeaponIndex = Random.Range(0, weaponProjectiles.Count);
        return weaponProjectiles[randomWeaponIndex];
    }

    private Vector3 GetRandomSpawnLocation()
    {
        if (shootRight)
        {
            float randomY = Random.Range(0, 5);
            float randomX = Random.Range(-25, -16);
            return new Vector3(randomX, randomY, transform.position.z);
        }
        else
        {
            float randomY = Random.Range(0, 5);
            float randomX = Random.Range(15, 26);
            return new Vector3(randomX, randomY, transform.position.z);
        }

    }

}
