using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public static class ExtensionsMethods {
        public static Color Random(ref List<Color> colors) {
            var i = UnityEngine.Random.Range(0, colors.Count);
            var result = colors[i];
            colors.RemoveAt(i);
            return result;
        }

        public static Transform Random(ref List<Transform> transforms) {
            var i = UnityEngine.Random.Range(0, transforms.Count);
            var result = transforms[i];
            transforms.RemoveAt(i);
            return result;
        }
    }
}