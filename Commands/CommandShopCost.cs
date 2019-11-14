using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.Unturned.Chat;
using Rocket.API;

namespace TPlugins.TShop
{
    public class CommandShopCost : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopCost";
        public string Help => "You can get price of the items from the shop";
        public string Syntax => "<ItemId>";
        public List<string> Aliases => new List<string> { "cost", "shcost" };
        public List<string> Permissions => new List<string> { "TShop.shopcost" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            if (args.Length == 1)
            {
                UnturnedPlayer p = (UnturnedPlayer)caller;
                ushort id = Convert.ToUInt16(args[0]);

                if (Assets.find(EAssetType.ITEM, id) == null)
                {
                    UnturnedChat.Say(caller, m.Translate("item_isn't_found", id), color: m.ErrorColor);
                    return;
                }

                ItemShop Is = m.Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == id);

                if (Is == null)
                {
                    UnturnedChat.Say(caller, m.Translate("item_isn't_added", Assets.find(EAssetType.ITEM, id).name, id), color: m.ErrorColor);
                    return;
                }
                else
                {
                    UnturnedChat.Say(caller, m.Translate("cost", id, Is.BuyCost, Is.SellCost), color: m.SuccessColor);
                    return;
                }
            }
            else
            {
                UnturnedChat.Say(caller, m.Translate("usage_cost"), color: m.MessageColor);
                return;
            }
        }
    }
}
