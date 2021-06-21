using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerControls : MonoBehaviour {
    public float weightFactor = 10.0f;
    public float moveSpeed;
    public float movementFalloffRate = 0.5f;
    public float jumpForce = 10.0f;
    public float jumpTimer = 1.0f;
    public float jumpFalloffRate = 0.5f;
    public float jetTimer = 1.0f;
    public float jetMovementFalloff = 1.0f;
    public float dashForce = 3.0f;
    public float dashTimer = 0.5f;
    public float dashCooldown = 0.5f;
    public float grappleSpeed = 5.0f;

    [Space(10.0f)]
    [Header("Unlocks")]
    public bool obtainedDash = false;
    public bool obtainedJetpack = false;
    public bool obtainedGrapple = false;

    [Space(10.0f)]
    [Header("References")]
    public Rigidbody2D rigidbody2D;
    public GameObject grappleHook;
    public Camera camera;

    Vector2 _moveVec = Vector2.zero;
    Vector2 _mouseVec = Vector2.zero;

    float _jumpTimer = 0.0f;
    float _jumpForce = 0.0f;
    [HideInInspector] public bool jumpComplete = false;

    float _jetTimer = 0.0f;
    bool _jetOn = false;
    bool _jetComplete = false;

    bool _dashComplete = false;
    float _dashTimer = 0.0f;

    bool _grappleComplete = false;

    private void Start() {
        _jumpForce = jumpForce;
        grappleHook.transform.SetParent(null);
    }

    private void FixedUpdate() {
        rigidbody2D.mass = weightFactor;
        var force = _moveVec * moveSpeed;
        force.y = 0.0f;
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);

        var vel = rigidbody2D.velocity;
        if(_jetOn)
            vel *= jetMovementFalloff;
        else
            vel *= movementFalloffRate;

        vel.y = 0.0f;
        if(vel.magnitude > 1.0f)
            rigidbody2D.velocity -= vel;

    }

    public void OnMove(CallbackContext ctx) {
        _moveVec = ctx.ReadValue<Vector2>();
    }

    public void OnMouse(CallbackContext ctx) {
        _mouseVec = ctx.ReadValue<Vector2>();
        Debug.Log(_mouseVec);
    }

    public void ResetJumpComplete() {
        jumpComplete = false;
        _dashComplete = false;
        _jetComplete =  false;
    }

    public void OnJump(CallbackContext ctx) {
        if(ctx.canceled) {
            jumpComplete = true;
            _jumpTimer = 0.0f;
        }
        else if(ctx.performed) {
            IEnumerator Jump() {
                while(_jumpTimer < jumpTimer && !jumpComplete) {
                    yield return new WaitForFixedUpdate();
                    _jumpTimer += Time.fixedDeltaTime;
                    rigidbody2D.AddForce(new Vector2(0.0f, _jumpForce), ForceMode2D.Impulse);
                    _jumpForce /= jumpFalloffRate;
                }
                _jumpTimer = 0.0f;
                _jumpForce = jumpForce;
                jumpComplete = true;
            }
            StartCoroutine(Jump());
        }
    }
    public void OnJetpack(CallbackContext ctx) {
        if(!obtainedJetpack || _jetOn || _jetComplete || grappleHook.activeSelf)
            return;
        if(ctx.canceled) {
            _jetOn = false;
            _jetComplete = true;
            _jetTimer = 0.0f;
        }
        else if(ctx.performed) {
            IEnumerator Jetpack() {
                _jetOn = true;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                while(_jetTimer < jetTimer) {
                    yield return new WaitForFixedUpdate();
                    _jetTimer += Time.fixedDeltaTime;
                }
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                _jetTimer = 0.0f;
                _jetOn = false;
                _jetComplete = true;
            }
            StartCoroutine(Jetpack());
        }
    }

    public void OnDash(CallbackContext ctx) {
        if(!obtainedDash || _jetOn || _dashComplete || grappleHook.activeSelf)
            return;
        if(ctx.canceled) {
            _dashComplete = true;
            _dashTimer = 0.0f;
        }
        else if(ctx.performed) {
            IEnumerator Dash() {
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                while(_dashTimer < dashTimer) {
                    yield return new WaitForFixedUpdate();
                    _dashTimer += Time.fixedDeltaTime;
                    var vec = _moveVec;
                    vec.y = 0.0f;
                    rigidbody2D.AddForce(vec * dashForce, ForceMode2D.Impulse);

                }
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                _dashTimer = 0.0f;
                _dashComplete = true;
            }
            StartCoroutine(Dash());
        }

    }

    public void OnGrapple(CallbackContext ctx) {
        if(!obtainedGrapple)
            return;
        if(ctx.canceled)
            _grappleComplete = true;
        else if(ctx.performed) {
            _grappleComplete = false;
            IEnumerator GrappleHook() {
                grappleHook.SetActive(true);
                var p = camera.WorldToScreenPoint(gameObject.transform.position);
                var dir = (_mouseVec - new Vector2(p.x, p.y)).normalized;
                var r = grappleHook.GetComponent<Rigidbody2D>();
                grappleHook.transform.position = gameObject.transform.position + new Vector3(dir.x, dir.y, 0.0f) * 1.5f;
                r.velocity = Vector2.zero;
                r.AddForce(dir * grappleSpeed);
                while(!_grappleComplete) {
                    yield return null;
                }
                grappleHook.SetActive(false);
            }
            StartCoroutine(GrappleHook());
        }
    }

    //something to add to make it nicer:
    //shoot out a ray in the direction of the player
    //find a point in that direction that is not inside another point (box collider?)
    //lerp to that point instead
    bool _lerping = false;
    public void GrappleLerp() {
        if(_lerping)
            return;
        IEnumerator LerpToGrapple() {
            _lerping = true;
            float u = 0.0f;
            var r = grappleHook.GetComponent<Rigidbody2D>();
            r.velocity = Vector2.zero;
            r.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            var p = gameObject.transform.position;
            while(u < 1.0f && !_grappleComplete) {
                yield return new WaitForEndOfFrame();
                u += Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(p, grappleHook.transform.position, u);
            }
            r.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            grappleHook.SetActive(false);
            _lerping = false;
        }
        StartCoroutine(LerpToGrapple());
    }
}
