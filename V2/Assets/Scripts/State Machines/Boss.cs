using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IShootable
{
    private FSM<Boss> _fsm;
    [SerializeField] private int health;
    [SerializeField] private float speed;

    private class BossState : FSM<Boss>.State
    {

    }

    private class Seeking : BossState
    {
        private Vector3 playerLoc;
        private MobManager myMM;

        public override void Init()
        {
            myMM = GameObject.Find("Managers").GetComponent<MobManager>();
        }

        public override void Update()
        {
            playerLoc = GameObject.FindGameObjectWithTag("Player").transform.position;
            Context.MoveToDestination(playerLoc, Context.speed);
            if (Vector3.Distance(Context.transform.position, playerLoc) <= 10)
            {
                if (myMM.NumOfType(ManagedObjectTypes.enemyType1) + myMM.NumOfType(ManagedObjectTypes.enemyType2) > 0)
                    TransitionTo<Attack>();
                else
                    TransitionTo<AttackPrep>();
            }
        }
    }

    private class AttackPrep : BossState
    {
        private int waveNum;
        private MobManager myMM;

        private float range = 10f;

        private float timer;

        public override void Init()
        {
            waveNum = 0;
            myMM = GameObject.Find("Managers").GetComponent<MobManager>();
            timer = 0;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (waveNum >= 5)
            {
                TransitionTo<Attack>();
            }

            if (timer > 1)
            {
                timer -= 1;
                myMM.Make(Context.transform.position + Vector3.forward * range, "Enemy");
                myMM.Make(Context.transform.position + Vector3.forward * -range, "Enemy");
                myMM.Make(Context.transform.position + Vector3.right * range, "Enemy");
                myMM.Make(Context.transform.position + Vector3.right * -range, "Enemy");
                waveNum++;
            }
        }
    }

    private class Attack : BossState
    {
        private float pauseTime;
        private Vector3 playerPos;

        public override void Init()
        {
            pauseTime = 0;
        }

        public override void OnEnter()
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        public override void Update()
        {
            pauseTime += Time.deltaTime;
            if (pauseTime >= 2)
            {
                Context.MoveToDestination(playerPos, Context.speed * 2);
                if (Vector3.Distance(Context.transform.position, playerPos) <= .15f)
                {
                    TransitionTo<Seeking>();
                }
            }
        }
    }

    private class Flee : BossState
    {
        private Vector3 fleeSpot;

        public override void OnEnter()
        {
            //fleeSpot = Vector3.Normalize(Context.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position) * 5 + Context.transform.position;   
            fleeSpot = new Vector3(Random.Range(-45, 45), 0, Random.Range(-25, 25));
        }

        public override void Update()
        {
            Context.MoveToDestination(fleeSpot, Context.speed * 3);
            if (Vector3.Distance(Context.transform.position, fleeSpot) < .05f)
            {
                TransitionTo<Seeking>();
            }
        }
    }

    private void Start()
    {
        _fsm = new FSM<Boss>(this);

        _fsm.TransitionTo<Seeking>();
    }

    private void Update()
    {
        _fsm.Update();
    }

    private void MoveToDestination (Vector3 dest, float spd)
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
    }

    public void OnShoot(int dmg)
    {
        health -= dmg;
        _fsm.TransitionTo<Flee>();
    }
}
