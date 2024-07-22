using UnityEngine;

public class DrawerController : MonoBehaviour
{
    public Transform drawer;  // �ekmece objesi
    public Transform player;  // Oyuncu objesi
    public float openDistance = 0.5f;  // �ekmece a��lma mesafesi
    public float openSpeed = 2f;  // �ekmece a��lma h�z�
    public float interactionDistance = 2f;  // Etkile�im mesafesi

    private bool isOpen = false;  // �ekmece a��k m�?
    private Vector3 closedPosition;  // �ekmece kapal� pozisyonu
    private Vector3 openPosition;  // �ekmece a��k pozisyonu

    void Start()
    {
        closedPosition = drawer.localPosition;
        openPosition = closedPosition + new Vector3(0, 0, openDistance);
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;  // �ekmece durumunu de�i�tir
        }

        if (isOpen)
        {
            drawer.localPosition = Vector3.Lerp(drawer.localPosition, openPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            drawer.localPosition = Vector3.Lerp(drawer.localPosition, closedPosition, Time.deltaTime * openSpeed);
        }
    }
}
