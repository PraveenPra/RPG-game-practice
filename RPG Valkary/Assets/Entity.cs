using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

  #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

 [Header("Collision info")]
    [SerializeField] protected Transform GroundCheck;
    [SerializeField] protected float GroundCheckDistance;
    [SerializeField] protected Transform WallCheck;
    [SerializeField] protected float WallCheckDistance;
    [SerializeField] protected LayerMask whereIsGround;

    
    [Header("Flip info")]
    public int facingDir = 1;
    protected bool facingRight = true;

      protected virtual void Awake()
    {
        
    }

   protected virtual void Start()
    {
          anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

   protected virtual void Update()
    {
        
    }

     #region  CollisionDetect
    public virtual bool IsGroundDetected() => Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckDistance, whereIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(WallCheck.position, Vector2.right * facingDir, WallCheckDistance, whereIsGround);


    protected virtual void OnDrawGizmos()
    {

        //draw a line from the player(GroundCheck) position to whatever distance(groundcheckdist) value till it touches the ground slightly.Here the line goes down from player to gnd from(0,0) to (0,0-1) = downwards line
        Gizmos.DrawLine(GroundCheck.position, new Vector3(GroundCheck.position.x, GroundCheck.position.y - GroundCheckDistance));

        //Here the line goes down from player to rightside from(0,0) to (0 + 1,0) = rightside line. Just like a graph, draw which side the detection should happen 
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y));
    }

    #endregion

    
    #region  Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float _x)
    {
        //here x can be used while move, jump etc.. any state needing flip on x
        if (_x < 0 && facingRight)
            Flip();
        else if (_x > 0 && !facingRight)
            Flip();
    }
    #endregion

      #region Velocity

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {   //for movement
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    // public void SetAccelaration(float _xInitialVelocity,float _yInitialVelocity,float duration)
    // {//for gradual rising movements:dash
    //     // float speedMultiplier = 1.5f;
    //    Vector2 initialVelocity = new Vector2(0, 0);
    //    Vector2 finalVelocity = new Vector2(_xInitialVelocity * speedMultiplier, _yInitialVelocity * speedMultiplier);

    //    Vector2 accelaration = (finalVelocity - initialVelocity)/duration;

    //    rb.velocity = accelaration;
    //    FlipController(_xInitialVelocity);
    // }
    #endregion
}
