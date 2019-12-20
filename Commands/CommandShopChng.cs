using SDG.Unturned;
using System;
using System.Collections.Generic;
using Rocket.Unturned.Chat;
using Rocket.API;
using System.Linq;

namespace TPlugins.TShop
{
    public class CommandShopChng : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopChng";
        public string Help => "You can change items price in the shop";
        public string Syntax => "<ItemId> <BuyPrice> <SellPrice>";
        public List<string> Aliases => new List<string> { "shchng" };
        public List<string> Permissions => new List<string> { "TShop.shopchng" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            if (args.Length == 3)
            {
                ushort id = Convert.ToUInt16(args[0]);
                decimal buycost = Convert.ToDecimal(args[1]);
                decimal sellcost = Convert.ToDecimal(args[2]);
                ItemShop Is = m.Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == id);

                if (Assets.find(EAssetType.ITEM, id) != null && buycost != 0 && sellcost != 0)
                {
                    Asset a = Assets.find(EAssetType.ITEM, id);
                    if (Is != null)
                    {
                        Is.BuyCost = buycost;
                        Is.SellCost = sellcost;
                        m.Configuration.Save();
                        UnturnedChat.Say(caller, m.Translate("successfully_changed", ((ItemAsset)a).itemName, id, buycost.ToString(), sellcost.ToString()), color: m.SuccessColor);
                        return;
                    }
                    else
                    {
                        UnturnedChat.Say(caller, m.Translate("item_isn't_added", ((ItemAsset)a).itemName, id), color: m.ErrorColor);
                        return;
                    }
                }
                else
                {
                    UnturnedChat.Say(caller, m.Translate("item_isn't_found", id), color: m.ErrorColor);
                    return;
                }
            }
            else
            {
                UnturnedChat.Say(caller, m.Translate("usage_chng"), color: m.MessageColor);
                return;
            }
        }
    }
}
