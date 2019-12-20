using SDG.Unturned;
using System;
using System.Collections.Generic;
using Rocket.Unturned.Chat;
using Rocket.API;
using System.Linq;

namespace TPlugins.TShop
{
    public class CommandShopAdd : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopAdd";
        public string Help => "You can add items to the shop";
        public string Syntax => "<ItemId> <BuyPrice> <SellPrice>";
        public List<string> Aliases => new List<string> { "shadd" };
        public List<string> Permissions => new List<string> { "TShop.shopadd" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            if (args.Length == 3)
            {
                ushort id = Convert.ToUInt16(args[0]);
                decimal buycost = Convert.ToDecimal(args[1]);
                decimal sellcost = Convert.ToDecimal(args[2]);
                ItemShop Is = m.Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == id);

                if (Assets.find(EAssetType.ITEM, id) != null)
                {
                    Asset a = Assets.find(EAssetType.ITEM, id);
                    if (Is == null)
                    {
                        m.Configuration.Instance.ItemShop.Add(new ItemShop(id, buycost, sellcost));
                        m.Configuration.Save();
                        UnturnedChat.Say(caller, m.Translate("successfully_added", ((ItemAsset)a).itemName, id, buycost.ToString(), sellcost.ToString()), color: m.SuccessColor);
                        return;
                    }
                    else
                    {
                        UnturnedChat.Say(caller, m.Translate("item_already_added", ((ItemAsset)a).itemName), color: m.ErrorColor);
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
                UnturnedChat.Say(caller, m.Translate("usage_add"), color: m.MessageColor);
                return;
            }
        }
    }
}
