using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public Vector2 inputDirection, lookDirection;
    Animator anim;

    private Vector2 touchStart, touchEnd;
    public Image dpad;
    public float dpadRadius = 30f;

    void Start()
    {
        anim = GetComponent<Animator>();

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);
    }

    void Update()
    {
        //getting input from keyboard controls
        CalculateMouseInputs();

        //sets up the animator
        AnimationSetup();

        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }

    void CalculateMouseInputs()
    {
        if (Input.GetMouseButton(0))
        {
            dpad.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
            }

            touchEnd = Input.mousePosition;

            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            inputDirection = new Vector2(x, y).normalized;

            if ((touchEnd - touchStart).magnitude > dpadRadius)
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
            }
            else
            {
                dpad.transform.position = touchEnd;
            }
        }
        else
        {
            dpad.gameObject.SetActive(false);
            inputDirection = Vector2.zero;
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
