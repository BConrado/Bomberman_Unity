using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    public float destructionTime = 1f;
    [Range(0f, 1f)]
    public float itemSpwnChance = 0.2f; // chance to spw when breaking a tile
    public GameObject[] spwnableItems;


    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spwnableItems.Length > 0 && Random.value < itemSpwnChance)
        {
            int randomIndex = Random.Range(0, spwnableItems.Length);
            Instantiate(spwnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }


}
