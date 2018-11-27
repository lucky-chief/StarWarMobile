using System;
using UnityEngine;

namespace SWEngine
{
	public enum SteerState
	{
		idel,
		steering
	}

    public class SteerAgent : SWObject, IUpdater
    {
		private Transform2D _transform;
		private float _angularSpeed;
		private float _speed;
		private SteerState _steerState;
		private Vector2 _destination;

		private Action _arriveCallback;

		public SteerAgent(Transform2D trans2D)
		{
			_transform = trans2D;
		}

		public float AngularSpeed
		{
			get{
				return _angularSpeed;
			}
			set{
				_angularSpeed = value;
			}
		}

		public float Speed
		{
			get{
				return _speed;
			}
			set{
				_speed = Mathf.Clamp(value,0,float.MaxValue);
			}
		}

		public void SetDestination(Vector2 destnation,Action arriveCallback = null)
		{
			_steerState = SteerState.steering;
			_destination = destnation;
			_arriveCallback = arriveCallback;
		}

		public void StopSteer(bool callArriveCallback = false)
		{
			_steerState = SteerState.idel;
		}

        public void OnUpdate(float deltaTime)
        {
			switch(_steerState)
			{
				case SteerState.idel:
					break;
				case SteerState.steering:
					break;
			}
        }

		public override void Dispose(){
			_transform = null;
			_arriveCallback = null;
		}
    }
}
