using System.Collections;
using UnityEngine;

namespace Powerups {
    public class SpeedPowerup : BasePowerup {

        public float SpeedAddition = 1f;
        public float RotationAddition = 30f;

        public override IEnumerator OnPowerup() {
            Debug.Log("Speed powerup started");
            Other.GetComponent<SnakeController>().Speed += SpeedAddition;
            Other.GetComponent<SnakeController>().RotationSpeed += RotationAddition;
            yield return new WaitForSeconds(Duration);
            Other.GetComponent<SnakeController>().Speed -= SpeedAddition;
            Other.GetComponent<SnakeController>().RotationSpeed -= RotationAddition;
            Debug.Log("Speed powerup finished");
            Destroy(gameObject);

        }
    }
}
