using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public int damage;
    public float startTime;
    private Animator ani;
    private PolygonCollider2D polygoncollider2D;
    public float AttackCd; //攻击cd时长,以帧为单位
    // Start is called before the first frame update
    void Start()
    {
        ani=GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        polygoncollider2D=GetComponent<PolygonCollider2D>();
        polygoncollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))// 虚拟按钮名字为攻击
        {
            ani.SetTrigger("Attack");//播放攻击动画
            StartCoroutine(StartAttack());
        }

    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        polygoncollider2D.enabled = true;
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(AttackCd);
        polygoncollider2D.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage); 
        }
    }
}
