using Phoenix.Core.Interfaces;

namespace Phoenix.Core.Interfaces
{
    internal interface IStage
    {
        bool IsDetected { get; }

        void Execute(IContext context);
    }
}