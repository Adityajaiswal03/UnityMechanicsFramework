// Author: Aditya Jaiswal, Atharv S. Jain
using UnityEngine;

namespace GameplayMechanicsUMFOSS.Physics
{
    /// <summary>
    /// Defines a physics abstraction for 2D and 3D movement systems.
    /// </summary>
    public interface IPhysicsAdapter
    {
        /// <summary>
        /// Applies force using the engine default force mode.
        /// </summary>
        /// <param name="force">Force vector to apply.</param>
        void AddForce(Vector2 force);

        /// <summary>
        /// Applies force using the provided 2D force mode.
        /// </summary>
        /// <param name="force">Force vector to apply.</param>
        /// <param name="mode">How the force is applied.</param>
        void AddForce(Vector2 force, ForceMode2D mode);

        /// <summary>
        /// Sets the current velocity.
        /// </summary>
        /// <param name="velocity">Velocity to assign.</param>
        void SetVelocity(Vector2 velocity);

        /// <summary>
        /// Gets the current velocity.
        /// </summary>
        /// <returns>The current velocity vector.</returns>
        Vector2 GetVelocity();

        /// <summary>
        /// Sets the gravity scale behavior.
        /// </summary>
        /// <param name="scale">Desired gravity scale value.</param>
        void SetGravityScale(float scale);

        /// <summary>
        /// Gets the gravity scale behavior value.
        /// </summary>
        /// <returns>The configured gravity scale value.</returns>
        float GetGravityScale();

        /// <summary>
        /// Checks if the object is grounded.
        /// </summary>
        /// <param name="checkOrigin">World-space origin of the grounded check.</param>
        /// <param name="checkRadius">Radius of the grounded check.</param>
        /// <param name="groundLayer">Layer mask to test for ground colliders.</param>
        /// <returns>True when ground is detected; otherwise false.</returns>
        bool IsGrounded(Vector2 checkOrigin, float checkRadius, LayerMask groundLayer);
    }
}
