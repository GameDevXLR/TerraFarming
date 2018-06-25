using UnityEngine;
using UnityEngine.UI;

namespace PhaethonGames.TerraFarming.OreToEssence
{
    [System.Serializable]
    public struct TextDisplay
    {
        public Text alertDisplay;

        [Header("Result")]
        public Text oreDisplay;

        public Text oreNeddDisplay;
        public Text EssenceGot;

        [Header("Game")]
        public Text chrono;

        public Text timeBonus;
        public Text score;
    }

    [System.Serializable]
    public struct PanelList
    {
        public GameObject jaugePanel;
        public GameObject alertPanel;
        public GameObject gamePanel;
    }

    [System.Serializable]
    public struct AlertMessages
    {
        public string warning;
        public string success;
        public string end;
    }
}