using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _minTime = 0.5f;
    [SerializeField] private float _heightRange = 0.45f;
    [SerializeField] private GameObject _pipe;
    public Transform player;

    private float _timer;
    private List<GameObject> _spawnedPipes = new List<GameObject>();
    private int _maxPipeCount = 20;
    private bool canPUP = false;
    public GameObject Powerup;
    public float spawnchance = 25f;

    [Header("Boostpowerup field")]
    public bool speedup = false;
    public float speeding = 4f;
    private Coroutine blinkCoroutine;

    private float pipeSpeed = 4f; // Normal speed
    private float boostedPipeSpeed = 8f; // Boosted speed
    private float originalPipeSpeed;

    // Reference to the unchosen characters
    public List<GameObject> unchosenCharacters; // Add unchosen characters in the editor
    private List<GameObject> spawnedUnchosenCharacters = new List<GameObject>(); // Keep track of spawned unchosen characters
    public float detachmentRadius = 5f; // Radius to check for detachment
    private HashSet<GameObject> spawnedCharactersSet = new HashSet<GameObject>(); // Track spawned characters

    private List<GameObject> remainingUnchosenCharacters; // Track unchosen characters to be spawned
    private bool charactersSpawned = false; // Flag to check if the characters have been spawned

    // Reference to GameManager
    private GameManager gameManager;

    // Spawn chance for unchosen characters
    public float unchosenCharacterSpawnChance = 25f; // Adjust this value in the editor

    private void Start()
    {
        originalPipeSpeed = pipeSpeed; // Store the original pipe speed
        gameManager = FindObjectOfType<GameManager>(); // Find GameManager instance
        StartCoroutine(FindAndAssignPlayer()); // Find and assign the player
        StartCoroutine(SpawnPipeCoroutine());

        // Initialize the remainingUnchosenCharacters with unchosen characters from GameManager
        if (gameManager != null)
        {
            remainingUnchosenCharacters = new List<GameObject>(gameManager.GetUnchosenCharacters());

            if (remainingUnchosenCharacters.Count != 3)
            {
                Debug.LogError("Ensure the GameManager provides exactly 3 unchosen characters.");
            }
        }
        else
        {
            Debug.LogError("GameManager not found.");
        }
    }

    private IEnumerator FindAndAssignPlayer()
    {
        // Wait until the player is instantiated
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            yield return null;
        }
    }

    private IEnumerator SpawnPipeCoroutine()
    {
        while (true)
        {
            _timer += Time.deltaTime;

            if (_timer > _minTime && _timer < _maxTime && _spawnedPipes.Count < _maxPipeCount)
            {
                SpawnPipe();
                _timer = 0f;
            }

            if (speedup)
            {
                Speedboosthandle();
            }

            // Check for player proximity to unchosen characters
            for (int i = spawnedUnchosenCharacters.Count - 1; i >= 0; i--)
            {
                var character = spawnedUnchosenCharacters[i];
                if (character != null)
                {
                    float distance = Vector2.Distance(player.position, character.transform.position);

                    if (distance < detachmentRadius)
                    {
                        character.transform.parent = null; // Detach from pipe
                    }
                }
            }

            yield return null;
        }
    }

    private void Speedboosthandle()
    {
        speeding -= Time.deltaTime;
        player.GetComponent<Collider2D>().enabled = false;

        if (speeding <= 2f)
        {
            if (blinkCoroutine == null)
            {
                player.GetComponent<FlyBehaviour>().SetControl(true); // Re-enable player control
                StartBlinking(2f);
                ChangePipeSpeed(boostedPipeSpeed); // Change pipe speed to boosted speed
            }

            if (speeding <= 0f)
            {
                speedup = false;
                player.GetComponent<Collider2D>().enabled = true;
                speeding = 4f;
                StopBlinking();
                ChangePipeSpeed(originalPipeSpeed); // Revert pipe speed to original speed
            }
        }
        else
        {
            player.GetComponent<FlyBehaviour>().SetControl(false); // Disable player control and freeze position
        }
    }

    private void ChangePipeSpeed(float newSpeed)
    {
        foreach (var pipe in _spawnedPipes)
        {
            var pipeMovement = pipe.GetComponent<MovePipe>();
            if (pipeMovement != null)
            {
                pipeMovement.SetSpeed(newSpeed); // Ensure SetSpeed is a method on MovePipe
            }
        }
    }

    public void StartBlinking(float duration)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = StartCoroutine(BlinkingCoroutine(duration));
    }

    private void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
            // Ensure the player is fully visible
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private IEnumerator BlinkingCoroutine(float duration)
    {
        float endTime = Time.time + duration;
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        while (Time.time < endTime)
        {
            // Set sprite to invisible
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            yield return new WaitForSeconds(0.1f); // Adjust the duration as needed

            // Set sprite to visible
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f); // Adjust the duration as needed
        }

        // Ensure sprite is fully visible when blinking stops
        spriteRenderer.color = originalColor;
        blinkCoroutine = null;
    }

    private void SpawnPipe()
    {
        PUPRando();

        // Determine the right side of the screen in world coordinates
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1.2f, 0, Camera.main.nearClipPlane));
        float xPos = screenRight.x;

        // Randomize the height within the specified range
        float yPos = transform.position.y + Random.Range(-_heightRange, _heightRange);

        // Set the spawn position
        Vector3 spawnPos = new Vector3(xPos, yPos, 0);

        // Instantiate the pipe and add it to the list
        GameObject pipe = Instantiate(_pipe, spawnPos, Quaternion.identity);
        _spawnedPipes.Add(pipe);

        // Set the pipe speed
        var pipeMovement = pipe.GetComponent<MovePipe>();
        if (pipeMovement != null)
        {
            pipeMovement.SetSpeed(pipeSpeed);
        }

        // Destroy the pipe after 10 seconds
        StartCoroutine(DestroyPipeAfterTime(pipe, 10f));

        // Spawn Powerup
        if (canPUP)
        {
            var dpower = Instantiate(Powerup, new Vector3(spawnPos.x, spawnPos.y - 2f, spawnPos.z), Quaternion.Euler(0, 0, 45));
            StartCoroutine(DestroyPipeAfterTime(dpower, 10f));
        }

        // Spawn Unchosen Characters with a chance
        if (remainingUnchosenCharacters.Count > 0 && Random.value <= unchosenCharacterSpawnChance / 100f)
        {
            SpawnUnchosenCharacters(spawnPos, pipe);
        }
    }

    private void PUPRando()
    {
        float spawnnum = Random.Range(0, 100);
        canPUP = spawnnum <= spawnchance;
    }

    private void SpawnUnchosenCharacters(Vector3 spawnPos, GameObject pipe)
    {
        // Randomly select 1 unique character
        if (remainingUnchosenCharacters.Count > 0)
        {
            int randomIndex = Random.Range(0, remainingUnchosenCharacters.Count);
            GameObject unchosenCharacter = remainingUnchosenCharacters[randomIndex];
            remainingUnchosenCharacters.RemoveAt(randomIndex); // Remove to ensure uniqueness

            GameObject character = Instantiate(unchosenCharacter, spawnPos, Quaternion.identity);
            character.transform.parent = pipe.transform;

            // Setup the character
            character.tag = "Untagged";

            // Apply modifications only if in the main scene
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                // Setup the character
                character.tag = "Untagged";

                // Disable FlyBehaviour script
                FlyBehaviour flyBehaviour = character.GetComponent<FlyBehaviour>();
                if (flyBehaviour != null)
                {
                    flyBehaviour.enabled = false;
                }
                else
                {
                    Debug.LogError("FlyBehaviour script is missing on the unchosen character prefab.");
                }

                // Set gravity scale to 0
                Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 0;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    Debug.LogError("Rigidbody2D is missing on the unchosen character prefab.");
                }

                // Disable CircleCollider2D
                CircleCollider2D circleCollider = character.GetComponent<CircleCollider2D>();
                if (circleCollider != null)
                {
                    circleCollider.enabled = false; // Disable the collider
                }
                else
                {
                    Debug.LogError("CircleCollider2D is missing on the unchosen character prefab.");
                }

                // Enable FollowPlayer script
                FollowPlayer followPlayer = character.GetComponent<FollowPlayer>();
                if (followPlayer != null)
                {
                    followPlayer.player = player.transform; // Set the player reference
                    followPlayer.enabled = true; // Initially enable
                }
                else
                {
                    Debug.LogError("FollowPlayer script is missing on the unchosen character prefab.");
                }
            }

            // Add to the list of spawned unchosen characters
            spawnedUnchosenCharacters.Add(character);
            spawnedCharactersSet.Add(character); // Mark this character as spawned
        }
        else
        {
            charactersSpawned = true; // Ensure characters are spawned only once
        }
    }

    private IEnumerator DestroyPipeAfterTime(GameObject pipe, float delay)
    {
        yield return new WaitForSeconds(delay);
        _spawnedPipes.Remove(pipe);
        Destroy(pipe);
    }

    // Draw the detachment radius in the Scene view
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.position, detachmentRadius);
        }
    }
}
