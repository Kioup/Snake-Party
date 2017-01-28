using UnityEngine;

namespace Powerups {
    public interface IPowerup {
        void OnTriggerEnter2D(Collider2D other);
    }
}