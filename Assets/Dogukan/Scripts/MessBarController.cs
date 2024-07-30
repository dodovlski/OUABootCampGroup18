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

    // Da��n�kl�k bar�n� azaltmak i�in �a��r�lacak metot
    public void DecreaseMessBar(float amount)
    {
        messBar.value -= amount;
        if (messBar.value < messBar.minValue)
        {
            messBar.value = messBar.minValue;
        }
    }
}
