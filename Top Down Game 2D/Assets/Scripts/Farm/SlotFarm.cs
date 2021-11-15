using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Setting")]
    [SerializeField] private int digAmount; // quantidade de "escavação"
    [SerializeField] private float waterAmount; // total de agua
    [SerializeField] private bool detecing;

    private int initialDigAmount;
    private float currentWater;

    private bool digHole;

    PlayerItems playerItems;

    private void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (digHole)
        {
            if (detecing)
            {
                currentWater += 0.01f;
            }

            if (currentWater >= waterAmount)
            {
                spriteRenderer.sprite = carrot;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItems.Carrots++;
                    currentWater = 0f;
                }
            }
        }
        
    }

    public void OnHit()
    {
        digAmount--;
        if(digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            digHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }
        if(collision.CompareTag("Water")) {
            detecing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water")) {
            detecing = false;
        }
    }
}
