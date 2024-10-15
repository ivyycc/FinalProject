using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoZoom : MonoBehaviour
{
    // Camera Zoom
    public Camera playerCamera;
    public float normalFOV = 60f;  // Default field of view
    public float zoomFOV = 30f;    // Zoomed-in field of view
    public float zoomSpeed = 5f;   // Speed of zoom transition
    private bool isZoomed = false; // Tracks whether zoom is active

    // Photo-taking variables
    public List<Texture2D> photoGallery = new List<Texture2D>(); // Stores taken photos
    public RawImage photoDisplay; // UI element to display the photo
    public GameObject photoGalleryUI; // UI panel for viewing photos
    private int currentPhotoIndex = 0; // Track the currently viewed photo

    void Start()
    {
        // Initialize the camera's field of view to normal
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = normalFOV;
        }

        // Hide the photo gallery UI at the start
        if (photoGalleryUI != null)
        {
            photoGalleryUI.SetActive(false);
        }
    }

    void Update()
    {
        HandleZoom();

        // Take a photo if the player is zoomed in and presses the Photo key
        if (isZoomed && Input.GetKeyDown(KeyCode.P))
        {
            TakePhoto();
        }

        // Open the photo gallery if G is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            TogglePhotoGallery();
        }

        // Cycle through photos if the gallery is open
        if (photoGalleryUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShowNextPhoto();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShowPreviousPhoto();
            }
        }
    }

    // Handle zoom logic
    void HandleZoom()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isZoomed = !isZoomed;
        }

        float targetFOV = isZoomed ? zoomFOV : normalFOV;
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }
    }

    // Take a photo and add it to the gallery
    void TakePhoto()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        playerCamera.targetTexture = renderTexture;
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        playerCamera.Render();
        RenderTexture.active = renderTexture;
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        photo.Apply();

        playerCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        photoGallery.Add(photo);
        Debug.Log("Photo taken and added to gallery. Total photos: " + photoGallery.Count);
    }

    // Toggle the photo gallery UI
    void TogglePhotoGallery()
    {
        if (photoGalleryUI != null)
        {
            bool isActive = photoGalleryUI.activeSelf;
            photoGalleryUI.SetActive(!isActive);
            if (!isActive && photoGallery.Count > 0)
            {
                DisplayPhoto(currentPhotoIndex);
            }
        }
    }

    // Display a photo in the gallery
    void DisplayPhoto(int index)
    {
        if (photoGallery.Count > 0 && index >= 0 && index < photoGallery.Count)
        {
            Texture2D photo = photoGallery[index];
            photoDisplay.texture = photo;
            // Adjust the aspect ratio
            float aspectRatio = (float)photo.width / (float)photo.height;
            photoDisplay.rectTransform.sizeDelta = new Vector2(photoDisplay.rectTransform.sizeDelta.y * aspectRatio, photoDisplay.rectTransform.sizeDelta.y);
        }
    }

    // Show the next photo
    void ShowNextPhoto()
    {
        if (photoGallery.Count > 0)
        {
            currentPhotoIndex = (currentPhotoIndex + 1) % photoGallery.Count;
            DisplayPhoto(currentPhotoIndex);
        }
    }

    // Show the previous photo
    void ShowPreviousPhoto()
    {
        if (photoGallery.Count > 0)
        {
            currentPhotoIndex = (currentPhotoIndex - 1 + photoGallery.Count) % photoGallery.Count;
            DisplayPhoto(currentPhotoIndex);
        }
    }
}
