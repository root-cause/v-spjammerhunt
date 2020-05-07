using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using JammerHunt.Enums;

namespace JammerHunt.Classes
{
    public class CompleteStage : StageBase
    {
        #region Fields
        private List<int> _carGenerators = new List<int>();
        private List<Blip> _carGeneratorBlips = new List<Blip>();
        #endregion

        #region Properties
        public override JammerHuntStage NextStage => JammerHuntStage.None;
        #endregion

        #region Methods
        public override void Init(bool scriptStart)
        {
            for (int i = 0; i < Constants.MaxCarGens; i++)
            {
                Vector3 position = Constants.ThrusterLocations[i].Item1;

                int carGenHandle = Function.Call<int>(
                    Hash.CREATE_SCRIPT_VEHICLE_GENERATOR,
                    position.X, position.Y, position.Z, Constants.ThrusterLocations[i].Item2,
                    5.0f, 3.0f,
                    (int)VehicleHash.Thruster,
                    -1, -1, -1, -1, 1, 0, 0, 0, 1, -1
                );

                Blip carGenBlip = World.CreateBlip(position);
                carGenBlip.Sprite = BlipSprite.Thruster;
                carGenBlip.IsShortRange = true;

                _carGenerators.Add(carGenHandle);
                _carGeneratorBlips.Add(carGenBlip);
            }

            if (!scriptStart)
            {
                Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "All jammers destroyed. The Mammoth Thruster can be found at locations marked with ~BLIP_NHP_WP3~.");
                Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, 0, 1, Constants.HighlightTime);

                foreach (Blip blip in _carGeneratorBlips)
                {
                    Function.Call(Hash.SET_BLIP_FLASHES, blip.Handle, true);
                    Function.Call(Hash.SET_BLIP_FLASH_TIMER, blip.Handle, Constants.HighlightTime);
                }
            }
        }

        public override bool Update()
        {
            return false;
        }

        public override void Destroy(bool scriptExit)
        {
            foreach (int handle in _carGenerators)
            {
                Function.Call(Hash.DELETE_SCRIPT_VEHICLE_GENERATOR, handle);
            }

            foreach (Blip blip in _carGeneratorBlips)
            {
                blip.Remove();
            }

            _carGenerators.Clear();
            _carGeneratorBlips.Clear();

            _carGenerators = null;
            _carGeneratorBlips = null;
        }
        #endregion
    }
}
