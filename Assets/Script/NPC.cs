using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject textBox;
    public Text dialogueText;
    public string signText;
    public bool isPlayerInNPC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)&&isPlayerInNPC)
        {
            dialogueText.text = signText;
            textBox.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isPlayerInNPC = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isPlayerInNPC = false;
            textBox.SetActive(false);
        }
    }
}
