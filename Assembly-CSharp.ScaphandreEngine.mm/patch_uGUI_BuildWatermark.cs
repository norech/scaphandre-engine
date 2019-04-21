using MonoMod;
using ScaphandreEngine;
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
            + "\nScaphandre Engine v" + Scaphandre.Version;
    }
}