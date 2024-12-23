using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour {
    private SpriteRenderer _sprite;

    private void Start() {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            _sprite.sortingOrder = 10;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            _sprite.sortingOrder = 2;
        }
    }
}
