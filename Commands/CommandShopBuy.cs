using fr34kyn01535.Uconomy;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.Unturned.Chat;
using Rocket.API;

namespace TPlugins.TShop
{
    public class CommandShopBuy : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopBuy";
        public string Help => "You can buy items from the shop";
        public string Syntax => "<ItemId> <Amount>";
        public List<string> Aliases => new List<string> { "buy", "shbuy" };
        public List<string> Permissions => new List<string> { "TShop.shopbuy" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            if (args.Length == 2 || args.Length == 1)
            {

                byte amt = 1;
                UnturnedPlayer p = (UnturnedPlayer)caller;
                ushort id = Convert.ToUInt16(args[0]);
                if (args.Length == 2)
                    amt = Convert.ToByte(args[1]);

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

                if (Uconomy.Instance.Database.GetBalance(p.Id) > Is.BuyCost * amt)
                {
                    Uconomy.Instance.Database.IncreaseBalance(p.Id, -Is.BuyCost * amt);
                    p.GiveItem(Is.Id, amt);
                    UnturnedChat.Say(caller, m.Translate("successfully_buy", amt.ToString(), Assets.find(EAssetType.ITEM, Is.Id).name, Is.Id, Is.BuyCost * amt), color: m.SuccessColor);
                    return;
                }
                else
                {
                    UnturnedChat.Say(caller, m.Translate("not_enough_money", Is.BuyCost * amt - Uconomy.Instance.Database.GetBalance(p.Id)), color: m.ErrorColor);
                    return;
                }
            }
            else
            {
                UnturnedChat.Say(caller, m.Translate("usage_buy"), color: m.MessageColor);
                return;
            }
        }
    }
}
