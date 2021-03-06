﻿using System;
using UnityEngine;

namespace SWEngine
{
	public enum SteerState
	{
		idle,
		steering
	}

	public enum SteerFace
	{
		None,
		FaceUp,
		FaceDown,
		FaceBLU,
		FaceBRU,
		FaceBLD,
		FaceBRD
	}

    public class SteerAgent : SWObject, IUpdater
    {
		private Transform2D _transform;
		private float _speed;
		private SteerState _steerState;
		private SteerFace _steerFace;
		private Vector2 _destination;
		private float _arriveThreshold = 0.01f;

		private Action _arriveCallback;
		private Action<SteerFace> _onSteerFaceChange;

		public SteerAgent(Transform2D trans2D)
		{
			_transform = trans2D;
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

		public SteerFace Face
		{
			get{
				return _steerFace;
			}
		}

		public float ArriveThresHold
		{
			get{
				return _arriveThreshold;
			}
			set{
				_arriveThreshold = Mathf.Clamp(value,0,float.MaxValue);
			}
		}

		public Action arriveCallback{
			get{
				return _arriveCallback;
			}
			set{
				_arriveCallback = value;
			}
		}

		public Action<SteerFace> OnSteerFaceChange{
			get{
				return _onSteerFaceChange;
			}
			set{
				_onSteerFaceChange = value;
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
			_steerState = SteerState.idle;
		}

        public void OnUpdate(float deltaTime)
        {
			switch(_steerState)
			{
				case SteerState.idle:
					break;
				case SteerState.steering:
					Vector2 dir = (_destination - _transform.Position).normalized;
					Rotate(deltaTime,dir);
					Move(deltaTime,dir);
					break;
			}
        }

		public override void Dispose(){
			_transform = null;
			_arriveCallback = null;
		}

		protected bool Arrive()
		{
			return Vector2.Distance(_transform.Position,_destination) <= _arriveThreshold;
		}

		protected void Move(float deltaTime,Vector2 dir)
		{
			if(Arrive())
			{
				_steerState = SteerState.idle;
				if(_arriveCallback != null)
				{
					_arriveCallback.Invoke();
				}
				return;
			}
			_transform.Position += dir * _speed * deltaTime;
		}

		protected void SetSteerFace(SteerFace face)
		{
			if(_steerFace != face)
			{
				_steerFace = face;
				if(_onSteerFaceChange != null)
				{
					_onSteerFaceChange.Invoke(face);
				}
			}
		}

		protected void Rotate(float deltaTime,Vector2 dir)
		{
			Vector3 cross = Vector3.Cross( _transform.Forword,dir);
			float angleTarget = Vector2.Angle(_transform.Forword,dir);
			bool faceUp = Vector2.Dot(dir,Vector2.up) >= 0;

			if(cross.z < 0)
			{
				// 右边
				if(faceUp)
				{
					if(angleTarget <= 45)
					{
						SetSteerFace(SteerFace.FaceUp);
					}
					else if(angleTarget <= 90)
					{
						SetSteerFace(SteerFace.FaceBRU);
					}
					else if(angleTarget < 135)
					{
						SetSteerFace(SteerFace.FaceBRD);
					}
					else
					{
						SetSteerFace(SteerFace.FaceDown);
					}
				}
				else
				{
					if(angleTarget <= 45)
					{
						SetSteerFace(SteerFace.FaceDown);
					}
					else if(angleTarget <= 90)
					{
						SetSteerFace(SteerFace.FaceBRD);
					}
					else if(angleTarget < 135)
					{
						SetSteerFace(SteerFace.FaceBRD);
					}
					else
					{
						SetSteerFace(SteerFace.FaceDown);
					}
				}
			}
			else
			{
				if(faceUp)
				{
					if(angleTarget <= 45)
					{
						SetSteerFace(SteerFace.FaceUp);
					}
					else if(angleTarget <= 90)
					{
						SetSteerFace(SteerFace.FaceBLU);
					}
					else if(angleTarget < 135)
					{
						SetSteerFace(SteerFace.FaceBLD);
					}
					else
					{
						SetSteerFace(SteerFace.FaceDown);
					}
				}
				else
				{
					if(angleTarget <= 45)
					{
						SetSteerFace(SteerFace.FaceDown);
					}
					else if(angleTarget <= 90)
					{
						SetSteerFace(SteerFace.FaceBLD);
					}
					else if(angleTarget < 135)
					{
						SetSteerFace(SteerFace.FaceBLD);
					}
					else
					{
						SetSteerFace(SteerFace.FaceDown);
					}
				}
			}
		}
    }
}
