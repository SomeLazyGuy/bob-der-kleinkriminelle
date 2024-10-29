using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;

    private GameController _gameManager;
    private Rigidbody2D _rigidbody2D;
    private PlayerControls _controls;
    private Vector2 _moveDirection = Vector2.zero;
    private List<ItemEntity> _itemsInRange = new List<ItemEntity>();

    private bool _canPickupItem = true;

    private void Awake() {
        _controls = new PlayerControls();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable() {
        _controls.Enable();
    }

    private void OnDisable() {
        _controls.Disable();
    }

    private void Update() {
        // Zur Behebung der NullReferenceException
        if(_controls.Player.Move.ReadValue<Vector2>() != null){
            _moveDirection = _controls.Player.Move.ReadValue<Vector2>();
        }
        
        
        if ((int)_controls.Player.Interact.ReadValue<float>() == 1) {
            if (!_canPickupItem) return;
            if (_itemsInRange.Count == 0) return;
            
            if (_gameManager.PickupItem(_itemsInRange[0])) _canPickupItem = false;
        } else {
            _canPickupItem = true;
        }
    }

    private void FixedUpdate() {
        _rigidbody2D.velocity = _moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items")) {
            _itemsInRange.Add(other.gameObject.GetComponent<ItemEntity>());
            
            if (_itemsInRange.Count == 1) _itemsInRange[0].SelectItem();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items")) {
            ItemEntity item = other.gameObject.GetComponent<ItemEntity>();
            item.DeselectItem();
            _itemsInRange.Remove(item);
            
            if (_itemsInRange.Count > 0) _itemsInRange[0].SelectItem();
        }
    }
}
