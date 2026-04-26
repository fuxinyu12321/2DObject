using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour
{
    private GameObject player;
    private PolygonCollider2D polygonCollider2D;
    public GameObject DeathMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        polygonCollider2D= player.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isGameAlive)
        {
            DeathMenuUI.SetActive(true);
            if (Input.GetButtonDown("Revive"))
            {
                
                revive();
            }
            
        }
    }
    void revive()
    {
        DeathMenuUI.SetActive(false);
        player.transform.position= gameObject.transform.position;
        player.SetActive(true);
        PlayerController.isGameAlive = true;
        HealthBar.HealthCurrent = HealthBar.HealthMax;
        PlayerHealth.health = HealthBar.HealthMax;
        polygonCollider2D.enabled = true;
    }
}
