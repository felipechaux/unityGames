using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour {

    public CollectableType type = CollectableType.money;

    //componententes internos
    private SpriteRenderer spriteCollectable;
    //collider del objeto
    private CircleCollider2D itemCollider;
    //objeto jugador
    GameObject player;


    bool hasBeenCollected = false;

    public int value = 1;

    private void Awake()
    {
        spriteCollectable = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        player= GameObject.Find("Player");
    }

    void Show()
    {
        spriteCollectable.enabled = true;
        itemCollider.enabled = true;
        //aun el objeto no ha sido recolectado
        hasBeenCollected = false;
    }

    void Hide()
    {
        spriteCollectable.enabled = false;
        itemCollider.enabled = false;
    }

    void Collect()
    {

        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.money:
                //notificar a game manager
                GameManager.sharedInstance.CollectObject(this);
                break;
            case CollectableType.healthPotion:
                //TODO: logica de la pocion de vida
                //player controler no tiene instancia compartida - asi que se accede por medio de gameobject
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                break;
            case CollectableType.manaPotion:
                //TODO: logica de la pocion de mana
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    //cuando colisionan con moneda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collect();
        }

    }

}
