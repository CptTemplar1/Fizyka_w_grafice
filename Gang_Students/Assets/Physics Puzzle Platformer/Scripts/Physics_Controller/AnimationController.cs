//----------------------------
//---Physics Puzzle Platformer
//---© TFM™
//-------------------

using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //Exposed Variables
    ///////////////////
    
    [SerializeField]
    private bool invertRotation;
    
    [SerializeField]
    private ConfigurableJoint thisJoint;
    
    [SerializeField]
    private Transform animationTarget;
    
    
    //Hidden Variables
    //////////////////
    
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
