using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIAbstract : MonoBehaviour {

		public UIPercentTouch percentTouch {
			get {
				return Fengkong.instance.percentTouch;
			}
		}

		public AudioSource audioSound {
			get {
				return Fengkong.instance.audioSound;
			}
		}

		public virtual void Init() {
			
		}

		public void Next() {
			Fengkong.instance.Next();
		}

	}


}
