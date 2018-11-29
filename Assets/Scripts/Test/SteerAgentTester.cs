using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWEngine;
using DragonBones;

public class SteerAgentTester : MonoBehaviour {
	public UnityEngine.Transform target;
	public float speed;
	public SteerFace face;
	public UnityArmatureComponent dbComp;


	private Transform2D transform2D;
	private Transform2D transform2DTarget;
	private SteerAgent agent;
	private SWEngine.Animation anim;

	// Use this for initialization
	void Start () {
		transform2D = new Transform2D(transform);
		transform2DTarget = new Transform2D(target);
		agent = new SteerAgent(transform2D);
		anim = new SWEngine.Animation(dbComp);

		agent.OnSteerFaceChange += OnSteerFaceChange;
	}

	void OnSteerFaceChange(SteerFace face)
	{
		switch(face)
		{
			case SteerFace.FaceUp:
				anim.PlayAnimation(SoldierAnimationState.WalkUp,true);
				break;
			case SteerFace.FaceDown:
				anim.PlayAnimation(SoldierAnimationState.WalkDown,true);
				break;
			case SteerFace.FaceBLU:
				anim.PlayAnimation(SoldierAnimationState.WalkBLU,true);
				break;
			case SteerFace.FaceBRU:
				anim.PlayAnimation(SoldierAnimationState.WalkBRU,true);
				break;
			case SteerFace.FaceBLD:
				anim.PlayAnimation(SoldierAnimationState.WalkBLD,true);
				break;
			case SteerFace.FaceBRD:
				anim.PlayAnimation(SoldierAnimationState.WalkBRD,true);
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		agent.Speed = speed;
		agent.SetDestination(transform2DTarget.Position);
		agent.OnUpdate(Time.deltaTime);
		face = agent.Face;
	}
}
