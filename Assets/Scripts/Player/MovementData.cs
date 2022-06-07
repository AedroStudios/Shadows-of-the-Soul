using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Movement Data")]
public class MovementData : ScriptableObject
{
	//PHYSICS
	[Header("Gravity")]
	public float gravityScale; //overrides rb.gravityScale
	public float fallGravityMult;
	public float quickFallGravityMult;

	[Header("Drag")]
	public float dragAmount; //drag in air
	public float frictionAmount; //drag on ground

	[Header("Other Physics")]
	[Range(0, 0.5f)] public float coyoteTime; //grace time to Jump after the player has fallen off a platformer


	//GROUND
	[Header("Run")]
	public float runMaxSpeed;
	public float runAcceleration;
	public float runDecceleration;
	[Range(0, 1)] public float accelerationInAir;
	[Range(0, 1)] public float deccelerationInAir;
	[Space(5)]
	[Range(.5f, 2f)] public float accelerationPower;   
	[Range(.5f, 2f)] public float stopPower;
	[Range(.5f, 2f)] public float turnPower;


	//JUMP
	[Header("Jump")]
	public float jumpForce;
	[Range(0, 1)] public float jumpCutMultiplier;
	[Space(10)]
	[Range(0, 0.5f)] public float jumpBufferTime; //time after pressing the jump button where if the requirements are met a jump will be automatically performed

	//OTHER
	[Header("Other Settings")]
	public bool doKeepRunMomentum; //player movement will not decrease speed if above maxSpeed, letting only drag do so. Allows for conservation of momentum
}

//Think a setting should be renamed or added? Reach out to me @DawnosaurDev
