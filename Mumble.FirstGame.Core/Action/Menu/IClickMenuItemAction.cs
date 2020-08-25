using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene;

namespace Mumble.FirstGame.Core.Action.Menu
{
    public interface IClickMenuItemAction : IAction
    {
        void CalculateEffect(IOwnerIdentifier owner, IScene scene);
    }
}
