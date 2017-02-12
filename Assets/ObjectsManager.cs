using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager_Basic
{
    public interface IManaged
    {
        void OnManWithdraw();
        void OnManReturn();
    }

    public abstract class Manager<T> where T :IManaged
    {
        protected readonly List<T> ManagedObjects = new List<T>();

        public abstract T WithdrawFromManager();

        public abstract void ReturnToManager(T o);

        public T Find(Predicate<T> predicate)
        {
            return ManagedObjects.Find(predicate);
        }

        public List<T> FindAll (Predicate<T> predicate)
        {
            return ManagedObjects.FindAll(predicate);
        }
    }

    public enum EnemiesTypes { type1, type2 }

    public class Enemy: IManaged
    {
        public EnemiesTypes myType { get; private set; }

        public void Init (EnemiesTypes type)
        {
            myType = type;
        }

        public void OnManWithdraw()
        {
            Debug.Log("Created " + myType + " enemy");
        }
        public void OnManReturn()
        {
            Debug.Log("Destroyed " + myType + " enemy");
        }
    }

    public class Enemies : Manager<Enemy>
    {
        public override Enemy WithdrawFromManager()
        {
            var enemy = new Enemy();
            enemy.Init(EnemiesTypes.type1);
            ManangedObjects.Add(enemy);
            enemy.OnManWithdraw();
            return enemy;
        }

        public override Enemy ReturnToManager(Enemy enemy)
        {
            ManagedObjects.Remove(enemy);
            enemy.OnManReturn();
        }

        public List<Enemy> Create (uint n)
        {
            var enemies = new List<Enemy>();
            for (int i = 0; i < n; i++)
            {
                enemies.Add(WithdrawFromManager());
            }
            return enemies;
        }
    }
}
