using System.Collections;
using UnityEngine;

/// <summary>
/// Klasa obsługująca zachowanie obu rąk aktywnego ragdolla.
/// </summary>
public class HandController : MonoBehaviour
{
    [SerializeField]
    public PlayerController Player_Controller;

    [SerializeField]
    public bool isLeft, hasJoint;

    [SerializeField]
    public Rigidbody GrabbedObject;

    /// <summary>
    /// Metoda Update, wywoływana co klatkę, kontroluje zachowanie ręki.
    /// </summary>
    void Update()
    {
        if (Player_Controller.useControls)
        {
            //Dla lewej ręki niszczy połączenie Joint po puszczeniu przycisku myszy
            if (isLeft)
            {
                if (hasJoint && Input.GetAxisRaw(Player_Controller.reachLeft) == 0)
                {
                    this.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                    hasJoint = false;
                    GrabbedObject = null;
                }

                if (hasJoint && this.gameObject.GetComponent<FixedJoint>() == null)
                {
                    hasJoint = false;
                    GrabbedObject = null;
                }
            }

            //Dla prawej ręki niszczy połączenie Joint po puszczeniu przycisku myszy
            if (!isLeft)
            {
                if (hasJoint && Input.GetAxisRaw(Player_Controller.reachRight) == 0)
                {
                    this.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                    hasJoint = false;
                    GrabbedObject = null;
                }

                if (hasJoint && this.gameObject.GetComponent<FixedJoint>() == null)
                {
                    hasJoint = false;
                    GrabbedObject = null;
                }
            }
        }
    }

    /// <summary>
    /// Metoda wywoływana, gdy ręka koliduje z innym obiektem.
    /// </summary>
    /// <param name="col">Obiekt Collision reprezentujący kolizję.</param>
    void OnCollisionEnter(Collision col)
    {
        if (Player_Controller.useControls)
        {
            //Dla lewej ręki tworzy nowe łączenie Joint jeśli koliduje z obiektem "CanBeGrabbed"
            if (isLeft)
            {
                if (col.gameObject.tag == "CanBeGrabbed" && col.gameObject.layer != LayerMask.NameToLayer(Player_Controller.ragdollLayer) && !hasJoint)
                {
                    if (Input.GetAxisRaw(Player_Controller.reachLeft) != 0 && !hasJoint)
                    {
                        hasJoint = true;
                        GrabbedObject = col.gameObject.GetComponent<Rigidbody>();
                        this.gameObject.AddComponent<FixedJoint>();
                        this.gameObject.GetComponent<FixedJoint>().breakForce = Mathf.Infinity;
                        this.gameObject.GetComponent<FixedJoint>().connectedBody = col.gameObject.GetComponent<Rigidbody>();
                    }
                }
            }

            //Dla prawej ręki tworzy nowe łączenie Joint jeśli koliduje z obiektem "CanBeGrabbed"
            if (!isLeft)
            {
                if (col.gameObject.tag == "CanBeGrabbed" && col.gameObject.layer != LayerMask.NameToLayer(Player_Controller.ragdollLayer) && !hasJoint)
                {
                    if (Input.GetAxisRaw(Player_Controller.reachRight) != 0 && !hasJoint)
                    {
                        hasJoint = true;
                        GrabbedObject = col.gameObject.GetComponent<Rigidbody>();
                        this.gameObject.AddComponent<FixedJoint>();
                        this.gameObject.GetComponent<FixedJoint>().breakForce = Mathf.Infinity;
                        this.gameObject.GetComponent<FixedJoint>().connectedBody = col.gameObject.GetComponent<Rigidbody>();
                    }
                }
            }
        }
    }
}
