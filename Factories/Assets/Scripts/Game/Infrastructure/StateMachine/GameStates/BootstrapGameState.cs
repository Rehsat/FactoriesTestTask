using Game.Infrastructure.AssetsManagement;
using Game.Services.Canvases;
using Infrastructure.StateMachine;
using UniRx;
using UnityEngine;

namespace Game.Infrastructure.StateMachine.GameStates
{
    public class BootstrapGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;

        private const string MAIN_SCENE_NAME = "MainScene";

        public BootstrapGameState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public void SetStateMachine(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _sceneLoader.LoadScene(MAIN_SCENE_NAME, OnEnterMainScene);
        }

        private void OnEnterMainScene()
        {
            _stateMachine.EnterState<ResourceCollectState>();
        }
        
        public void Exit()
        {
        }
    }
}