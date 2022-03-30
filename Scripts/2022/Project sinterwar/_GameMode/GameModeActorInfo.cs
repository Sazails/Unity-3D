using Assets._Core.Scripts._Actor;

namespace Assets._Core.Scripts._GameMode
{
    public class GameModeActorInfo
    {
        public ActorBase _actor;
        public int _actorId;
        public int _teamId;
        public string _actorName;
        public int _score;
        public int _killCount;
        public int _deathCount;

        public GameModeActorInfo() { }

        public GameModeActorInfo(ActorBase actor, int actorId, int teamId) {
            this._actor = actor;
            this._actorId = actorId;
            this._teamId = teamId;
            this._score = 0;
            this._killCount = 0;
            this._deathCount = 0;
        }

        public override string ToString()
        {
            return string.Format("Actor id({0}), team id({1}), name({2}), score({3}), killCount({4}), deathCount({5})", _actorId, _teamId, _actorName, _score, _killCount, _deathCount);
        }
    }
}