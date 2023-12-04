using UnityEngine;

/// <summary>
/// Wykrywa wej�cie gracza w okre�lony obszar i wystrzeliwuje w jego kierunku kule armatnie.
/// </summary>
public class CannonBallTrigger : MonoBehaviour
{
    public GameObject cannonBallPrefab; ///< Prefab kuli armatniej do wystrzelenia.
    public GameObject player; ///< Gracz, w kierunku kt�rego zostanie wystrzelona kula.
    public float cannonBallForce; ///< Si�a, z jak� kula zostanie wystrzelona.
    public Transform cannonBallSpawnPoint; ///< Miejsce, gdzie kula zostanie zespawnowana.
    public AudioSource cannonExplosion; ///< �r�d�o d�wi�ku wystrza�u z armaty.
    public GameObject explosionParticles; ///< System cz�steczkowy dla efekt�w wybuchu.

    private bool wasFired; ///< Flaga zapobiegaj�ca wielokrotnemu wystrzeleniu kuli.

    /// <summary>
    /// Wywo�ywana, gdy obiekt wchodzi w pole Collidera.
    /// Wystrzeliwuje kule armatni� w kierunku gracza, je�li warunki s� spe�nione.
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
    /// Tworzy i wystrzeliwuje kule armatni� w kierunku gracza.
    /// </summary>
    void SpawnCannonBall()
    {
        //odegranie d�wi�ku wystzra�u armaty
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

            // Nadanie si�y do obiektu cannonBall w kierunku gracza
            rb.AddForce(directionToPlayer * cannonBallForce);
        }
    }
}
