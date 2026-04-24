using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSorting : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer npcRenderer;
    public SpriteRenderer playerRenderer;

    void Update()
    {
        // 比较Y轴位置
        if (player.position.y > transform.position.y)
        {
            // 玩家在NPC后面，NPC遮挡玩家
            npcRenderer.sortingOrder = 2;
            playerRenderer.sortingOrder = 1;
        }
        else
        {
            // 玩家在NPC前面，玩家遮挡NPC
            npcRenderer.sortingOrder = 1;
            playerRenderer.sortingOrder = 2;
        }
    }
}