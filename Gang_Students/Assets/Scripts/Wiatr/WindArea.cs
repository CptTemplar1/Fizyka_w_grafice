using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reprezentuje obszar, w którym wiatr oddzia³uje na obiekty w grze.
/// </summary>
/// <remarks>
/// Klasa WindArea definiuje obszar w œrodowisku gry, gdzie wiatr ma wp³yw na obiekty.
/// Si³a i kierunek wiatru s¹ zdefiniowane i mog¹ byæ u¿yte do symulacji wp³ywu wiatru
/// na ró¿ne elementy gry, takie jak obiekty fizyczne.
/// </remarks>
public class WindArea : MonoBehaviour
{
    public float strength = 5;      ///si³a wiatru oddzia³uj¹cego na obiekty
    public Vector3 direction = new Vector3(0,0,1);   ///kierunek wiatru
}
