using System.Collections.Generic;
using Rocket.API;

namespace TPlugins.TShop
{
    public class TShopConfiguration : IRocketPluginConfiguration
    {
        public bool UsingQuality;
        public string SuccessMessageColor;
        public string InfoMessageColor;
        public string ErrorMessageColor;
        public bool UIEnabled;
        public ushort OpenID;
        public ushort Main1ID;
        public ushort Main2ID;
        public ushort BuySellID;
        public List<ItemShop> ItemShop;

        public void LoadDefaults()
        {
            UsingQuality = true;
            SuccessMessageColor = "#00FF00";
            InfoMessageColor = "#FFFFFF";
            ErrorMessageColor = "#FF8C00";
            UIEnabled = true;
            OpenID = 8000;
            Main1ID = 8001;
            Main2ID = 8002;
            BuySellID = 8003;
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
