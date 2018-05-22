using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FadeToBlack : MonoBehaviour {

    //Panel to fade to black
    public float screenAlpha;

    //Whether to fade in or out
    public bool fadeIn;
    public bool fadeOut;

    //Wether to transport player to ground (play mode) or air (edit mode)
    public bool transportCharacterToGround;
    public bool transportCharacterToAir;

    //bool to check if player is on ground or not
    public bool grounded;

    //Enabled if scene transition is needed
    public bool loadScene;

    //Array of positions for ground positions an array for air positions
    public Vector3[] playerGroundPositions;
    public Vector3[] playerTopViewPositions;

    //Index for the arrays to teleport to
    public int groundLocationIndex;
    public int topLocationIndex;

    //Array of integers defining scenes
    public int[] scenes;

    //Index for scene array to teleport to
    public int sceneIndex;

    //The player
    public GameObject player;

    //prevents multiple interactions
    public bool isBusy;


    //public bool temp code
    public bool lockCursor;

    private void Awake()
    {
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<CharacterController>().enabled = false;
    }


    private void Update()
    {
        Fade();

        if(screenAlpha >0)
        {
            isBusy = true;
        }
        else
        {
            isBusy = false;
        }

        //temp code
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;

        if (Input.GetButtonDown("Fire3"))
        {
            lockCursor = !lockCursor ? lockCursor = true : lockCursor = false;
        }
    }


    /// <summary>
    ///  fadeIn is enabled when screen needs to fade to black.
    ///  If screen is black options enabled ( transport to air , ground or load new level) are executed.
    ///  
    ///  fadeOut is enabled when player finished editing mode or loaded a new level.
    /// </summary>
    private void Fade()
    {

        if (fadeIn)
        {
            screenAlpha += Time.deltaTime * 0.5f;

            if (screenAlpha >= 1)
            {
                if (transportCharacterToGround)
                {
                    TransportPlayerToGround(groundLocationIndex);
                }

                if (transportCharacterToAir)
                {
                    TransportPlayerToAir(topLocationIndex);
                }

                if (loadScene)
                {
                    SceneTransition(sceneIndex);
                }

                screenAlpha = 1;
                fadeIn = false;
            }
        }
        if (fadeOut)
        {
            screenAlpha -= Time.deltaTime * 0.5f;

            if (screenAlpha <= 0)
            {
                screenAlpha = 0;
                fadeOut = false;
            }
        }
        transform.GetComponent<Image>().color = new Color(0, 0, 0, screenAlpha);
    }

    /// <summary>
    /// transports player to ground if option is enabled.
    /// </summary>
    /// <param name="index"></param>
    /// index defines what index of the array the target should be teleported to.
    /// 
    public void TransportPlayerToGround(int index)
    {
        player.transform.position = playerGroundPositions[index];
        transportCharacterToGround = false;
        StartCoroutine(FadeoutTimer(1.0f));

        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<CharacterController>().enabled = true;

        grounded = true;
    }

    /// <summary>
    /// transports player to air if option is enabled.
    /// </summary>
    /// <param name="index"></param>
    /// index defines what index of the array the target should be teleported to.
    public void TransportPlayerToAir(int index)
    {
        player.transform.position = playerTopViewPositions[index];
        transportCharacterToAir = false;
        StartCoroutine(FadeoutTimer(1.0f));

        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<CharacterController>().enabled = false;

        grounded = false;
    }

    /// <summary>
    /// Loads a new scene if option is enabled.
    /// </summary>
    /// <param name="index"></param>
    /// index defines which scene should be loaded in the array of scenes.
    public void SceneTransition(int index)
    {
        if (!isBusy)
        {
            SceneManager.LoadScene(index);
            loadScene = false;
            StartCoroutine(FadeoutTimer(1.0f));

            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<CharacterController>().enabled = false;
        }
    }

    public IEnumerator FadeoutTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fadeOut = true;
    }

    public void SwapToGround(int index)
    {
        if (!isBusy)
        {
            fadeIn = true;
            transportCharacterToGround = true;
            groundLocationIndex = index;
        }
    }

    public void SwapToAir(int index)
    {
        if (!isBusy)
        {
            fadeIn = true;
            transportCharacterToAir = true;
            topLocationIndex = index;
        }
    }
}
