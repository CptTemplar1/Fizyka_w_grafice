using UnityEngine;

public class CannonBallTrigger : MonoBehaviour
{
    public GameObject cannonBallPrefab; // Prefab cannonBall, który chcesz zespawniæ
    public GameObject player; // Gracz, do którego zostanie skierowana kula
    public float cannonBallForce = 1000f; // Si³a, z jak¹ zostanie wystrzelona kula
    public Transform cannonBallSpawnPoint; // Miejsce spawnu pocisku
    public AudioSource cannonExplosion; //Ÿród³o dŸwiêku wystrza³u z armaty
    public GameObject explosionParticles; //ParticleSystem z wybuchem 
    
    private bool wasFired; //flaga zapobiegaj¹ca strzeleniu kilka razy

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !wasFired)
        {
            SpawnCannonBall();
            wasFired = true;
        }
    }

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
