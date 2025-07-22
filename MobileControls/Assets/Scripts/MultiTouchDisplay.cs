using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextAlignment multiTouchInfoDisplay;
    private int maxTapCount = 5;
    private string multiTouchInfo;
    private Touch theTouch;

    void Update()
    {
        multiTouchInfo = $"Max tap count: {maxTapCount}\n";
        
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                theTouch = Input.GetTouch(i);
                multiTouchInfo += $"Touch {i}: Position: {theTouch.position}, Phase: {theTouch.phase},\n";
                multiTouchInfo += $"FingerID: {theTouch.fingerId},\n";
                multiTouchInfo += $"DeltaPosition: {theTouch.deltaPosition}, DeltaTime: {theTouch.deltaTime}.";
                Debug.Log(multiTouchInfo);
            }
        }
    }
}
