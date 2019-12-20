using System.Collections.Generic;
using Rocket.API;

namespace TPlugins.TShop
{
    public class TShopConfiguration : IRocketPluginConfiguration
    {
        public bool UsingQuality;
        public bool AllowOpenUIWithKey;
        public string Button;
        public string SuccessMessageColor;
        public string InfoMessageColor;
        public string ErrorMessageColor;
        public bool UIEnabled;
        public bool OpenButtonEnabled;
        public readonly ushort OpenID = 8000;
        public readonly ushort Main1ID = 8001;
        public readonly ushort Main2ID = 8002;
        public readonly ushort BuySellID = 8003;
        public List<ItemShop> ItemShop;

        public void LoadDefaults()
        {
            UsingQuality = true;
            AllowOpenUIWithKey = true;
            Button = "Please write a number between 0 and 4. (It's the number of the code hotkey in controls)";
            SuccessMessageColor = "#00FF00";
            InfoMessageColor = "#FFFFFF";
            ErrorMessageColor = "#FF8C00";
            UIEnabled = true;
            OpenButtonEnabled = true;
            ItemShop = new List<ItemShop>();
        }
    }

    public class ItemShop
    {
        public ushort Id { get; set; }
        public decimal BuyCost { get; set; }
        public decimal SellCost { get; set; }

        public ItemShop(ushort id, decimal buycost, decimal sellcost)
        {
            Id = id;
            BuyCost = buycost;
            SellCost = sellcost;
        }

        public ItemShop() { }
    }
}
