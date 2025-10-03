using ICities;
using MoreTransferReasons.Utils;
using UnityEngine;

namespace MoreTransferReasons
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private static GameObject _gameObject1;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            ItemClasses.Register();
            _gameObject1 = new("ExtendedTransferManagerMod");
            _gameObject1.AddComponent<ExtendedTransferManagerComponent>();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
        } 

        public override void OnReleased()
        {
            base.OnReleased();
            ItemClasses.Unregister();
            if (_gameObject1 == null)
            {
                return;
            }
            Object.Destroy(_gameObject1);
            _gameObject1 = null;
        }
    }
}