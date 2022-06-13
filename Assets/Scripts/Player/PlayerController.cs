using UnityEngine; //The core theory behind this way of doing could be applied to other engines (can't comment too much though on this though)

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private MovementData data;
  [SerializeField] private Input.InputReader inputReader;

  #region COMPONENTS
  public Rigidbody2D RB { get; private set; }
  #endregion

  #region STATE PARAMETERS
  public bool IsFacingRight { get; private set; }
  [field: SerializeField]
  public bool IsJumping { get; private set; }
  [field: SerializeField]
  public float LastOnGroundTime { get; private set; }
  #endregion

  #region INPUT PARAMETERS
  [field: SerializeField]
  public Vector2 MoveInput { get; private set; }
  [field: SerializeField]
  public float LastPressedJumpTime { get; private set; }
  #endregion

  #region CHECK PARAMETERS
  [Header("Checks")]
  [SerializeField] private Vector3 _groundCheckPoint;
  [SerializeField] private Vector2 _groundCheckSize;
  #endregion

  #region LAYERS & TAGS
  [Header("Layers & Tags")]
  [SerializeField] private LayerMask _groundLayer;
  #endregion

  private void Awake()
  {
    RB = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    #region SETUP INPUTS
    inputReader.JumpEvent += OnJump;
    inputReader.JumpCanceledEvent += OnJumpUp;
    inputReader.MoveEvent += OnMove;
    #endregion

    SetGravityScale(data.gravityScale);
    IsFacingRight = true;
  }


  private void Update()
  {
    #region TIMERS
    LastOnGroundTime -= Time.deltaTime;

    LastPressedJumpTime -= Time.deltaTime;
    #endregion

    #region GENERAL CHECKS
    if (MoveInput.x != 0)
      CheckDirectionToFace(MoveInput.x > 0);
    #endregion

    #region PHYSICS CHECKS
    if (!IsJumping)
    {
      //Ground Check
      if (Physics2D.OverlapBox(transform.position + _groundCheckPoint, _groundCheckSize, 0, _groundLayer)) //checks if set box overlaps with ground
        LastOnGroundTime = data.coyoteTime; //if so sets the lastGrounded to coyoteTime
    }
    #endregion

    #region GRAVITY
    if (RB.velocity.y >= 0)
      SetGravityScale(data.gravityScale);
    else if (MoveInput.y < 0)
      SetGravityScale(data.gravityScale * data.quickFallGravityMult);
    else
      SetGravityScale(data.gravityScale * data.fallGravityMult);
    #endregion

    #region JUMP CHECKS
    if (IsJumping && RB.velocity.y < 0)
    {
      IsJumping = false;
    }

    if (CanJump() && LastPressedJumpTime > 0)
    {
      IsJumping = true;
    }
    #endregion
  }

  private void FixedUpdate()
  {
    #region DRAG
    if (LastOnGroundTime <= 0)
      Drag(data.dragAmount);
    else
      Drag(data.frictionAmount);
    #endregion
  }

  #region INPUT CALLBACKS
  public void OnJump()
  {
    LastPressedJumpTime = data.jumpBufferTime;
  }

  public void OnJumpUp()
  {
    if (CanJumpCut())
      JumpCut();
  }

  private void OnMove(Vector2 inputMovement)
  {
    MoveInput = inputMovement;
  }
  #endregion

  #region MOVEMENT METHODS

  public void SetGravityScale(float scale)
  {
    RB.gravityScale = scale;
  }

  /// <summary>
  /// Stop the player movement when is not moving
  /// </summary>
  /// <param name="amount"></param>
  private void Drag(float amount)
  {
    var velocity = RB.velocity;
    Vector2 force = amount * velocity.normalized;
    force.x = Mathf.Min(Mathf.Abs(velocity.x), Mathf.Abs(force.x)); //ensures we only slow the player down, if the player is going really slowly we just apply a force stopping them
    force.y = Mathf.Min(Mathf.Abs(velocity.y), Mathf.Abs(force.y));
    force.x *= Mathf.Sign(RB.velocity.x); //finds direction to apply force
    force.y *= Mathf.Sign(RB.velocity.y);

    RB.AddForce(-force, ForceMode2D.Impulse); //applies force against movement direction
  }

  public void Run(float multiplier = 1,float lerpAmount = 1)
  {
    float targetSpeed = MoveInput.x * data.runMaxSpeed; //calculate the direction we want to move in and our desired velocity
    float speedDif = targetSpeed - RB.velocity.x; //calculate difference between current velocity and desired velocity

    #region Acceleration Rate
    float accelRate;

    //gets an acceleration value based on if we are accelerating (includes turning) or trying to decelerate (stop). As well as applying a multiplier if we're air borne
    if (LastOnGroundTime > 0)
      accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAcceleration : data.runDecceleration;
    else
      accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAcceleration * data.accelerationInAir : data.runDecceleration * data.deccelerationInAir;

    //if we want to run but are already going faster than max run speed
    if (((RB.velocity.x > targetSpeed && targetSpeed > 0.01f) || (RB.velocity.x < targetSpeed && targetSpeed < -0.01f)) && data.doKeepRunMomentum)
    {
      accelRate = 0; //prevent any deceleration from happening, or in other words conserve are current momentum
    }
    #endregion

    #region Velocity Power
    float velPower;
    if (Mathf.Abs(targetSpeed) < 0.01f)
    {
      velPower = data.stopPower;
    }
    else if (Mathf.Abs(RB.velocity.x) > 0 && (Mathf.Sign(targetSpeed) != Mathf.Sign(RB.velocity.x)))
    {
      velPower = data.turnPower;
    }
    else
    {
      velPower = data.accelerationPower;
    }
    #endregion

    // applies acceleration to speed difference, then is raised to a set power so the acceleration increases with higher speeds, finally multiplies by sign to preserve direction
    float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
    movement = Mathf.Lerp(RB.velocity.x, movement, lerpAmount); // lerp so that we can prevent the Run from immediately slowing the player down, in some situations eg wall jump, dash 

    RB.AddForce(movement * multiplier * Vector2.right); // applies force force to rigidbody, multiplying by Vector2.right so that it only affects X axis 

    if (MoveInput.x != 0)
      CheckDirectionToFace(MoveInput.x > 0);
  }

  #endregion
  // TODO: Change to only flip animation?
  private void Turn()
  {
    Vector3 scale = transform.localScale; //stores scale and flips x axis, "flipping" the entire gameObject around. (could rotate the player instead)
    scale.x *= -1;
    transform.localScale = scale;

    IsFacingRight = !IsFacingRight;
  }

  public void Jump()
  {
    //ensures we can't call a jump multiple times from one press
    LastPressedJumpTime = 0;
    LastOnGroundTime = 0;

    #region Perform Jump
    float force = data.jumpForce;
    if (RB.velocity.y < 0)
      force -= RB.velocity.y;

    RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    #endregion
  }

  public void JumpCut()
  {
    //applies force downward when the jump button is released. Allowing the player to control jump height
    RB.AddForce(Vector2.down * RB.velocity.y * (1 - data.jumpCutMultiplier), ForceMode2D.Impulse);
  }

  #region CHECK METHODS
  public void CheckDirectionToFace(bool isMovingRight)
  {
    if (isMovingRight != IsFacingRight)
      Turn();
  }

  private bool CanJump() => LastOnGroundTime > 0 && !IsJumping;
  
  private bool CanJumpCut() => IsJumping && RB.velocity.y > 0;

  #endregion

  #region GIZMOS
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(transform.position + _groundCheckPoint, _groundCheckSize);
  }
  #endregion
}