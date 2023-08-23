using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerFox : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;

    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator animator;

    bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // if movement input is not 0, try to move 
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.y, 0));
                }



                animator.SetBool("run2", success);
            }
            else
            {
                animator.SetBool("run2", false);
            }
            //print("isMoving:" + animator.GetBool("isMoving"));

            // set direction of sprite to movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                
            }


        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // check for potetial collisions
            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            //cant move if there is no direction to move in 
            return false;
        }
        
    }

    void OnMove (InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    
    void OnFire()
    {
        animator.SetTrigger("attackSword");
      
    }

    public void SwordAttack()
    {
        if (swordAttack != null) // Kiểm tra swordAttack có giá trị khác null hay không
        {
            LockMovement();

            if (spriteRenderer.flipX == true)
            {
                swordAttack.AttackLeft();
            }
            else
            {
                swordAttack.AttackRight();
            }
        }
        else
        {
            Debug.LogError("swordAttack is not assigned!"); // In ra lỗi để theo dõi khi có vấn đề về swordAttack
        }
    }

    public void EndSwordAttack ()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement ()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
  

}
