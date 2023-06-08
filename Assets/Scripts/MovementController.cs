using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
  public new Rigidbody2D rigidbody { get; private set; }
  private Vector2 direction = Vector2.down;
  public float speed = 5f;
  public KeyCode inputUp = KeyCode.W;
  public KeyCode inputDown = KeyCode.S;
  public KeyCode inputLeft = KeyCode.A;
  public KeyCode inputRight = KeyCode.D;
  public AnimatedSpriteRenderer spriteRendererUp;
  public AnimatedSpriteRenderer spriteRendererDown;
  public AnimatedSpriteRenderer spriteRendererLeft;
  public AnimatedSpriteRenderer spriteRendererRight;
  private AnimatedSpriteRenderer activeSpriteRenderer;

  public void Awake()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    activeSpriteRenderer = spriteRendererDown;
  }

  private void Update()
  {
    if (Input.GetKey(inputUp))
    {
      SetDirection(Vector2.up, spriteRendererUp);
    }
    else if (Input.GetKey(inputDown))
    {
      SetDirection(Vector2.down, spriteRendererDown);
    }
    else if (Input.GetKey(inputRight))
    {
      SetDirection(Vector2.right, spriteRendererRight);
    }
    else if (Input.GetKey(inputLeft))
    {
      SetDirection(Vector2.left, spriteRendererLeft);
    }
    else
    {
      SetDirection(Vector2.zero, activeSpriteRenderer);
    }
  }

  private void FixedUpdate()
  {
    Vector2 position = rigidbody.position;
    Vector2 translation = direction * speed * Time.fixedDeltaTime;

    rigidbody.MovePosition(position + translation);
  }

  private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
  {
    direction = newDirection;

    spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
    spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
    spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
    spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

    activeSpriteRenderer = spriteRenderer;
    activeSpriteRenderer.idle = direction == Vector2.zero;
  }
}
