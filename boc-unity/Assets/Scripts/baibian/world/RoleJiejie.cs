using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {
	
	public class RoleJiejie : Role {

		protected override string GetBodyPath(BaibianType type) {
			return "baibian/world/jiejie_" + type.ToString();
		}
	}

}