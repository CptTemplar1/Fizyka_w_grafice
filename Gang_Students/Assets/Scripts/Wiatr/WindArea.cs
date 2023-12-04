using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reprezentuje obszar, w kt�rym wiatr oddzia�uje na obiekty w grze.
/// </summary>
/// <remarks>
/// Klasa WindArea definiuje obszar w �rodowisku gry, gdzie wiatr ma wp�yw na obiekty.
/// Si�a i kierunek wiatru s� zdefiniowane i mog� by� u�yte do symulacji wp�ywu wiatru
/// na r�ne elementy gry, takie jak obiekty fizyczne.
/// </remarks>
public class WindArea : MonoBehaviour
{
    public float strength = 5;      ///si�a wiatru oddzia�uj�cego na obiekty
    public Vector3 direction = new Vector3(0,0,1);   ///kierunek wiatru
}
