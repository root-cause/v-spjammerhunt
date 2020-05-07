using JammerHunt.Enums;

namespace JammerHunt.Classes
{
    public abstract class StageBase
    {
        public abstract JammerHuntStage NextStage { get; }

        public abstract void Init(bool scriptStart);
        public abstract bool Update();
        public abstract void Destroy(bool scriptExit);
    }
}
