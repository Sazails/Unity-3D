using UnityEngine;

namespace Assets._Core.Scripts._GameMode
{
    public class GameModeScoreController : MonoBehaviour
    {
        public GameModeController _gameModeController;

        public void SetActorScore(int actorId, int score)
        {
            _gameModeController._gameMode._actors[actorId]._score = score;
        }

        public int GetActorScore(int actorId) => _gameModeController._gameMode._actors[actorId]._score;
        public void IncreaseActorScore(int actorId, int score) => _gameModeController._gameMode._actors[actorId]._score += score;
        public int IncreaseAndGetActorScore(int actorId, int score) => _gameModeController._gameMode._actors[actorId]._score += score;
    }
}