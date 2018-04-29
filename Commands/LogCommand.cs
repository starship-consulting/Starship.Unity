using Starship.Unity.Utilities;

namespace Starship.Unity.Commands {
    public class LogCommand : Command {

        protected override void Start() {
            base.Start();

            Log.Write(Message);
            SetProgress(1);
        }

        public string Message;
    }
}