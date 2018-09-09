using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnPointer : MonoBehaviour {

	[SerializeField]
	GameObject pointer;

	[SerializeField]
	Transform scaleObject;

	[SerializeField]
	GraphicRaycaster caster;

	[SerializeField]
	float timer = 0.2f;
	float currTime = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0)) {

			currTime += Time.deltaTime;

		}

		if (Input.GetMouseButtonUp(0)) {

			if (currTime < timer) {
				PointerEventData ped = new PointerEventData (null);
				ped.position = Input.mousePosition;
				List<RaycastResult> results = new List<RaycastResult> ();
				caster.Raycast (ped, results);
				Debug.Log (results [0].screenPosition);
				Vector3 point = Camera.main.ScreenToWorldPoint (results [0].screenPosition);
				point.z = results [0].distance;
				Instantiate (pointer, point, transform.rotation, scaleObject);
			}

			currTime = 0;
		}

	}

}
