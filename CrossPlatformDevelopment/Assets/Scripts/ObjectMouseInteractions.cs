using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMouseInteractions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Object clicked: {gameObject.name}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Pointer down on object: {gameObject.name}");
        material.color = Color.green;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer entered object: {gameObject.name}");
        material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer exited object: {gameObject.name}");
        material.color = Color.white;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"Pointer up on object: {gameObject.name}");
        material.color = Color.yellow;
    }
}
