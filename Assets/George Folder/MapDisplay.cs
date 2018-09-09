using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour {

	[SerializeField]
	Sprite newImage;
	[SerializeField]
	RectTransform panelTransform;
	[SerializeField]
	Image imageReference;
	[SerializeField]
	float permanentWidth = 800f;
	[SerializeField]
	bool debubg = false;

	void Update() {

		if (debubg) {
			ChangeImage(newImage);
			debubg = false;
		}
		
	}

	void ChangeImage(Sprite image) {

		panelTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, permanentWidth);
		panelTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, permanentWidth * ((float) newImage.texture.height / (float) newImage.texture.width));
		imageReference.sprite = newImage;

	}

}
