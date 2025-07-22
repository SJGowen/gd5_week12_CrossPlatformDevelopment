using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public Vector2 inputDirection, lookDirection;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);
    }

    void Update()
    {
        //getting input from keyboard controls
        CalculateDesktopInputs();

        //sets up the animator
        AnimationSetup();

        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }

    void CalculateDesktopInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(x, y).normalized;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void AnimationSetup()
    {
        //checking if the player wants to move the character or not
        if (inputDirection.magnitude > 0.1f)
        {
            //changes look direction only when the player is moving, so that we remember the last direction the player was moving in
            lookDirection = inputDirection;

            //sets "isWalking" true. this triggers the walking blend tree
            anim.SetBool("isWalking", true);
        }
        else
        {
            // sets "isWalking" false. this triggers the idle blend tree
            anim.SetBool("isWalking", false);

        }

        //sets the values for input and lookdirection. this determines what animation to play in a blend tree
        anim.SetFloat("inputX", lookDirection.x);
        anim.SetFloat("inputY", lookDirection.y);
        anim.SetFloat("lookX", lookDirection.x);
        anim.SetFloat("lookY", lookDirection.y);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
