namespace Celeste.Mod.TempleDustSpinnersMod;

public class TempleDustSpinnersModModuleSettings : EverestModuleSettings {
    [SettingName("TEMPLEDUSTSPINNERSMOD_SETTING_NAME")]
    [SettingSubText("TEMPLEDUSTSPINNERSMOD_SETTING_DESC")]
    public bool ReplaceTempleSpinners { get; set; } = true;
}