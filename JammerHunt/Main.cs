using System;
using GTA;
using JammerHunt.Enums;
using JammerHunt.Classes;
using JammerHunt.Managers;

namespace JammerHunt
{
    public class Main : Script
    {
        public StageBase CurrentStageHandler = null;

        // Settings
        public static int JammerDestroyedReward = Constants.DefaultJammerDestroyedReward;
        public static int AllJammersDestroyedReward = Constants.DefaultAllDestroyedReward;
        public static bool EnableBlips = Constants.DefaultBlipsEnabled;

        #region Methods
        public void MakeNewHandler(JammerHuntStage newStage, bool scriptStart)
        {
            if (CurrentStageHandler != null)
            {
                CurrentStageHandler.Destroy(false);
                CurrentStageHandler = null;
            }

            switch (newStage)
            {
                case JammerHuntStage.Hunting:
                    CurrentStageHandler = new HuntingStage();
                    break;

                case JammerHuntStage.Complete:
                    CurrentStageHandler = new CompleteStage();
                    break;

                default:
                    throw new NotImplementedException("Not implemented stage used with MakeNewHandler.");
            }

            CurrentStageHandler.Init(scriptStart);
        }
        #endregion

        #region Constructor
        public Main()
        {
            Interval = 100;

            // Load settings
            JammerDestroyedReward = Settings.GetValue("CONFIG", "DESTROY_REWARD", Constants.DefaultJammerDestroyedReward);
            AllJammersDestroyedReward = Settings.GetValue("CONFIG", "DESTROY_ALL_REWARD", Constants.DefaultAllDestroyedReward);
            EnableBlips = Settings.GetValue("CONFIG", "BLIPS_ENABLED", Constants.DefaultBlipsEnabled);

            // Load save file & create handler
            MakeNewHandler(SaveManager.Load(), true);

            // Events
            Tick += Main_Tick;
            Aborted += Main_Aborted;
        }
        #endregion

        #region Events
        private void Main_Tick(object sender, EventArgs e)
        {
            if (CurrentStageHandler != null && CurrentStageHandler.Update())
            {
                MakeNewHandler(CurrentStageHandler.NextStage, false);
            }
        }

        private void Main_Aborted(object sender, EventArgs e)
        {
            if (CurrentStageHandler != null)
            {
                CurrentStageHandler.Destroy(true);
                CurrentStageHandler = null;
            }
        }
        #endregion
    }
}
