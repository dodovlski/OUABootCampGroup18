using UnityEngine;

public class CabinetDoorController : MonoBehaviour
{
    public Transform door;  // Kapak objesi
    public Transform player;  // Oyuncu objesi
    public float openAngle = 90f;  // Kapak a��lma a��s�
    public float openSpeed = 2f;  // Kapak a��lma h�z�
    public float interactionDistance = 2f;  // Etkile�im mesafesi

    private bool isOpen = false;  // Kapak a��k m�?
    private Vector3 closedRotation;  // Kapak kapal� rotasyonu
    private Vector3 openRotation;  // Kapak a��k rotasyonu

    void Start()
    {
        closedRotation = door.localEulerAngles;
        openRotation = closedRotation + new Vector3(0, openAngle, 0);
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;  // Kapak durumunu de�i�tir
        }

        if (isOpen)
        {
            door.localEulerAngles = Vector3.Lerp(door.localEulerAngles, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            door.localEulerAngles = Vector3.Lerp(door.localEulerAngles, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
