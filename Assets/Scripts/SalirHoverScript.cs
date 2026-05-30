using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SalirHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text buttonText;

    [SerializeField] private string normalText = "Salir";
    [SerializeField] private string hoverText = "Forro!";

    void Start()
    {
        // Ensure the initial text matches the normal state
        if (buttonText != null)
        {
            buttonText.text = normalText;
        }
    }

    // Triggered automatically when the cursor moves over the button boundary
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.text = hoverText;
        }
    }

    // Triggered automatically when the cursor leaves the button boundary
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.text = normalText;
        }
    }
}