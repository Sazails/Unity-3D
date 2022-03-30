using System.Collections.Generic;

namespace Assets._Core.Scripts._GameMode
{
    public abstract class GameMode
    {
        public List<GameModeActorInfo> _actors;

        public abstract void Init();
        public abstract void UpdateGameMode();
    }
}