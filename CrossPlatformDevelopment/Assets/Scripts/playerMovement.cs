using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public Vector2 inputDirection;
    public Vector2 lookDirection;
    Animator anim;

    private Vector2 touchStart;
    private Vector2 touchEnd;

    [Header("D-Pad Settings")]
    public GameObject dpad;
    public GameObject dpadBackground;
    public float dpadRadius = 15f;

    private Touch theTouch;
    private InputType inputType;

    private PlayerInput playerInput;
    private InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);

        //setup the New Input System
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");

        //turnoff the dpad and dpad background at the start
        dpadActivate(false);
    }

    // Update is called once per frame
    void Update()
    {
        //getting input from controls
        CalculateInputs();

        //sets up the animator
        AnimationSetup();

        //moves the player
        transform.Translate(moveSpeed * Time.deltaTime * inputDirection);
    }

    private void CalculateInputs()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            DebugLogInputMethod(InputType.Mobile);
            CalculateMobileInput();
        }
        //else if (Input.touchCount > 0)
        //{
        //    DebugLogInputMethod(InputType.Touch);
        //    CalculateTouchInputs();
        //}
        else
        {
            DebugLogInputMethod(InputType.Keyboard);
            CalculateDesktopInputs();
        }
    }

    private void DebugLogInputMethod(InputType input)
    {
        if (inputType != input)
        {
            inputType = input;
            Debug.Log("Input method changed to: " + inputType);
        }
    }

    private void dpadActivate(bool active)
    {
        if (dpad != null)
        {
            dpad.SetActive(active);
        }

        if (dpadBackground != null)
        {
            dpadBackground.SetActive(active);
        }
    }
    
    private void SetDpadBackgroundPosition(Vector2 position)
    {
        if (dpadBackground != null)
        {
            dpadBackground.transform.position = position;
        }
    }

    void CalculateDesktopInputs()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        //inputDirection = new Vector2(x, y).normalized;

        inputDirection = moveAction.ReadValue<Vector2>();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void CalculateMobileInput()
    {
        if (Input.GetMouseButton(0))
        {
            dpadActivate(true);

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
                SetDpadBackgroundPosition(touchStart);
                
            }

            touchEnd = Input.mousePosition;
            CalculateInputDirection(touchEnd);
            UpdateDpadPosition();
        }
        else
        {
            inputDirection = Vector2.zero;
            dpadActivate(false);
        }
    }

    //void CalculateTouchInputs()
    //{
    //    theTouch = Input.GetTouch(0);
    //    dpadActivate(true);

    //    if (theTouch.phase == TouchPhase.Began)
    //    {
    //        touchStart = theTouch.position;
    //        SetDpadBackgroundPosition(touchStart);
    //    }
    //    else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
    //    {
    //        touchEnd = theTouch.position;
    //        CalculateInputDirection(touchEnd);
    //        UpdateDpadPosition();
    //        dpadActivate(false);
    //    }
    //}

    private void UpdateDpadPosition()
    {
        if (dpad == null) return;

        if ((touchEnd - touchStart).magnitude > dpadRadius)
        {
            dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
        }
        else
        {
            dpad.transform.position = touchEnd;
        }
    }

    private void CalculateInputDirection(Vector2 position)
    {
        float x = touchEnd.x - touchStart.x;
        float y = touchEnd.y - touchStart.y;

        inputDirection = new Vector2(x, y).normalized;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
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
}

public enum InputType
{
    Keyboard,
    Mobile,
    Touch
}