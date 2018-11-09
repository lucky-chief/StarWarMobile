using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas2D : MonoBehaviour {
	public enum FixType
	{
		strench,
		fixWidth,
		fixHeight,
	}

	[SerializeField]
	private Camera renderCamera;
	[SerializeField]
	private Vector2 screenReslusion = new Vector2(720,1280);
	[SerializeField]
	private int pixelsPerUnit = 100;
	[SerializeField]
	private FixType fixType;

	private Vector2 standarSize;

	void Awake()
	{
		FixScreen();
	}

	public void FixScreen()
	{
		if(renderCamera != null)
		{
			renderCamera.orthographicSize = (float)Screen.height / pixelsPerUnit * 0.5f;
			standarSize = new Vector2(
				(float)screenReslusion.x / pixelsPerUnit * 0.5f,
				(float)screenReslusion.y / pixelsPerUnit * 0.5f);
			switch(fixType)
			{
				case FixType.fixHeight:
					FixHeight();
					break;
				case FixType.fixWidth:
					FixWidth();
					break;
				case FixType.strench:
					Strench();
					break;
			}
		}
	}

	void Strench()
	{
		float scaleY = ((float)Screen.height / pixelsPerUnit * 0.5f)/standarSize.y;
		float scaleX = ((float)Screen.width / pixelsPerUnit * 0.5f)/standarSize.x;
		transform.localScale = new Vector3(scaleX,scaleY,1);
	}

	void FixHeight()
	{
		float scaleY = ((float)Screen.height / pixelsPerUnit * 0.5f)/standarSize.y;
		transform.localScale = new Vector3(scaleY,scaleY,1);
	}

	void FixWidth()
	{
		float scaleX = ((float)Screen.width / pixelsPerUnit * 0.5f)/standarSize.x;
		transform.localScale = new Vector3(scaleX,scaleX,1);
	}
}
