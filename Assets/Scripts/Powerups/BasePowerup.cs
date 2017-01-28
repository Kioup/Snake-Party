using System.Collections;
using UnityEngine;

namespace Powerups {
    public class BasePowerup : MonoBehaviour, IPowerup {

        public float Duration;
        public bool IsNegative;

        protected Collider2D Other;

        public void OnTriggerEnter2D(Collider2D other) {
            Other = other;
            StartCoroutine(OnPowerup());
            DisablePowerup();
        }

        public virtual IEnumerator OnPowerup() {
            Debug.Log("Powerup started");
            yield return new WaitForSeconds(Duration);
            Debug.Log("Powerup finished");
            Destroy(gameObject);
        }

        private void DisablePowerup() {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }
}