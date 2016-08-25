/// <summary>
/// 
/// </summary>
namespace bsensor
{
    /// <summary>
    /// 
    /// </summary>
    public class CmdSaveProject : BsensorCommand
    {
        private MyApplication myApp = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myApp"></param>
        public CmdSaveProject(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Execute()
        {
            bool cancel;
            myApp.SaveProject(out cancel);
        }
    }
}
