using System.Collections;
using BehaviorTree;
using UnityEngine;

public class BTEnemy : MonoBehaviour, IShootable
{
    private Tree<BTEnemy> _tree;
    private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootRange;

    private const float MaxHealth = 10;
    [SerializeField] private float _health;
    public LayerMask myLM;

    private BulletManager myBM;

    private void Start()
    {
        myBM = GameObject.Find("Managers").GetComponent<BulletManager>();

        _player = GameObject.FindGameObjectWithTag("Player");
        _health = MaxHealth;

        _tree = new Tree<BTEnemy>(new Selector<BTEnemy>(

            new Sequence<BTEnemy>( 
                new IsInDanger(), 
                new Dodge() 
            ),
            
            new Sequence<BTEnemy>( 
                new IsPlayerInRange(), 
                new Attack() 
            ),
         
            new Idle()
        ));
    }

    private void Update()
    {
        // Update the tree by passing it the context that it should use to drive its decisions/act on
        _tree.Update(this);
        // Draw a red circle around the enemy so we can see the detection radius
        //DrawVisibilityRange();
    }

    public void OnShoot(int damage)
    {
        _health -= damage;
    }

    private void Strafe()
    {
        var playerDirection = (_player.transform.position - transform.position).normalized;
        var myRB = GetComponent<Rigidbody>();
        myRB.AddForce(Quaternion.Euler(0, 90, 0) * playerDirection * _speed, ForceMode.Force);
    }

    private float timer;

    private void Shoot()
    {
        timer += Time.deltaTime;
        if (timer >= .5f)
        {
            timer -= .5f;
            BulletBase myOrd = myBM.Make(this.transform.position + this.transform.forward * 3, this.gameObject.tag);
            myOrd.GetComponent<Rigidbody>().AddForce(this.transform.forward * 100, ForceMode.Impulse);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // NODES
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////
    // Conditions
    ////////////////////
    private class IsInDanger : Node<BTEnemy>
    {
        public override bool Update(BTEnemy enemy)
        {
            var playerDirection = (enemy._player.transform.position - enemy.transform.position).normalized;
            var myRB = enemy.GetComponent<Rigidbody>();
            myRB.AddForce(playerDirection * enemy._speed *.25f, ForceMode.Force);

            Ray NotRay = new Ray(enemy.transform.position, Vector3.one);
            RaycastHit[] theHits = Physics.SphereCastAll(NotRay, 40, 0.1f, enemy.myLM, QueryTriggerInteraction.Collide);
            //Debug.Log(theHits.Length);
            for (int i = 0; i < theHits.Length; i++)
            {
                if (theHits[i].collider.gameObject.tag == "PlayerBullet")
                {
                    //Debug.Log("Found a bullet");
                    return true;
                }
            }

            return false;
        }
    }

    private class IsPlayerInRange : Node<BTEnemy>
    {
        public override bool Update(BTEnemy enemy)
        {
            var playerPos = enemy._player.transform.position;
            var enemyPos = enemy.transform.position;
            return Vector3.Distance(playerPos, enemyPos) < enemy._shootRange;
        }
    }

    ///////////////////
    /// Actions
    ///////////////////
    private class Dodge : Node<BTEnemy>
    {
        public override bool Update(BTEnemy enemy)
        {
            enemy.Strafe();
            return true;
        }
    }

    private class Attack : Node<BTEnemy>
    {
        public override bool Update(BTEnemy enemy)
        {
            enemy.Shoot();
            return true;
        }
    }

    private class Idle : Node<BTEnemy>
    {
        public override bool Update(BTEnemy enemy)
        {
            return true;
        }
    }
}