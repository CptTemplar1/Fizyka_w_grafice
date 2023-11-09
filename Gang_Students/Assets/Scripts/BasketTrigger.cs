using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator slideDoor;
    
    [SerializeField]
    private AudioSource audioSource;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            slideDoor.Play("slideOpen");
            audioSource.Play();
        }
    }
}
