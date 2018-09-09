using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScrolling : MonoBehaviour
{

	Vector3 positionOffset = new Vector3 (0, 0, 0);
	Vector3 currPos = Vector3.zero, prevPos = Vector3.zero, deltaPos = Vector3.zero;
	[SerializeField]
	float movementRate = 20f;
	[SerializeField]
	float scrollRate = 0.2f;
	[SerializeField]
	RectTransform panel;
	[SerializeField]
	RectTransform canvas;
	[SerializeField]
	RectTransform magnifying;

	Vector2 currTouchA, currTouchB;
	float currExp = 0, prevExp = 0, deltaExp = 0;
	[SerializeField]
	float expRate = 0.0025f;

	Vector2 currPosX = Vector3.zero, prevPosX = Vector3.zero, deltaPosX = Vector3.zero;

	// Update is called once per frame
	void Update ()
	{
		#if UNITY_EDITOR

		if (!(Input.touchCount > 1) && Input.GetKey(KeyCode.LeftControl)) {
			if (Input.GetMouseButtonDown (0)) {

				prevPos = Input.mousePosition;
				Debug.Log (prevPos);

			} else if (Input.GetMouseButton (0)) {

				currPos = Input.mousePosition;

				deltaPos = currPos - prevPos;

				deltaPos.x = deltaPos.x / (float) Screen.width;
				deltaPos.y = deltaPos.y / (float) Screen.height;

				if (deltaPos.magnitude > 0) {
					panel.position += deltaPos * movementRate;
				}

				prevPos = currPos;

			}

			if (Input.mouseScrollDelta != Vector2.zero) {
				float totalScale = Input.mouseScrollDelta.y * scrollRate + magnifying.localScale.x;
				if (totalScale < 0.2f) { 
					totalScale = 0.2f;
				}
				magnifying.localScale = new Vector3 (totalScale, totalScale, magnifying.localScale.z);
			}
			//Touch scaling
		}

		#elif UNITY_ANDROID
		if ((Input.touchCount > 1)) {
			
			currPosX = (Input.GetTouch (0).position + Input.GetTouch (1).position) / 2;

			if (prevPosX == Vector2.zero) {
				prevPosX = currPosX;
			}

			deltaPosX = currPosX - prevPosX;

			deltaPosX.x = deltaPosX.x / (float) Screen.width;
			deltaPosX.y = deltaPosX.y / (float) Screen.height;

			if (deltaPosX.magnitude > 0) {
				panel.position += new Vector3 (deltaPosX.x, deltaPosX.y, 0) * movementRate;
			}

			prevPosX = currPosX;

			DetectTouchMovement.Calculate ();

			float totalScale = DetectTouchMovement.pinchDistanceDelta * expRate + magnifying.localScale.x;
			if (totalScale < 0.5f) {
				totalScale = 0.5f;
			} else if (totalScale > 10f) {
				totalScale = 10f;
			}



			magnifying.localScale = new Vector3 (totalScale, totalScale, magnifying.localScale.z);

			Quaternion desiredRotation = magnifying.rotation;

			Vector3 rotationDeg = Vector3.zero;
			rotationDeg.z = DetectTouchMovement.turnAngleDelta;
			desiredRotation *= Quaternion.Euler (rotationDeg);

			magnifying.rotation = desiredRotation;

		} else {
			prevPosX = Vector2.zero;
		}
		#endif
	}

}
