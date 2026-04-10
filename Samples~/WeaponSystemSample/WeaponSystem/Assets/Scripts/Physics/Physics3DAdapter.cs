// Author: Aditya Jaiswal, Atharv S. Jain
using UnityEngine;

namespace GameplayMechanicsUMFOSS.Physics
{
    /// <summary>
    /// 3D physics adapter implementation using Rigidbody. Unity Rigidbody has no gravityScale property.
    /// This adapter supports on/off gravity only. Fractional scales are stored but do not affect physics simulation.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Physics3DAdapter : MonoBehaviour, IPhysicsAdapter
    {
        private const float ZERO_FLOAT = 0f;
        private const float Z_AXIS_FORCE = 0f;

        private Rigidbody rb;
        private float gravityScaleValue;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Applies force using the default 3D force mode.
        /// </summary>
        /// <param name="force">2D force vector mapped to XY axes.</param>
        public void AddForce(Vector2 force)
        {
            rb.AddForce(new Vector3(force.x, force.y, Z_AXIS_FORCE));
        }

        /// <summary>
        /// Applies force after mapping <see cref="ForceMode2D"/> to <see cref="ForceMode"/>.
        /// </summary>
        /// <param name="force">2D force vector mapped to XY axes.</param>
        /// <param name="mode">2D mode to map into a 3D mode.</param>
        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            var mappedMode = mode == ForceMode2D.Impulse ? ForceMode.Impulse : ForceMode.Force;
            rb.AddForce(new Vector3(force.x, force.y, Z_AXIS_FORCE), mappedMode);
        }

        /// <summary>
        /// Sets the rigidbody velocity on XY and keeps Z at zero.
        /// </summary>
        /// <param name="velocity">Velocity to assign.</param>
        public void SetVelocity(Vector2 velocity)
        {
            rb.linearVelocity = new Vector3(velocity.x, velocity.y, Z_AXIS_FORCE);
        }

        /// <summary>
        /// Gets the rigidbody velocity projected into 2D.
        /// </summary>
        /// <returns>The current XY velocity.</returns>
        public Vector2 GetVelocity()
        {
            return new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }

        /// <summary>
        /// Stores gravity scale intent and toggles Rigidbody gravity on/off.
        /// </summary>
        /// <param name="scale">Desired gravity scale intent value.</param>
        public void SetGravityScale(float scale)
        {
            gravityScaleValue = scale;
            rb.useGravity = scale != ZERO_FLOAT;
        }

        /// <summary>
        /// Gets the stored gravity scale intent value.
        /// </summary>
        /// <returns>The stored gravity scale value.</returns>
        public float GetGravityScale()
        {
            return gravityScaleValue;
        }

        /// <summary>
        /// Checks grounded state using a sphere query projected at Z = 0.
        /// </summary>
        /// <param name="checkOrigin">World-space origin for the query.</param>
        /// <param name="checkRadius">Radius of the grounded query.</param>
        /// <param name="groundLayer">Layers considered as ground.</param>
        /// <returns>True when a ground collider is found.</returns>
        public bool IsGrounded(Vector2 checkOrigin, float checkRadius, LayerMask groundLayer)
        {
            return UnityEngine.Physics.CheckSphere(new Vector3(checkOrigin.x, checkOrigin.y, Z_AXIS_FORCE), checkRadius, groundLayer);
        }
    }
}
