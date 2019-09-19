using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    [SerializeField]
    private int HP = 1;
    
    bool THE_BOY = false;
    
    [SerializeField]
    private GameObject absorbPrefab = null;
    [SerializeField]
    private GameObject enemyExplosionPrefab = null;

    private void Start()
    {
        FadeIn(fadeInTime);
        sprite = GetComponentInChildren<SpriteRenderer>();

        if (absorbPrefab == null)
        {
            absorbPrefab = Resources.Load<GameObject>("AbsorbBubble.prefab");
        }
    }

    private void Update()
    {
        if (fadeInLeft == 0) return;
        fadeInLeft = Mathf.Max(fadeInLeft - Time.deltaTime, 0);

        var newColor = sprite.color;
        newColor.a = Mathf.Lerp(1, 0, fadeInLeft / fadeInTime);
        sprite.color = newColor;
    }

    public void Damage(int damage = 1)
    {
        if (THE_BOY)
        {
            HP -= damage;
            if (HP <= 0)
            {
                ArenaEnemySpawner.ChangeTheBoy(gameObject);
                
                var enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.5f);
                Destroy(gameObject);
            }
        }
        else
        {
            if (absorbPrefab)
            {
                var absorb = Instantiate(absorbPrefab, gameObject.transform.position, Quaternion.identity);
                absorb.transform.SetParent(gameObject.transform);
                Destroy(absorb, 0.5f);
            }
        }
    }

    public void FadeIn(float _fadeInTime)
    {
        fadeInLeft = _fadeInTime;
    }

    public float FadeInLeft
    {
        get => fadeInLeft;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (fadeInLeft == 0 && coll.gameObject.tag == "Player")
        {
            CharacterLife life = coll.gameObject.GetComponent<CharacterLife>();
            life.Death();
            RelodScene.PressR();
        }
    }


    public void MakeBoy()
    {
        THE_BOY = true;
    }

    private float fadeInTime = 0.5f;
    private float fadeInLeft;
    private SpriteRenderer sprite;
}
