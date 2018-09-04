using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class Const {

//        public class Layer {
//            public const int Default = 0;
//            public const int Ignore = 2;
//            public const int UI = 5;
//
//			public const int WorldTouch = 8;
//			public const int BgShouye = 9;
//        }

        public class Mask {
            public const int Default = 1;
            public const int Ignore = 1 << 2;
            public const int UI = 1 << 5;

			public const int Shouji = 1 << 8;
			public const int BgShouye = 1 << 9;
			public const int Didi = 1 << 10;
			public const int Jiejie = 1 << 11;
        }

    }

}