using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWEngine;

public class SteerAgentTester : MonoBehaviour {
	public Transform target;
	public float speed;
	public SteerFace face;


	private Transform2D transform2D;
	private Transform2D transform2DTarget;
	private SteerAgent agent;

	// Use this for initialization
	void Start () {
		transform2D = new Transform2D(transform);
		transform2DTarget = new Transform2D(target);
		agent = new SteerAgent(transform2D);
	}
	
	// Update is called once per frame
	void Update () {
		agent.Speed = speed;
		agent.SetDestination(transform2DTarget.Position);
		agent.OnUpdate(Time.deltaTime);
		face = agent.Face;
	}
}
