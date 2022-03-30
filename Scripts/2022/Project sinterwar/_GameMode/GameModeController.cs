using Assets._Core.Scripts._Actor;
using System.Collections;
using UnityEngine;

namespace Assets._Core.Scripts._GameMode
{
    public class GameModeController : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject _actorPrefab;

        [Header("Gamemode")]
        public GameMode _gameMode;

        public Transform _spawnPoint;

        private int _nextActorId = 0;

        private void Start()
        {
            CreateAndSpawnActor(_spawnPoint, 0);
        }

        private void Update()
        {
            //_gameMode.UpdateGameMode();
        }

        public void CreateAndSpawnActor(Transform spawnPoint, int actorTeam)
        {
            GameObject actor = Instantiate(_actorPrefab, spawnPoint.position, Quaternion.Euler(0, spawnPoint.rotation.y, 0));
            GameModeActorInfo newActor = new GameModeActorInfo(actor.GetComponent<ActorBase>(), _nextActorId, actorTeam);
            _gameMode._actors.Add(newActor);
            newActor._actor.Initialize(true, 1, actor.GetComponent<CharacterController>());

            Debug.LogFormat("Spawned new actor. {0}", newActor.ToString());
            _nextActorId++;
        }

        public void RemoveActor(int actorId)
        {
            GameModeActorInfo actor = _gameMode._actors[actorId];
            for(int i = 0; i < _gameMode._actors.Count; i++)
            {
                if (actor._actorId == _gameMode._actors[i]._actorId)
                {
                    _gameMode._actors.RemoveAt(i);
                    Destroy(actor._actor.gameObject);
                    Debug.LogFormat("Removed and destroyed actor. {0}", actor.ToString());
                    return;
                }
            }
            Debug.LogWarningFormat("Didn't find and remove actor with id({0}).", actorId);
        }

        public void RespawnActor(int actorId, float respawnTime)
        {
            StartCoroutine(DoRespawnActor(actorId, respawnTime));
        }

        private IEnumerator DoRespawnActor(int actorId, float respawnTime)
        {
            foreach (GameModeActorInfo actor in _gameMode._actors)
            {
                if (actorId == actor._actorId)
                {
                    Debug.LogFormat("Respawning actor id({0}) in {1} seconds.", actorId, respawnTime);
                    yield return new WaitForSeconds(respawnTime);
                    // Do the actual respawning of the actor.
                    yield break;
                }
            }
            Debug.LogWarningFormat("Respawn failed, actor with id({0}) could not be found!", actorId);
        }
    }
}