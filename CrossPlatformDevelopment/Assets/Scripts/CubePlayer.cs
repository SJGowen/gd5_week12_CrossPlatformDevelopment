using UnityEngine;
using UnityEngine.InputSystem;

public class CubePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveInput = Vector2.zero;

    void Update()
    {
        var movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void ChangeColour()
    {
        var colour = Color.HSVToRGB(Random.value, 0.8f, 1f);
        GetComponent<Renderer>().material.color = colour;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
