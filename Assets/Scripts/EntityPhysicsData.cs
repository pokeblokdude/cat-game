using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newEntityPhysicsData", menuName="Data/EntityPhysicsData")]
public class EntityPhysicsData : ScriptableObject {
    
    [Header("Physics")]
    public float groundDrag = 10;
    public float airDrag = 5;

    [Header("Movement")]
    public float moveSpeed = 5;
    public float airMoveSpeedMultiplier = 0.4f;
    public float jumpForce = 5;
}
