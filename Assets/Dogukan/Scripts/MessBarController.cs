using UnityEngine;
using UnityEngine.UI;

public class MessBarController : MonoBehaviour
{
    public Slider messBar; // Da��n�kl�k bar� UI Slider

    // Da��n�kl�k bar�n� art�rmak i�in �a��r�lacak metot
    public void IncreaseMessBar(float amount)
    {
        messBar.value += amount;
        if (messBar.value > messBar.maxValue)
        {
            messBar.value = messBar.maxValue;
        }
    }
}
