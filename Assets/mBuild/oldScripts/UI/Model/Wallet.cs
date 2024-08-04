using System;
using YG;

public class Wallet 
{
    public event Action<int> MoneyChanged;
    private int _money;

    public void Init() => _money = YandexGame.savesData.money;

    public bool TryUseMoney(int value)
    {
        if (_money >= value)
        {
            _money -= value;
            MoneyChanged?.Invoke(_money);
            Save();

            return true;
        }
        else 
            return false;
    }

    public void AddMoney(int value)
    {
        _money += value;
        MoneyChanged?.Invoke(_money);

        Save();
    }

    private void Save()
    {
        YandexGame.savesData.money = _money;
        YandexGame.SaveProgress();
    }
}
