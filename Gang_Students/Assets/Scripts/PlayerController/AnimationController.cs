using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za kontrolę animacji obiektu za pomocą ConfigurableJoint.
/// </summary>
public class AnimationController : MonoBehaviour
{
    /// <summary>
    /// Określa, czy obroty mają być odwrócone.
    /// </summary>
    [SerializeField]
    private bool invertRotation;

    /// <summary>
    /// Referencja do obiektu ConfigurableJoint.
    /// </summary>
    [SerializeField]
    private ConfigurableJoint thisJoint;

    /// <summary>
    /// Referencja do docelowego obiektu animacji.
    /// </summary>
    [SerializeField]
    private Transform animationTarget;

    private Quaternion Rotation;

    void Start()
    {
        // Inicjalizacja początkowej rotacji
        Rotation = Quaternion.Inverse(animationTarget.localRotation);
    }

    void LateUpdate()
    {
        if (invertRotation)
        {
            // Odwrócenie rotacji, jeśli invertRotation jest ustawione
            thisJoint.targetRotation = Quaternion.Inverse(animationTarget.localRotation * Rotation);
        }
        else
        {
            // Ustawienie rotacji na podstawie docelowego obiektu animacji
            thisJoint.targetRotation = animationTarget.localRotation * Rotation;
        }
    }
}
