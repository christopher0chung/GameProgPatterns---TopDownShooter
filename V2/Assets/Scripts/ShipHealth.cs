using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipHealth : MonoBehaviour, IShootable {

    [SerializeField] private int health;
    public delegate void Hit (float _health);
    public static Hit onHit; 

    public void OnShoot(int damage)
    {
        health -= damage;
        onHit(health);
        if (health <= 0)
        {
            //explode
        }
    }
}
