using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RearWeaponSpawner : MonoBehaviour
{
    [SerializeField] private List<SpinningWeapon> weaponProjectiles = new List<SpinningWeapon>();

    [SerializeField] private CollapsePlayer collapserPlayer;

    private float weaponThrowInterval = 2f;
    private float timeDeltaPassed = 0f;

    void Update()
    {
        if (!collapserPlayer.HasGameFinished) ThrowWeapon();
    }

    private void ThrowWeapon()
    {
        timeDeltaPassed += Time.deltaTime;

        if (timeDeltaPassed >= weaponThrowInterval)
        {
            SpinningWeapon thrownProjectile = GetRandomWeapon();
            Instantiate(thrownProjectile, GetRandomSpawnLocation(), thrownProjectile.transform.rotation);
            if (weaponThrowInterval > 1.5) weaponThrowInterval -= 0.1f;
            timeDeltaPassed = 0f;
        }
    }

    private SpinningWeapon GetRandomWeapon()
    {
        int randomWeaponIndex = Random.Range(0, weaponProjectiles.Count);
        return weaponProjectiles[randomWeaponIndex];
    }

    private Vector3 GetRandomSpawnLocation()
    {
        float randomX = Random.Range(-13, 13);
        float randomY = Random.Range(-5, 5);
        return new Vector3(randomX, randomY, transform.position.z);
    }

}
