using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour, iShootable {

    public int health;

    public void OnShoot(int damage)
    {
        health -= damage;
    }
}
