// !!! NOTE: Don't forget to include needed libraries. !!!
// !!! NOTE: You can use keywords to search for specific functionality
// All keywords are written witout spaces (e.g. gameover, horizontalaxis, externalscript)!!!

/*
 * KEYWORD LIST:
 *  - addforce
 *  - animation
 *  - audio
 *  - axis
 *  - background
 *  - border
 *  - bounderie
 *  - collsiondetection
 *  - destroy
 *  - externalscript
 *  - getobject
 *  - gameobjectarray
 *  - gameover
 *  - getkey
 *  - getposition
 *  - getsize
 *  - input
 *  - location
 *  - move
 *  - particles
 *  - physicsgravity
 *  - playercontroll
 *  - random
 *  - repeating
 *  - rigidbody
 *  - sound
 *  - spawnmanager
 *  - tag
 *  - tag
 *  - tag
 *  - tag
 *  - tag

 */

/*
 * TABLE OF CONTENT:
 *  - Object related code
 *  - Animation related code
 *  - Texture related code
*/

/* ############################ Object related code ######################################################## */

// Implement the infinite movement of an object and the destruction if a bound is passed.
// If GameOver stop Moving.
// KEYWORDS: move, tag, destroy, gameover, externalscript, bounderie, border, getobject, playercontrollerscript

public class MoveLeftX : MonoBehaviour
{
    public float speed;
    private PlayerControllerX playerControllerScript;
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // If game is not over, move to the left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}

/* ---------------------------------------------------------------------------------------------------------- */

// Implement the controll of the horizontal axis of the player as well as collisiondetection.
// KEYWORDS: move, playercontroll, axis, collsiondetection, sound, particles, gameover, audio, physicsgravity, addforce, input, getkey, tag, destroy
public class PlayerCtrl : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public AudioClip explodeSound;

    public float speed = 20;
    public bool gameOver = false;

    private Rigidbody rb;
    private AudioSource playerAudio;
    private float gravityModifier = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(Vector3.forward * horizontalInput * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
    }
}

/* ---------------------------------------------------------------------------------------------------------- */

// Implement a spawn manager.
// KEYWORDS: gameobjectarray, spawnmanager, getobject, repeating, random, location, gameover

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;

    private float spawnDelay = 2;
    private float spawnInterval = 1.5f;
    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Spawn obstacles
    void SpawnObjects()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}

/* ############################ Animation related code ######################################################## */

// Generates a infinit moving background. It takes a background which is duplicated in the direction in which it shall move. */
// KEYWORDS: move, background, animation, getsize, getposition

public class RepeatBackgroundX : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private void Start()
    {
        // Establish the default starting position
        startPos = transform.position;
        // Set repeat width to half of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    private void Update()
    {
        // If background moves left by its repeat width, move it back to start position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}

/* ############################ Texture related code ######################################################## */

// Unifiy the size of texture models ondiferent sized objects

[ExecuteInEditMode]
public class TileTexture : MonoBehaviour
{
	void Start()
	{
		// Get scales
		float x = transform.localScale.x;
		float y = transform.localScale.y;
		float z = transform.localScale.z;
		float max = x > y ? x : y;
		max = max > z ? max : z;

		// Get the mesh
	    Mesh theMesh;
	    theMesh = this.transform.GetComponent<MeshFilter>().mesh as Mesh;

	    // Now store a local reference for the UVs
	    Vector2[] theUVs = new Vector2[theMesh.uv.Length];
	    theUVs = theMesh.uv;

		// Vertices order:
		//    2    3    0    1   z+
		//    6    7   10   11   z-
		//   19   17   16   18   x-
		//   23   21   20   22   x+
		//    4    5    8    9   Top
		//   15   13   12   14   Bottom

	    // set UV co-ordinates
		theUVs[2] = new Vector2(0f, y/max);
	    theUVs[3] = new Vector2(x/max, y/max);
	    theUVs[0] = new Vector2(0f, 0f);
	    theUVs[1] = new Vector2(x/max, 0f);

		theUVs[11] = new Vector2(0f, y/max);
	    theUVs[10] = new Vector2(x/max, y/max);
	    theUVs[7] = new Vector2(0f, 0f);
	    theUVs[6] = new Vector2(x/max, 0f);

		theUVs[19] = new Vector2(0f, y/max);
	    theUVs[17] = new Vector2(z/max, y/max);
	    theUVs[16] = new Vector2(0f, 0f);
	    theUVs[18] = new Vector2(z/max, 0f);

	    theUVs[23] = new Vector2(0f, y/max);
	    theUVs[21] = new Vector2(z/max, y/max);
	    theUVs[20] = new Vector2(0f, 0f);
	    theUVs[22] = new Vector2(z/max, 0f);

		theUVs[4] = new Vector2(0f, z/max);
	    theUVs[5] = new Vector2(x/max, z/max);
	    theUVs[8] = new Vector2(0f, 0f);
		theUVs[9] = new Vector2(x/max, 0f);

	    theUVs[15] = new Vector2(0f, 0f);
	    theUVs[13] = new Vector2(0f, 0f);
	    theUVs[12] = new Vector2(0f, 0f);
		theUVs[14] = new Vector2(0f, 0f);

	    // Assign the mesh its new UVs
	    theMesh.uv = theUVs;

		renderer.material.SetTextureScale("_MainTex", new Vector2(max/2f, max/2f));
	}
}
