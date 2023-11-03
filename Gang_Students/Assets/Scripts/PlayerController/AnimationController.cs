using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private bool invertRotation;
    
    [SerializeField]
    private ConfigurableJoint thisJoint;
    
    [SerializeField]
    private Transform animationTarget;
    
    
    private Quaternion Rotation;
    
    
    void Start()
    {
        Rotation = Quaternion.Inverse(animationTarget.localRotation);
    }

    void LateUpdate()
    {
        if(invertRotation)
        {
			thisJoint.targetRotation = Quaternion.Inverse(animationTarget.localRotation * Rotation);
        }
        
		else
        {
			thisJoint.targetRotation = animationTarget.localRotation * Rotation;
        }
    }
}
