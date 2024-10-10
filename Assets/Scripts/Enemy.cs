using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    [Header("Red - 0, Green - 1, Blue - 2")]
    public int colorIndex;

    float playerPosX, playerPosZ;
    bool isRegistered = false; // Flag to check if registered

    public ParticleSystem particleSys;
    public AudioSource enemyAudio;
    Renderer enemyRenderer;
    Player playerScript;
    Transform playerInitPos;
    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets Components
        playerInitPos = GameObject.Find("Bean (Player)").GetComponent<Transform>();
        playerScript = GameObject.Find("Bean (Player)").GetComponent<Player>();
        enemyAudio = GetComponentInChildren<AudioSource>();
        particleSys = gameObject.GetComponentInChildren<ParticleSystem>();
        enemyRenderer = GetComponent<Renderer>();
        playerPosX = playerInitPos.position.x;
        playerPosZ = playerInitPos.position.z;

        //Changes Color
        Color[] enemyColor = { Color.red, Color.green, Color.blue };
        enemyRenderer.material.color = enemyColor[colorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isGameOver)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosX, transform.position.y, playerPosZ), enemySpeed * Time.deltaTime);
            transform.LookAt(playerInitPos);

            float distFromPlayer = Vector3.Distance(transform.position, playerInitPos.position);

            // Call RegisterGameObject only once when distance is below 5
            if (distFromPlayer < playerScript.playerRange && !isRegistered)
            {
                GameManager.Instance.EnterNewEnemy(this.gameObject); //Registers into array
                isRegistered = true; // Prevents future calls
            }
        }
    }

    public void PlayDeathParticle()
    {
        // Detach the particle system and plays eet
        particleSys.transform.parent = null;
        particleSys.Play();
        enemyAudio.Play();
        var main = particleSys.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.ExplodePlayer();

            GameManager.Instance.GameOver();


                PlayDeathParticle();
            // Destroy the enemy immediately
            Destroy(this.gameObject);
        }
    }
}
