using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("Camera Movement")]
    [SerializeField] private Camera camera;

    [Header("Inventory")]
    [SerializeField] private Transform inventoryGO;

    // ---------------------------

    private PlayerMovement playerMovement;
    private bool inventoryState = false;

    // ---------------------------

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        if (Input.GetKeyDown("i"))
        {
            inventoryState = !inventoryState;
            playerMovement.StopPlayer(inventoryState);

            camera.GetComponent<CameraMovement>().Zoom(inventoryState);

            if(inventoryState)
                inventoryGO.GetComponent<Animator>().SetTrigger("Open");
            else
                inventoryGO.GetComponent<Animator>().SetTrigger("Close");
        }

    }

}
