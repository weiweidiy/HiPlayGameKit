namespace HotFixGameKit.Game
{
    public class BussinessMainStage : BussinessBase
    {
        public override void DoBussiness(params object[] args)
        {
            StageManager.LoadSceneAsync("Main");
        }
    }
}
