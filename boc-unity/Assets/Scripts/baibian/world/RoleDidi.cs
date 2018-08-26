using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class RoleDidi : Role {

		protected override string GetBodyPath(BaibianType type) {
			return "baibian/world/didi_" + type.ToString();
		}
	}

}
