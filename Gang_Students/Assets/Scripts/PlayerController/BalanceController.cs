using UnityEngine;

public class BalanceController : MonoBehaviour
{   
    [SerializeField]
    private PlayerController controller;
    
    void OnCollisionEnter(Collision col)
    {
        controller.PlayerGetUp();
    }
}
