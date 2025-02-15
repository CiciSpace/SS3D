using System;
using SS3D.Core;
using SS3D.Core.Behaviours;
using SS3D.Systems.Health;
using SS3D.Systems.Screens;
using UnityEngine;

namespace SS3D.Systems.Entities.Humanoid
{
    /// <summary>
    /// Controls the movement for living biped characters that use the same armature
    /// as the human model uses.
    /// </summary>
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(HumanoidAnimatorController))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class HumanoidLivingController : HumanoidController
    {

        [Header("Components")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private StaminaController _staminaController;
        
        /// <summary>
        /// Executes the movement code and updates the IK targets
        /// </summary>
        protected override void ProcessCharacterMovement()
        {
            ProcessPlayerInput();

            _characterController.Move(Physics.gravity);

            if (Input.magnitude != 0)
            {
                MoveMovementTarget(Input);
                RotatePlayerToMovement();
                MovePlayer();
            }
            else
            {
                MovePlayer();
                MoveMovementTarget(Vector2.zero, 5);
            }
        }

        protected override float FilterSpeed()
        {
            return IsRunning && _staminaController.CanContinueInteraction ? RunAnimatorValue : WalkAnimatorValue;
        }

        /// <summary>
        /// Moves the player to the target movement
        /// </summary>
        protected override void MovePlayer()
        {
            _characterController.Move(TargetMovement * ((_movementSpeed) * Time.deltaTime));
        }
    }

}