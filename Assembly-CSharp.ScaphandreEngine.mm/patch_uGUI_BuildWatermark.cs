#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.
using ScaphandreInjector;
using MonoMod;
using System;
using UnityEngine.UI;

class patch_uGUI_BuildWatermark : uGUI_BuildWatermark
{
    [MonoModReplace]
    protected void UpdateText()
    {
        var component = GetComponent<Text>();

        var dateTimeOfBuild = SNUtils.GetDateTimeOfBuild();
        var plasticChangeSetOfBuild = SNUtils.GetPlasticChangeSetOfBuild();

        component.text = Language.main.GetFormat("EarlyAccessWatermarkFormat", dateTimeOfBuild, plasticChangeSetOfBuild)
            + "\nScaphandre Engine v" + ScaphandreEngine.Scaphandre.Version;
    }
}