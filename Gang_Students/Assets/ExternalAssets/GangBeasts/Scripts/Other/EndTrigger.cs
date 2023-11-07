using UnityEngine;

public class EndTrigger : MonoBehaviour
{    
    [SerializeField]
    public bool reachedTheEnd;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            reachedTheEnd = true;
        }
    }
}

