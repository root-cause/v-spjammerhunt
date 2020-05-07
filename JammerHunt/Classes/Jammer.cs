using GTA;
using GTA.Math;
using GTA.Native;

namespace JammerHunt.Classes
{
    public class Jammer
    {
        #region Properties
        public Vector3 Position { get; private set; } = Vector3.Zero;
        public Vector3 Rotation { get; private set; } = Vector3.Zero;

        public bool IsDestroyed
        {
            get
            {
                return _isDestroyed;
            }

            set
            {
                _isDestroyed = value;

                if (_blip != null)
                {
                    Function.Call(Hash._0x74513EA3E505181E, _blip.Handle, value); // SHOW_TICK_ON_BLIP
                }
            }
        }
        
        public int PropHandle
        {
            get
            {
                return _prop == null || !_prop.Exists() ? 0 : _prop.Handle;
            }
        }
        #endregion

        #region Fields
        private bool _isDestroyed = false;
        private Prop _prop = null;
        private Blip _blip = null;
        #endregion

        #region Constructor
        public Jammer(Vector3 position, Vector3 rotation, bool isDestroyed)
        {
            Position = position;
            Rotation = rotation;
            _isDestroyed = isDestroyed;
        }
        #endregion

        #region Methods
        public void CreateProp()
        {
            if (_prop != null)
            {
                return;
            }

            _prop = World.CreateProp("ch_prop_ch_mobile_jammer_01x", Position, Rotation, false, false);
            _prop.IsOnlyDamagedByPlayer = true;

            Function.Call(Hash.SET_OBJECT_TARGETTABLE, _prop.Handle, true);
            Function.Call((Hash)0x63ECF581BC70E363, _prop.Handle, true); // no idea
            Function.Call((Hash)0x9097EB6D4BB9A12A, Game.Player.Handle, _prop.Handle); // no idea
            Function.Call(Hash.SET_ENTITY_PROOFS, _prop.Handle, false, false, false, true, false, false, false, false);
        }

        public void CreateBlip()
        {
            if (_blip != null)
            {
                return;
            }

            _blip = World.CreateBlip(Position);
            _blip.Sprite = BlipSprite.Wifi;
            _blip.IsShortRange = true;
            _blip.Name = "Signal Jammer";

            Function.Call(Hash._0x74513EA3E505181E, _blip.Handle, _isDestroyed); // SHOW_TICK_ON_BLIP
        }

        public bool Check()
        {
            if (_prop == null || !_prop.Exists())
            {
                return false;
            }

            return _prop.IsInAir || _prop.IsDead || Function.Call<bool>(Hash.HAS_OBJECT_BEEN_BROKEN, _prop.Handle, 0);
        }

        public void RemoveProp()
        {
            if (_prop != null)
            {
                Function.Call(Hash.SET_OBJECT_TARGETTABLE, _prop.Handle, false);
                Function.Call((Hash)0x9F260BFB59ADBCA3, Game.Player.Handle, _prop.Handle); // no idea

                _prop.Delete();
                _prop = null;
            }
        }

        public void RemoveBlip()
        {
            if (_blip != null)
            {
                _blip.Remove();
                _blip = null;
            }
        }
        #endregion
    }
}
