using UnityEngine;

public class CannonBallTrigger : MonoBehaviour
{
    public GameObject cannonBallPrefab; // Prefab cannonBall, kt�ry chcesz zespawni�
    public GameObject player; // Gracz, do kt�rego zostanie skierowana kula
    public float cannonBallForce = 1000f; // Si�a, z jak� zostanie wystrzelona kula
    public Transform cannonBallSpawnPoint; // Miejsce spawnu pocisku
    public AudioSource cannonExplosion; //�r�d�o d�wi�ku wystrza�u z armaty
    public GameObject explosionParticles; //ParticleSystem z wybuchem 
    
    private bool wasFired; //flaga zapobiegaj�ca strzeleniu kilka razy

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
