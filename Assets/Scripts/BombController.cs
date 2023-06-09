using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
  [Header("Bomb")]
  public GameObject bombPreFab;
  public KeyCode inputKey = KeyCode.Space;
  public float bombFuseTime = 3f;
  public int bombAmount = 1;
  private int bombsRemaining = 0;

  [Header("Explosion")]
  public Explosion explosionPrefab;
  public LayerMask explosionLayerMask;
  public float explosionDuration = 1f;
  public int explosionRadius = 1;

  [Header("Breakable")]
  public Tilemap breakableTiles;
  public BreakableTile breakableTilePrefab;


  private void OnEnable()
  {
    bombsRemaining = bombAmount;
  }

  private void Update()
  {
    if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
    {
      StartCoroutine(PlaceBomb());
    }
  }

  private IEnumerator PlaceBomb()
  {
    Vector2 position = transform.position;
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);

    GameObject bomb = Instantiate(bombPreFab, position, Quaternion.identity);
    bombsRemaining--;

    yield return new WaitForSeconds(bombFuseTime);

    position = bomb.transform.position; // if we push the bomb we need the get the new position of the bomb
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);

    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    explosion.SetActiveRenderer(explosion.start);
    explosion.DestroyAfter(explosionDuration);

    Explode(position, Vector2.up, explosionRadius);
    Explode(position, Vector2.down, explosionRadius);
    Explode(position, Vector2.left, explosionRadius);
    Explode(position, Vector2.right, explosionRadius);

    Destroy(bomb);
    bombsRemaining++;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
    {
      other.isTrigger = false;
    }
  }

  private void Explode(Vector2 position, Vector2 direction, int lenght)
  {
    if (lenght <= 0)
    {
      return;
    }

    position += direction;

    if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
    {
      ClearBreakableTiles(position);
      return;
    }

    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    explosion.SetActiveRenderer(lenght > 1 ? explosion.middle : explosion.end);
    explosion.SetDirection(direction);
    explosion.DestroyAfter(explosionDuration);

    Explode(position, direction, lenght - 1);
  }

  private void ClearBreakableTiles(Vector2 position)
  {
    Vector3Int cell = breakableTiles.WorldToCell(position);
    TileBase tile = breakableTiles.GetTile(cell);

    if (tile != null)
    {
        Instantiate(breakableTilePrefab, position, Quaternion.identity);
        breakableTiles.SetTile(cell, null);
    }
  }

  public void AddBomb()
  {
    bombAmount++;
    bombsRemaining++;
  }
}
