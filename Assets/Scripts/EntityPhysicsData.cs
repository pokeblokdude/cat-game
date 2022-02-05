using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newEntityPhysicsData", menuName="Data/EntityPhysicsData")]
public class EntityPhysicsData : ScriptableObject {
    
    public float gravity = 20;

    [Header("Movement")]
    public float moveSpeed = 5;
    public float jumpForce = 5;
}
