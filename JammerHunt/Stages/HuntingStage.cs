using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;
using JammerHunt.Enums;
using JammerHunt.Managers;

namespace JammerHunt.Classes
{
    public class HuntingStage : StageBase
    {
        #region Fields
        private List<Jammer> _jammers = new List<Jammer>();
        private int _nextUpdate = 0;
        private int _activeIndex = -1;
        private int _soundId = -1;
        #endregion

        #region Properties
        public override JammerHuntStage NextStage => JammerHuntStage.Complete;
        #endregion

        #region Private methods
        private int GetClosestJammerIndex(Vector3 position, float range)
        {
            int closestIndex = -1;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < _jammers.Count; i++)
            {
                float distance = position.DistanceToSquared2D(_jammers[i].Position);
                if (distance < closestDistance && distance < range)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }

            return closestIndex;
        }

        private int GetNumJammersDestroyed()
        {
            return _jammers.Count(jammer => jammer.IsDestroyed);
        }

        private void StopSound()
        {
            if (_soundId != -1)
            {
                Audio.StopSound(_soundId);
                Audio.ReleaseSound(_soundId);

                _soundId = -1;
            }
        }
        #endregion

        #region Public methods
        public override void Init(bool scriptStart)
        {
            // Load audio
            Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "DLC_VINEWOOD/DLC_VW_HIDDEN_COLLECTIBLES", false, -1);

            // Initialize jammers
            for (int i = 0; i < Constants.MaxJammers; i++)
            {
                Jammer jammer = new Jammer(Constants.JammerLocations[i].Item1, Constants.JammerLocations[i].Item2, SaveManager.Progress[i]);

                if (Main.EnableBlips)
                {
                    jammer.CreateBlip();
                }

                _jammers.Add(jammer);
            }
        }

        public override bool Update()
        {
            int gameTime = Game.GameTime;

            // Find closest jammer
            if (gameTime > _nextUpdate)
            {
                _nextUpdate = gameTime + Constants.LocationUpdateInterval;

                int newIndex = GetClosestJammerIndex(Game.Player.Character.Position, Constants.MaxJammerDistance);
                if (_activeIndex != newIndex)
                {
                    // Remove the previously active jammer
                    if (_activeIndex != -1)
                    {
                        _jammers[_activeIndex].RemoveProp();
                        StopSound();
                    }

                    // Create new jammer prop
                    if (newIndex != -1 && !_jammers[newIndex].IsDestroyed)
                    {
                        _jammers[newIndex].CreateProp();

                        _soundId = Function.Call<int>(Hash.GET_SOUND_ID);
                        Function.Call(
                            Hash.PLAY_SOUND_FROM_ENTITY,
                            _soundId,
                            "attract",
                            _jammers[newIndex].PropHandle,
                            "dlc_ch_hidden_collectibles_sj_sounds",
                            false,
                            0
                        );
                    }

                    _activeIndex = newIndex;
                }
            }

            // Check if the active jammer is destroyed
            if (_activeIndex != -1 && !_jammers[_activeIndex].IsDestroyed && _jammers[_activeIndex].Check())
            {
                StopSound();

                Jammer jammer = _jammers[_activeIndex];
                jammer.IsDestroyed = true;

                Function.Call(
                    Hash.PLAY_SOUND_FROM_COORD,
                    -1,
                    "destroyed",
                    jammer.Position.X, jammer.Position.Y, jammer.Position.Z,
                    "dlc_ch_hidden_collectibles_sj_sounds",
                    false,
                    0,
                    0
                );

                Function.Call(Hash.SET_OBJECT_TARGETTABLE, jammer.PropHandle, false);
                Function.Call((Hash)0x9F260BFB59ADBCA3, Game.Player.Handle, jammer.PropHandle); // no idea

                // Update save file
                SaveManager.Progress[_activeIndex] = true;
                SaveManager.Save();

                // Reward stuff
                Game.Player.Money += Main.JammerDestroyedReward;

                int numDestroyed = GetNumJammersDestroyed();
                Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "SIGNAL_COLLECT");
                Function.Call(Hash.ADD_TEXT_COMPONENT_INTEGER, numDestroyed);
                Function.Call(Hash._DRAW_NOTIFICATION, false, true);

                if (numDestroyed == Constants.MaxJammers)
                {
                    Game.Player.Money += Main.AllJammersDestroyedReward;
                    return true;
                }
            }

            return false;
        }

        public override void Destroy(bool scriptExit)
        {
            // Stop the audio clue
            StopSound();

            // Remove active jammer prop
            if (_activeIndex != -1)
            {
                _jammers[_activeIndex].RemoveProp();
            }

            // Remove blips
            if (Main.EnableBlips)
            {
                foreach (Jammer jammer in _jammers)
                {
                    jammer.RemoveBlip();
                }
            }

            _jammers.Clear();
            _jammers = null;
        }
        #endregion
    }
}
