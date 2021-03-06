using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class PlayerDash : MonoBehaviour
{
  public PlayerMovement pm;
  private Rigidbody2D rb;

  Vector2 _velocity;
  float _gravity;

  [SerializeField]
  float dashSpeed = 8f;
  [SerializeField]
  float dashTime = 0.38f;
  float _dashTime;
  [SerializeField]
  float dashFatigueWaitTime = 0.38f;
  float dashFatigue;

  void DashAction(InputAction.CallbackContext context)
  {
    if (currentState == playerState.Normal && dashFatigue == 0)
    {
      currentState = playerState.Dashing;
      StartCoroutine(Dash());
    }
  }

  IEnumerator Dash()
  {
    float direction = Mathf.Sign(transform.rotation.y);
    _velocity = rb.velocity;
    rb.gravityScale = 0;
    _dashTime = dashTime;

    rb.velocity = new Vector2(direction * dashSpeed, 0);

    // wait till dash is over
    while (_dashTime > 0)
    {
      _dashTime -= Time.deltaTime;
      yield return null;
    }
    // reset gravity and x velocity
    rb.gravityScale = _gravity;
    rb.velocity = new Vector2(_velocity.x, 0);
    currentState = playerState.Normal;
    dashFatigue = dashFatigueWaitTime;
    while (dashFatigue > 0)
    {
      dashFatigue -= Time.deltaTime;
      yield return null;
    }
    dashFatigue = 0;
  }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    _gravity = rb.gravityScale;
  }
  void Start()
  {
    playerInputActions.Player.Dash.performed += DashAction;
  }
}
