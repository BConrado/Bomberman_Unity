using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BombExplosionRadiusBonus,
        SpeedBonus,
    }

    public ItemType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickUp(other.gameObject);
        }
    }

    private void OnItemPickUp(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                 player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BombExplosionRadiusBonus:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedBonus:
                player.GetComponent<MovementController>().speed++;
                break;
        }
        Destroy(gameObject);
    }
}
