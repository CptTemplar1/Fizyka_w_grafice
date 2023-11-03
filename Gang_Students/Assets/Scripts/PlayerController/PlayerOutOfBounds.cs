using System.Collections;
using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za obsługę wykrywania gracza poza obszarem gry.
/// </summary>
public class PlayerOutOfBounds : MonoBehaviour
{
    /// <summary>
    /// Referencja do obiektu PlayerController.
    /// </summary>
    [SerializeField]
    private PlayerController playerController;

    /// <summary>
    /// Referencja do obiektu ragdolla gracza.
    /// </summary>
    [SerializeField]
    private GameObject ragdollPlayer;

    /// <summary>
    /// Referencja do korzenia ragdolla gracza.
    /// </summary>
    [SerializeField]
    private GameObject ragdollRoot;

    /// <summary>
    /// Referencja do kontrolera prawej ręki.
    /// </summary>
    [SerializeField]
    private HandController rightHandController;

    /// <summary>
    /// Referencja do kontrolera lewej ręki.
    /// </summary>
    [SerializeField]
    private HandController leftHandController;

    /// <summary>
    /// Punkt resetowania pozycji gracza.
    /// </summary>
    [SerializeField]
    private Transform resetPoint;

    /// <summary>
    /// Określa, czy kamera ma być natychmiastowo zaktualizowana.
    /// </summary>
    [SerializeField]
    private bool instantCameraUpdate = false;

    Camera cam;
    bool checkedTrigger;
    Rigidbody[] ragdollParts;
    Vector3 storedVelocity;

    void Awake()
    {
        cam = Camera.main;
    }

    /// <summary>
    /// Metoda wywoływana, gdy obiekt wchodzi w obszar wykrywania.
    /// </summary>
    /// <param name="col">Obiekt Collider reprezentujący kolizję.</param>
    void OnTriggerEnter(Collider col)
    {
        if (!checkedTrigger)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player") || col.gameObject.layer == LayerMask.NameToLayer("Ragdoll"))
            {
                checkedTrigger = true;

                // Wyłącz kontrolę gracza
                playerController.useControls = false;

                // Obsługa prawej ręki
                if (rightHandController.gameObject.GetComponent<FixedJoint>())
                {
                    rightHandController.gameObject.SetActive(false);
                    rightHandController.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                    rightHandController.GrabbedObject = null;
                    rightHandController.hasJoint = false;
                }

                // Obsługa lewej ręki
                if (leftHandController.gameObject.GetComponent<FixedJoint>())
                {
                    leftHandController.gameObject.SetActive(false);
                    leftHandController.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                    leftHandController.GrabbedObject = null;
                    leftHandController.hasJoint = false;
                }

                if (ragdollPlayer != null)
                {
                    ragdollParts = ragdollPlayer.GetComponentsInChildren<Rigidbody>();

                    // Deaktywacja fizyki i przechowanie prędkości
                    foreach (Rigidbody physics in ragdollParts)
                    {
                        storedVelocity = physics.velocity;
                        physics.isKinematic = true;
                    }

                    // Zapisanie aktualnego przesunięcia kamery
                    var cameraOffset = new Vector3(cam.transform.position.x - ragdollRoot.transform.position.x, cam.transform.position.y - ragdollRoot.transform.position.y, cam.transform.position.z - ragdollRoot.transform.position.z);

                    // Ustawienie gracza na nową pozycję
                    ragdollRoot.transform.localPosition = Vector3.zero;
                    ragdollPlayer.transform.position = resetPoint.position;

                    // Ponowne aktywowanie fizyki i zastosowanie przechowanej prędkości
                    foreach (Rigidbody physics in ragdollParts)
                    {
                        physics.isKinematic = false;
                        physics.velocity = storedVelocity;
                    }

                    // Zastosowanie przesunięcia kamery do nowej pozycji
                    if (instantCameraUpdate)
                    {
                        cam.transform.position = ragdollRoot.transform.position + cameraOffset;
                    }
                }

                checkedTrigger = false;

                // Włącz kontrolę gracza ponownie
                playerController.useControls = true;

                // Włącz obiekt kontrolera prawej ręki
                rightHandController.gameObject.SetActive(true);

                // Włącz obiekt kontrolera lewej ręki
                leftHandController.gameObject.SetActive(true);
            }
        }
    }
}
