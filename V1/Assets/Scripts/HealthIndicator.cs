using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour {

    [SerializeField] private Image myLife;
    [SerializeField] private float currentLife = 1;
    private Color invisColor;
    private Color visColor;

    void Awake ()
    {
        ShipHealth.onHit += UpdateLife;
    }

    void Start () {
        myLife = GetComponent<Image>();
        visColor = new Color(0, .789f, 1f, 1);
        invisColor = new Color(0, .789f, 1f, 0);
        myLife.color = invisColor;
	}

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    public void UpdateLife(float health)
    {
        StopAllCoroutines();
        if (currentLife > 0)
        {
            currentLife = health/100;
            Debug.Log(currentLife);

            StartCoroutine(AnimateLife(currentLife));
        }
        else
        {
            myLife.fillAmount = 0;
        }

    }

    private IEnumerator AnimateLife (float cL)
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
