using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class ButtonClickChangeActive : MonoBehaviour {

		public void OnClick(GameObject go) {
			go.SetActive(!go.activeSelf);
		}

	}


}
