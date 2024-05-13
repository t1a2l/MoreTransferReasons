using ICities;
using MoreTransferReasons.Utils;
using UnityEngine;

namespace MoreTransferReasons
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private static GameObject _gameObject;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            ItemClasses.Register();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            _gameObject = new("ExtendedTransferManagerMod");
            _gameObject.AddComponent<ExtendedTransferManagerComponent>();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (_gameObject == null)
            {
                return;
            }
            Object.Destroy(_gameObject);
            _gameObject = null;
        } 

        public override void OnReleased()
        {
            base.OnReleased();
            ItemClasses.Unregister();
        }
    }
}