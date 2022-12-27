public class DebugManager : Singleton<DebugManager> {

    public bool debug;

    public override void LoadOnUpdateIntervall () { }

    protected override bool IsAsync () => false;

    protected override void OnLoadSync () { }
}