using UnityEngine;

/// <summary>
/// Wykrywa wejœcie gracza w okreœlony obszar i wystrzeliwuje w jego kierunku kule armatnie.
/// </summary>
public class CannonBallTrigger : MonoBehaviour
{
    public GameObject cannonBallPrefab; ///< Prefab kuli armatniej do wystrzelenia.
    public GameObject player; ///< Gracz, w kierunku którego zostanie wystrzelona kula.
    public float cannonBallForce; ///< Si³a, z jak¹ kula zostanie wystrzelona.
    public Transform cannonBallSpawnPoint; ///< Miejsce, gdzie kula zostanie zespawnowana.
    public AudioSource cannonExplosion; ///< ród³o dŸwiêku wystrza³u z armaty.
    public GameObject explosionParticles; ///< System cz¹steczkowy dla efektów wybuchu.

    private bool wasFired; ///< Flaga zapobiegaj¹ca wielokrotnemu wystrzeleniu kuli.

    /// <summary>
    /// Wywo³ywana, gdy obiekt wchodzi w pole Collidera.
    /// Wystrzeliwuje kule armatni¹ w kierunku gracza, jeœli warunki s¹ spe³nione.
    /// </summary>
    /// <param name="other">Collider innego obiektu.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !wasFired)
        {
            SpawnCannonBall();
            wasFired = true;
        }
    }

    /// <summary>
    /// Tworzy i wystrzeliwuje kule armatni¹ w kierunku gracza.
    /// </summary>
    void SpawnCannonBall()
    {
        //odegranie dŸwiêku wystzra³u armaty
        cannonExplosion.Play();

        explosionParticles.SetActive(true);
        
        // Tworzenie nowego obiektu cannonBall w miejscu spawnu
        GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.position, Quaternion.identity);

        // Pobranie komponentu Rigidbody obiektu cannonBall
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Obliczenie kierunku od cannonBall do gracza
            Vector3 directionToPlayer = (player.transform.position - cannonBall.transform.position).normalized;

            // Nadanie si³y do obiektu cannonBall w kierunku gracza
            rb.AddForce(directionToPlayer * cannonBallForce);
        }
    }
}
