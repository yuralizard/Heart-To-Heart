using UnityEngine;
using UnityEngine.EventSystems;

public class Closing : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel; 
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.gameObject.SetActive(false);
    }
}