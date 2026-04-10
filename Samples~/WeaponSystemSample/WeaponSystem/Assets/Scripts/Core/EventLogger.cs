// Author: Aditya Jaiswal, Atharv S. Jain
using UnityEngine;
using GameplayMechanicsUMFOSS.Combat;
using GameplayMechanicsUMFOSS.Core;

namespace GameplayMechanicsUMFOSS.Samples.WeaponSystem
{
    /// <summary>
    /// Demo-only listener that subscribes to every WeaponEventBus channel and
    /// logs each event to the Unity console. Drop this on any GameObject in
    /// the demo scene to verify that publish/subscribe is wired correctly
    /// without needing UI elements. Lives in Samples~ so it never ships with
    /// the runtime package.
    /// </summary>
    public class EventLogger : MonoBehaviour
    {
        [SerializeField] private bool logEquipEvents = true;
        [SerializeField] private bool logFireEvents = true;
        [SerializeField] private bool logHitEvents = true;
        [SerializeField] private bool logReloadEvents = true;
        [SerializeField] private bool logAmmoEvents = true;
        [SerializeField] private bool logChargeEvents = false;

        private void OnEnable()
        {
            WeaponEventBus.OnWeaponEquipped(HandleWeaponEquipped);
            WeaponEventBus.OnWeaponUnequipped(HandleWeaponUnequipped);
            WeaponEventBus.OnWeaponFired(HandleWeaponFired);
            WeaponEventBus.OnWeaponHit(HandleWeaponHit);
            WeaponEventBus.OnWeaponReloadStart(HandleReloadStart);
            WeaponEventBus.OnWeaponReloadComplete(HandleReloadComplete);
            WeaponEventBus.OnAmmoChanged(HandleAmmoChanged);
            WeaponEventBus.OnChargeChanged(HandleChargeChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<WeaponEquippedEvent>(HandleWeaponEquipped);
            EventBus.Unsubscribe<WeaponUnequippedEvent>(HandleWeaponUnequipped);
            EventBus.Unsubscribe<WeaponFiredEvent>(HandleWeaponFired);
            EventBus.Unsubscribe<WeaponHitEvent>(HandleWeaponHit);
            EventBus.Unsubscribe<WeaponReloadStartEvent>(HandleReloadStart);
            EventBus.Unsubscribe<WeaponReloadCompleteEvent>(HandleReloadComplete);
            EventBus.Unsubscribe<AmmoChangedEvent>(HandleAmmoChanged);
            EventBus.Unsubscribe<ChargeChangedEvent>(HandleChargeChanged);
        }

        private void HandleWeaponEquipped(WeaponEquippedEvent evt)
        {
            if (!logEquipEvents) return;
            Debug.Log($"[EventLogger] Equipped: {evt.data.name} (dmg {evt.data.damage}, range {evt.data.range})");
        }

        private void HandleWeaponUnequipped(WeaponUnequippedEvent evt)
        {
            if (!logEquipEvents) return;
            Debug.Log($"[EventLogger] Unequipped: {evt.data.name}");
        }

        private void HandleWeaponFired(WeaponFiredEvent evt)
        {
            if (!logFireEvents) return;
            Debug.Log($"[EventLogger] Fired: {evt.data.name}");
        }

        private void HandleWeaponHit(WeaponHitEvent evt)
        {
            if (!logHitEvents) return;
            string targetName = evt.hitData.hitObject != null ? evt.hitData.hitObject.name : "<null>";
            Debug.Log($"[EventLogger] Hit: {targetName} for {evt.hitData.damage} dmg @ {evt.hitData.hitPoint}");
        }

        private void HandleReloadStart(WeaponReloadStartEvent evt)
        {
            if (!logReloadEvents) return;
            Debug.Log($"[EventLogger] Reload start: {evt.data.name}");
        }

        private void HandleReloadComplete(WeaponReloadCompleteEvent evt)
        {
            if (!logReloadEvents) return;
            Debug.Log($"[EventLogger] Reload complete: {evt.data.name}");
        }

        private void HandleAmmoChanged(AmmoChangedEvent evt)
        {
            if (!logAmmoEvents) return;
            Debug.Log($"[EventLogger] Ammo: {evt.current} / {evt.reserve}");
        }

        private void HandleChargeChanged(ChargeChangedEvent evt)
        {
            if (!logChargeEvents) return;
            Debug.Log($"[EventLogger] Charge: {evt.percent:P0}");
        }
    }
}
