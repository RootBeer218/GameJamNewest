using UnityEngine;

public class MorningToNight : MonoBehaviour
{
    public GameObject[] backgrounds; // Array to hold background images
    public float normalScrollSpeed = 0.5f; // Normal speed at which the background moves
    public float fastScrollSpeed = 2f; // Speed to move the background faster
    public Camera mainCamera; // Reference to the main camera
    private float imageWidth; // Width of the background images
    private bool isMovingFaster = false; // Flag to indicate if the background is moving faster

    void Start()
    {
        // Assuming all background images have the same width
        imageWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float currentScrollSpeed = normalScrollSpeed;

        // Determine the camera's view edges
        float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        // Check if the camera's right edge reaches the end of any background image
        bool shouldSpeedUp = false;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float backgroundRightEdge = backgrounds[i].transform.position.x + imageWidth / 2;
            if (!isMovingFaster && cameraRightEdge >= backgroundRightEdge)
            {
                shouldSpeedUp = true;
                break;
            }
        }

        if (shouldSpeedUp)
        {
            isMovingFaster = true;
        }

        // If moving faster, increase the scroll speed for all images
        if (isMovingFaster)
        {
            currentScrollSpeed = fastScrollSpeed;
        }

        // Move all backgrounds together at the current speed
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.left * currentScrollSpeed * Time.deltaTime;

            // If the background image is out of the camera's view (its right edge is left of the camera), reposition it
            if (backgrounds[i].transform.position.x + imageWidth / 2 < cameraLeftEdge)
            {
                RepositionBackground(i);
            }
        }

        // Check if the camera's left edge has reached the left edge of the next background image
        bool shouldSlowDown = false;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            int nextIndex = (i + 1) % backgrounds.Length;
            float backgroundLeftEdge = backgrounds[nextIndex].transform.position.x - imageWidth / 2;
            if (isMovingFaster && cameraLeftEdge <= backgroundLeftEdge)
            {
                shouldSlowDown = true;
                break;
            }
        }

        if (shouldSlowDown)
        {
            isMovingFaster = false; // Revert to normal scroll speed
        }
    }

    void RepositionBackground(int index)
    {
        // Find the rightmost background image
        int lastIndex = (index - 1 + backgrounds.Length) % backgrounds.Length;

        // Reposition the current background to the right of the last background
        backgrounds[index].transform.position = new Vector3(
            backgrounds[lastIndex].transform.position.x + imageWidth,
            backgrounds[index].transform.position.y,
            backgrounds[index].transform.position.z
        );
    }
}
