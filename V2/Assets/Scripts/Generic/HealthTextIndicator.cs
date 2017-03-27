using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextIndicator : MonoBehaviour
{

    [SerializeField] private Text myLife;
    [SerializeField] private float currentLife = 1;
    private Color invisColor;
    private Color visColor;

    void Awake()
    {
        ShipHealth.onHit += UpdateLife;
    }

    void Start()
    {
        myLife = GetComponent<Text>();
        visColor = new Color(0, .789f, 1f, 1);
        invisColor = new Color(0, .789f, 1f, 0);
        myLife.color = invisColor;
    }

    public void LateUpdate()
    {
        transform.parent.eulerAngles = Vector3.zero;
    }

    public void UpdateLife(float health)
    {
        StopAllCoroutines();
        myLife.text = health + "%";
        if (currentLife > 0)
        {
            currentLife = health;
            StartCoroutine(AnimateLife(currentLife));
        }
        else
        {
        }

    }

    private IEnumerator AnimateLife(float cL)
    {
        myLife.color = Color.Lerp(invisColor, visColor, cL); ;

        while (myLife.color.a > .001f)
        {
            myLife.color = Color.Lerp(myLife.color, invisColor, .15f);
            yield return null;
        }
        myLife.color = invisColor;
        yield break;
    }
}
