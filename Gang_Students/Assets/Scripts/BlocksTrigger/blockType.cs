using UnityEngine;
/// <summary>
/// Reprezentuje typ bloku w grze, identyfikowany przez numer.
/// </summary>
/// <remarks>
/// Ta prosta klasa jest u¿ywana do reprezentowania ró¿nych typów bloków w grze. 
/// Ka¿dy blok jest identyfikowany unikalnym numerem.
/// </remarks>
public class blockType : MonoBehaviour
{   
    [SerializeField]
    public int blockNumber;
}
