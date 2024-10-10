using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color[] colorOptions = {Color.red, Color.green, Color.blue};
    public Renderer playerRenderer;
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public int colorIndex = 0, enemyCount = 0;

    GameObject nearestEnemy;
    ParticleSystem particleSys;

    public float bulletDelay, playerRange, distFromEnemy;
    float enemyPosX, enemyPosZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets Components
        playerRenderer.material.color = colorOptions[colorIndex];
        particleSys = gameObject.GetComponentInChildren<ParticleSystem>();
        //starts shooting
        StartCoroutine(StartShootSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver == false)
        {
            //gets the nearest enemy
            Debug.Log("Current enemy Count is: " + enemyCount);
            nearestEnemy = GameManager.Instance.enemyList[enemyCount];
            if (nearestEnemy != null)
            {
                distFromEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
                //Makes sure the player only rotates in the Y axis
                enemyPosX = nearestEnemy.transform.position.x;
                enemyPosZ = nearestEnemy.transform.position.z;
                //stares directly at enemy
                if (distFromEnemy <= playerRange)
                {
                    transform.LookAt(new Vector3(enemyPosX, transform.position.y, enemyPosZ));
                }
            }
        }
    }

    //shoots bullet from bulletpoint
    void Shoot()
    {
        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }

    //starts the shoot sequence
    IEnumerator StartShootSequence()
    {
        while(!GameManager.Instance.isGameOver)
        {
            yield return new WaitForSeconds(bulletDelay);
            Shoot();
        }
    }

    public void ChangeColor()
    {
        if (colorIndex < colorOptions.Length - 1)
        {
            colorIndex++;
        } else
        {
            colorIndex = 0;
        }
        playerRenderer.material.color = colorOptions[colorIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerRange);
    }

    public void ExplodePlayer()
    {
        particleSys.transform.parent = null;
        particleSys.Play();
        var main = particleSys.main;
        main.stopAction = ParticleSystemStopAction.Destroy; // Destroy after all particles finish
        AudioManager.Instance.PlayPlayerDeath();
        UIManager.Instance.OpenFailMenu();
        Destroy(this.gameObject);
    }
}
