using Assets.Scripts.Utilities;

namespace Assets.Scripts.Commands {
    public class LogCommand : Command {

        protected override void Start() {
            base.Start();

            Log.Write(Message);
            SetProgress(1);
        }

        public string Message;
    }
}