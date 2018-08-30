using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace wuyy {


	public class UIHuanying : MonoBehaviour {

		public List<DOTweenAnimation> tweenAnims;

		public void Close() {
			DOTweenAnimation anim = null;
			foreach (var item in tweenAnims) {
				item.DOPlayForward();
				anim = item;
			}
			if (anim) {
				anim.tween.OnComplete(delegate {
					gameObject.SetActive(false);
				});
			}
		}

		public void Open() {
			gameObject.SetActive(true);
			foreach (var item in tweenAnims) {
				item.DOPlayBackwards();
			}
		}

	}

}