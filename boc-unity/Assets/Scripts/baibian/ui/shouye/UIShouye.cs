using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class UIShouye : MonoBehaviour {

		public void ButtonClick(int type) {
			gameObject.SetActive(false);
			Baibian.instance.Change(type, true);
		}

	}

}
