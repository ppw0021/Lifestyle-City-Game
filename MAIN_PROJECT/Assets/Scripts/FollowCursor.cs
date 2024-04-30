using UnityEngine;
using UnityEngine.EventSystems;

public class FollowCursor : MonoBehaviour, IPointerClickHandler
{
    RectTransform rectTransform;
    bool shouldFollow = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollow && Input.mousePosition != null)
        {
            Vector2 mousePos = Input.mousePosition;
            rectTransform.position = mousePos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        shouldFollow = false;
    }
}
