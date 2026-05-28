using TMPro;
using UnityEngine;

public class SalirHoverScript : MonoBehaviour
{
    public TMP_Text myText;

    public void OnHoverEnter() {
        myText.text = "Forro!";
    }

    public void OnHoverExit() {
        myText.text = "Salir";
    }
}
