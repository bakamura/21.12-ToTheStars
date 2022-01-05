using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; } = null;

    [System.NonSerialized] public Rigidbody2D rbPlayer;

    private bool _isTrackingTouch = false;
    private Vector3 _touchInitialPos;
    private Vector3 _touchCurrentPos;
    // % of height that defines a drag touch.
    [SerializeField] private float _dragScreenPercentage;
    private float _dragDistance;

    [SerializeField] private float timeToSwitchLane;
    private bool _isMoving;
    private float _targetHeight;

    [System.NonSerialized] public Coroutine _changeInSizeCoroutine = null;
    private Vector2 _playerSizeBeforeChange;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        rbPlayer = GetComponent<Rigidbody2D>();
        _dragDistance = Screen.height * _dragScreenPercentage;
    }

    private void Update() {
        DetectMovement();

    }

    private void DetectMovement() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                _touchInitialPos = touch.position;
                _isTrackingTouch = true;
            }
            else if (touch.phase == TouchPhase.Ended) {
                _isTrackingTouch = false;
                return;
            }

            if (_isTrackingTouch) {
                _touchCurrentPos = touch.position;

                if (Vector3.Distance(_touchInitialPos, _touchCurrentPos) > _dragDistance) {
                    Vector3 dragDirection = _touchCurrentPos - _touchInitialPos;
                    float dragAngle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;
                    if (dragAngle > 45 && dragAngle < 135) {
                        Debug.Log("Swiped Up"); //
                        if (!_isMoving && transform.position.y < 2) StartCoroutine(MoveTowardsHeight(true));
                        _isTrackingTouch = false;
                        return;
                    }
                    else if (dragAngle < -45 && dragAngle > -135) {
                        Debug.Log("Swiped Down"); //
                        if (!_isMoving && transform.position.y > -2) StartCoroutine(MoveTowardsHeight(false));
                        _isTrackingTouch = false;
                    }
                }
            }
        }
    }

    private IEnumerator MoveTowardsHeight(bool isUpwards) {
        _isMoving = true;

        // Direction should be called with the distance between each lane's pivot
        rbPlayer.velocity = isUpwards ? (2 * Vector2.up / timeToSwitchLane) : (2 * Vector2.down / timeToSwitchLane);

        //for (int i = 0; i < 60; i++) { // WaitForSeconds doesn't properly work when float is too small
        //    transform.position += new Vector3(0, direction / 60, 0);

        //    yield return new WaitForSeconds(timeToSwitchLane);
        //} 

        yield return new WaitForSeconds(timeToSwitchLane);

        rbPlayer.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, (int)transform.position.y, 0);
        _isMoving = false;
    }
    public void ChangeSize(bool increaseSize){
        if (_changeInSizeCoroutine == null){
            _changeInSizeCoroutine = StartCoroutine(this.ChangeSizeEffectPowderKeg(increaseSize));
        }
    }
    private IEnumerator ChangeSizeEffectPowderKeg(bool increaseSize)
    {
        Vector2 changeInScale = Vector2.zero;
        if(increaseSize){
            _playerSizeBeforeChange = new Vector2(transform.localScale.x, transform.localScale.y);
            changeInScale = _playerSizeBeforeChange;
            while (transform.localScale.x < PowerUp.maxSizePlayerIncreasePowderKeg && transform.localScale.y < PowerUp.maxSizePlayerIncreasePowderKeg)
            {
                changeInScale += new Vector2(.05f, .05f);
                transform.localScale = changeInScale;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            _changeInSizeCoroutine = null;
        }
        else{
            changeInScale = transform.localScale;
            while (transform.localScale.x > _playerSizeBeforeChange.x && transform.localScale.y > _playerSizeBeforeChange.y)
            {
                changeInScale -= new Vector2(.1f, .1f);
                transform.localScale = changeInScale;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            _changeInSizeCoroutine = null;
        }
    }
}
