using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [HideInInspector] public bool canMove = false;

    private GameController _gameManager;
    private Rigidbody2D _rigidbody2D;
    private PlayerControls _controls;
    private Vector2 _moveDirection = Vector2.zero;
    private readonly List<ItemEntity> _itemsInRange = new List<ItemEntity>();

    private bool _canPickupItem = true;
    private bool _isInLevelTransition = false;
    private Vector2 _transitionDirection = Vector2.zero;

    private void Awake() {
        _controls = new PlayerControls();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, -13, 0);
    }
    
    private void OnEnable() {
        _controls.Enable();
    }

    private void OnDisable() {
        _controls.Disable();
    }

    private void Update() {
        if (canMove) {
            _moveDirection = _controls.Player.Move.ReadValue<Vector2>();
            animator.SetFloat("Horizontal", _moveDirection.x);
            animator.SetFloat("Vertical", _moveDirection.y);
            animator.SetFloat("Speed", _moveDirection.sqrMagnitude);    
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
        if (_isInLevelTransition) {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", _transitionDirection.y);
            animator.SetFloat("Speed", _transitionDirection.sqrMagnitude);  

            _rigidbody2D.velocity = _transitionDirection * moveSpeed;
            
            if (transform.position.y is > -7 and < 0) {
                _isInLevelTransition = false;
                canMove = true;
            }
        }

        if (canMove) {
            _rigidbody2D.velocity = _moveDirection * moveSpeed;
        }
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

    public void StartLevelTransition(Vector2 direction) {
        _transitionDirection = direction;
        canMove = false;
        _isInLevelTransition = true;
    }

    public void EnableMovement() {
        canMove = true;
    }
    
    public void DisableMovement() {
        canMove = false;
        _moveDirection = Vector2.zero;
        _rigidbody2D.velocity = Vector2.zero;
        animator.SetFloat("Horizontal", _moveDirection.x);
        animator.SetFloat("Vertical", _moveDirection.y);
        animator.SetFloat("Speed", _moveDirection.sqrMagnitude);
    }
}
