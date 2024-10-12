using Microsoft.Xna.Framework;
using System;

namespace Celeste.Mod.TempleDustSpinnersMod;

public class TempleDustSpinnersModModule : EverestModule {
    public static TempleDustSpinnersModModule Instance { get; private set; } = null!;

    public override Type SettingsType => typeof(TempleDustSpinnersModModuleSettings);
    public static TempleDustSpinnersModModuleSettings Settings => (TempleDustSpinnersModModuleSettings) Instance._Settings;

    public TempleDustSpinnersModModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(TempleDustSpinnersModModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(TempleDustSpinnersModModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        Everest.Events.Level.OnLoadEntity += OnLoadEntity;
        On.Celeste.DustStyles.Get_Session += OnGetDustStyle;
    }

    public override void Unload() {
        Everest.Events.Level.OnLoadEntity -= OnLoadEntity;
        On.Celeste.DustStyles.Get_Session -= OnGetDustStyle;
    }

    private static bool OnLoadEntity(Level level, LevelData levelData, Vector2 offset, EntityData entityData)
    {
        if (Settings.ReplaceTempleSpinners
            && level.Session.Area.ID == 5 || (level.Session.Area.ID == 7 && level.Session.Level.StartsWith("f-")))
        {
            switch (entityData.Name)
            {
                case "spinner":
                    level.Add(new DustStaticSpinner(entityData, offset));
                    return true;

                case "trackSpinner":
                    level.Add(new DustTrackSpinner(entityData, offset));
                    return true;

                case "rotateSpinner":
                    level.Add(new DustRotateSpinner(entityData, offset));
                    return true;
            }
        }
        return false;
    }

    private static DustStyles.DustStyle OnGetDustStyle(On.Celeste.DustStyles.orig_Get_Session orig, Session session)
        => (Settings.ReplaceTempleSpinners && session.Area.ID == 7 && session.Level.StartsWith("f-")) ?
        DustStyles.Styles[5] : orig(session);
}