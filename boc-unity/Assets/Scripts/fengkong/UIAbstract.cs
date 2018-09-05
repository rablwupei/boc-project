using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy.fk {

	public class UIAbstract : MonoBehaviour {

		public virtual void Init() {
			
		}

		public void Next() {
			Fengkong.instance.Next();
		}

	}


}
