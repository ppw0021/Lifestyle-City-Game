// using System.Collections;
// using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEngine;
// using UnityEngine.EventSystems;

// [RequireComponent(typeof(Image))]
// public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
// {
//     public TabGroup tabGroup;

//     public Image background;

//     void start()
//     {
//         background = GetComponent<Image>();
//         tabGroup.Suscribe(this);
//     }

//     public void OnPointerEnter(PointerEventData eventData)
//     {
//         tabGroup.OnTabEnter(this);
//     }

//     public void OnPointerExit(PointerEventData eventData)
//     {
//        tabGroup.OnTabExit(this);
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {
//        tabGroup.OnTabSelected(this);
//     }
// }
