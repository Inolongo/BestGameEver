using System;
using UnityEngine;
using static UnityEngine.Debug;


namespace DefaultNamespace
{
    public abstract class DialogReactions : MonoBehaviour
    {
        [SerializeField]
        protected float changedHealthPoints; //- if damage + if heal

        public abstract void StartReaction();
        public  bool IsAngry;
    }
}