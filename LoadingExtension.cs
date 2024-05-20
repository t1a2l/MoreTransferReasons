using ICities;
using MoreTransferReasons.Utils;
using UnityEngine;

namespace MoreTransferReasons
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private static GameObject _gameObject1;

        private static GameObject _gameObject2;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            ItemClasses.Register();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            _gameObject1 = new("ExtendedTransferManagerMod");
            _gameObject1.AddComponent<ExtendedTransferManagerComponent>();
            _gameObject2 = new("ExtendedDistrictManager");
            _gameObject2.AddComponent<ExtendedDistrictManagerComponent>();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (_gameObject1 == null && _gameObject2 == null)
            {
                return;
            }
            Object.Destroy(_gameObject1);
            Object.Destroy(_gameObject2);
            _gameObject1 = null;
            _gameObject2 = null;
        } 

        public override void OnReleased()
        {
            base.OnReleased();
            ItemClasses.Unregister();
        }
    }
}