using YG;

namespace Scripts.Infrastructure
{
    public static class PlayerData
    {
        public static int[] PlayerUnits
        {
            get => YandexGame.savesData.playerUnits;
            set
            {
                YandexGame.savesData.playerUnits = value;
                YandexGame.SaveProgress();
            }
        }

        public static int PlayerMoney
        {
            get => YandexGame.savesData.money;
            set
            {
                YandexGame.savesData.money = value;
                YandexGame.SaveProgress();
            }
        }

        public static int CurrentLevel
        {
            get => YandexGame.savesData.level;
            set
            {
                YandexGame.savesData.level = value;
                YandexGame.SaveProgress();
            }
        }
    }
}