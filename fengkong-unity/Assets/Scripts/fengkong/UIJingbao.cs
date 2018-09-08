using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace wuyy.fk {

	public class UIJingbao : UIAbstract {

		public CanvasGroup[] hideList;

		public void HideClick() {
			if (!_isHide) {
				_isHide = true;
				StartCoroutine(DoHide());
			}
		}

		bool _isHide;

		IEnumerator DoHide() {
			var length = Fengkong.PlaySound("3-1");
			var fadeLength = 2f;

			if (Fengkong.isNormal) {
				yield return new WaitForSeconds(length - fadeLength);
			}

			Tween tween = null;
			foreach (var item in hideList) {
				tween = item.DOFade(0f, fadeLength);
			}
			if (tween != null) {
				yield return tween.WaitForCompletion();
			}

			while (Fengkong.instance.audioSound.isPlaying) {
				yield return null;
			}

			Fengkong.instance.Next();
		}
		
	}


}
