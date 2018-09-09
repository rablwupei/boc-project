using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace wuyy.fk {

	public class JielunAnim : MonoBehaviour {

		void Start() {
			var images = GetComponentsInChildren<Image>();
			images[0].transform.DOScale(new Vector3(1f, 1f), 0.5f).WaitForCompletion();
			images[1].DOFillAmount(1f, 0.5f);
		}

	}

}
