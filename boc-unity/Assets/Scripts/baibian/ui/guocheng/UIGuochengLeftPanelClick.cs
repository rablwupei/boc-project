using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace wuyy {

	public class UIGuochengLeftPanelClick : MonoBehaviour {
		public DOTweenAnimation[] tweens;

		void OnDisable() {
			if (_open) {
				OnBackClick();
				foreach (var tween in tweens) {
					tween.DOComplete();
				}
			}
		}

		bool _open;
		public void OnBackClick() {
			foreach (var tween in tweens) {
				if (_open) {
					tween.DOPlayBackwards();
				} else {
					tween.DOPlayForward();
				}
			}
			_open = !_open;
		}

	}

}
