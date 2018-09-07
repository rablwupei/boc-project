using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace wuyy.fk {

	public class JingbaoTime : MonoBehaviour {

		public Text text;

		IEnumerator Start() {
			var time = new WaitForSeconds(1f);
			while (true) {
				//9:30:23 AM. SEPT.17TH 2018 A.D.
				text.text = DateTime.Now.ToString("h:mm:ss tt. MMM.dd\\T\\H yyyy A.D.");
				yield return time;
			}
		}

	}

}

