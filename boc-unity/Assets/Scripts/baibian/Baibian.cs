using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.wuyy {

	public enum BaibianType {
		Tongnian,
		Haiwai,
		Chuguo,
		Chengjia,
		Zinv,
		Wannian,
	}

	public class Baibian : MonoBehaviour {

		public static Baibian instance;

		void Awake() {
			instance = this;
		}

		void OnDestroy() {
			if (instance == this) {
				instance = null;
			}
		}

		public void Change(BaibianType biabian) {
			
		}

	}


}
