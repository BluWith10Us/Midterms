using Unity.Hierarchy;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;
    Renderer bulletRenderer;

    public float bulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets player component
        player = FindAnyObjectByType<Player>();

        //changes bullet color depending on player color
        bulletRenderer = GetComponent<Renderer>();
        bulletRenderer.material.color = player.colorOptions[player.colorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // Continue moving the bullet forward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Renderer enemyRenderer = other.gameObject.GetComponent<Renderer>();

            if (enemyRenderer != null && bulletRenderer.material.color == enemyRenderer.material.color)
            {
                float distFromEnemy = Vector3.Distance(player.transform.position, other.gameObject.transform.position);
                if (distFromEnemy <= player.playerRange)
                {
                    player.enemyCount++;
                }

                // Play enemy death particle system
                Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.PlayDeathParticle();
                }

                //plays death sounds
                AudioManager.Instance.PlayPoofSounds();

                // Destroy the bullet and the enemy if colors match
                Destroy(other.gameObject); // Destroy enemy
                Destroy(this.gameObject);  // Destroy bullet
            }
        }
    }
}
