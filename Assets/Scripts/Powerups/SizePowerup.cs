using System.Collections;
using UnityEngine;

namespace Powerups {
    public class SizePowerup : BasePowerup {

        public float SizeAddition = 1f;

        public override IEnumerator OnPowerup() {
            Debug.Log("Size powerup started");
            var tail = Other.transform.parent.FindChild("Tail").GetComponent<LineRenderer>();
//            tail.startWidth += SizeAddition;
//            tail.endWidth += SizeAddition;

            tail.widthMultiplier += IsNegative ? -SizeAddition : SizeAddition;
            yield return new WaitForSeconds(Duration);
            tail.widthMultiplier -= IsNegative ? -SizeAddition : SizeAddition;
//            tail.startWidth -= SizeAddition;
//            tail.endWidth -= SizeAddition;
            Debug.Log("Size powerup finished");
            Destroy(gameObject);

        }
    }
}
