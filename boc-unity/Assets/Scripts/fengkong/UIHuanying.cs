using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace wuyy.fk {

	public class UIHuanying : UIAbstract {

		public Image fg;

		public void FgClick() {
			fg.DOFade(0f, 1f).OnComplete(delegate {
				Fengkong.instance.Next();	
			});
		}

	}

}
