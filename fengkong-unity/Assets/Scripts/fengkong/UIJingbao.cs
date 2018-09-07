using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace wuyy.fk {

	public class UIJingbao : UIAbstract {

		public CanvasGroup[] hideList;

		public void HideClick() {
			if (!_isHide) {
				StartCoroutine(DoHide());
			}
		}

		bool _isHide;

		IEnumerator DoHide() {
			_isHide = true;
			Tween tween = null;
			foreach (var item in hideList) {
				tween = item.DOFade(0f, 0.5f);
			}
			if (tween != null) {
				yield return tween.WaitForCompletion();
			}
			Fengkong.instance.Next();
		}
		
	}


}
