﻿using A2DK.Phys;
#if UNITY_EDITOR
using ASK.Editor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    public class SwingGrappleBehavior : MonoBehaviour, IGrappleable
    {
        private PhysObj _myPhysObj;
        [SerializeField] private UnityEvent onAttachGrapple;
        [SerializeField] private bool useAnchor;

        #if UNITY_EDITOR
        [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And,
            nameof(useAnchor))]
        [SerializeField]
        #endif
        private Transform anchor;

        #if UNITY_EDITOR
        [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And,
            nameof(useAnchor))]
        [SerializeField]
        #endif
        private Vector2 anchorOffset;
        
        void Awake()
        {
            _myPhysObj = GetComponent<PhysObj>();
        }

        public (Vector2 curPoint, IGrappleable attachedTo, GrappleapleType grappleType) AttachGrapple(Actor grappler,
            Vector2 rayCastHit)
        {
            onAttachGrapple?.Invoke();
            
            return (GetGrapplePos(rayCastHit), this, GrappleapleType.SWING);
        }
        public Vector2 ContinuousGrapplePos(Vector2 grapplePos, Actor grapplingActor) => GetGrapplePos(grapplePos);

        public Vector2 GetGrapplePos(Vector2 origPos)
        {
            if (useAnchor)
            {
                origPos = (Vector2)anchor.position + anchorOffset;
            }

            return origPos;
        }
        
        public PhysObj GetPhysObj() => _myPhysObj;

        public void DetachGrapple() {}
    }
}