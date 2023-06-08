using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] animationSprites;
    public Sprite idleSprite;
    public float animationTime = 0.25f;
    private int animationFrame = 0;
    public bool loop = true;
    public bool idle = true;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        spriteRenderer.enabled = true;
    }
    
    private void OnDisable() {
        spriteRenderer.enabled = false;
    }

    private void Start() {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame() {
        animationFrame++;

        if (loop && animationFrame >= animationSprites.Length) {
            animationFrame = 0;
        }

        if (idle) {
            spriteRenderer.sprite = idleSprite;
        } else if (animationFrame >= 0 && animationFrame < animationSprites.Length){ // garantee im not going to index out of bounds in our frames
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
