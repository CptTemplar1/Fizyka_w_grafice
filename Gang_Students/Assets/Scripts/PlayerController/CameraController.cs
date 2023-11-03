using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za kontrolę kamery.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Referencja do transformacji gracza, którą kamera będzie śledzić.
    /// </summary>
    [SerializeField]
    public Transform player;

    /// <summary>
    /// Przesunięcie pozycji kamery względem gracza.
    /// </summary>
    [SerializeField]
    public Vector3 positionOffset;

    /// <summary>
    /// Odległość kamery od gracza.
    /// </summary>
    [SerializeField]
    public float distance = 10.0f;

    /// <summary>
    /// Prędkość obrotu kamery.
    /// </summary>
    [SerializeField]
    public float rotateSpeed = 5.0f;

    /// <summary>
    /// Gładkość poruszania kamery.
    /// </summary>
    [SerializeField]
    public float smoothness = 0.25f;

    /// <summary>
    /// Minimalny kąt obrotu kamery w pionie.
    /// </summary>
    [SerializeField]
    public float minAngle = -45.0f;

    /// <summary>
    /// Maksymalny kąt obrotu kamery w pionie.
    /// </summary>
    [SerializeField]
    public float maxAngle = -10.0f;

    private Camera cam;
    private float currentX = 0.0f, currentY = 0.0f;

    void Start()
    {
        // Blokowanie kursora i ukrywanie go na starcie gry.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = Camera.main;
    }

    void Update()
    {
        // Obsługa ruchu kamery na podstawie ruchu myszy.
        currentX = currentX + Input.GetAxis("Mouse X") * rotateSpeed;
        currentY = currentY + Input.GetAxis("Mouse Y") * rotateSpeed;

        currentY = Mathf.Clamp(currentY, minAngle, maxAngle);
    }

    void FixedUpdate()
    {
        // Obliczenie pozycji kamery na podstawie obrotu i odległości od gracza.
        Vector3 dir = new Vector3(0, 1, -distance);
        Quaternion rotation = Quaternion.Euler(-currentY, -currentX, 0);
        cam.transform.position = Vector3.Lerp(cam.transform.position, player.position + rotation * dir, smoothness);

        // Skierowanie kamery na gracza z uwzględnieniem pozycji offsetu.
        cam.transform.LookAt(player.position + positionOffset);
    }
}
