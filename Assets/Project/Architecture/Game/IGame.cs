using Project.Game;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAndFixedUpdatable, IUpdater, IFixedUpdater
    {
        ICameraController CameraController { get; set; }
        IGameInputService InputService { get; set; }
        IPlayer Player { get; set; }
        void Start();
    }
}