using SDG.Unturned;
using System;
using System.Collections.Generic;
using Rocket.Unturned.Chat;
using Rocket.API;
using System.Linq;

namespace TPlugins.TShop
{
    public class CommandShopRemove : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopRemove";
        public string Help => "You can remove items from the shop";
        public string Syntax => "<ItemId>";
        public List<string> Aliases => new List<string> { "shrem" };
        public List<string> Permissions => new List<string> { "TShop.shopremove" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            if (args.Length == 1)
            {
                ushort id = Convert.ToUInt16(args[0]);
                ItemShop Is = null;
                Is = m.Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == id);
                if (Assets.find(EAssetType.ITEM, id) != null)
                {
                    if (Is != null)
                    {
                        Asset a = Assets.find(EAssetType.ITEM, id);
                        m.Configuration.Instance.ItemShop.Remove(Is);
                        m.Configuration.Save();
                        UnturnedChat.Say(caller, m.Translate("successfully_removed", ((ItemAsset)a).itemName, id), color: m.SuccessColor);
                        return;
                    }
                    else
                    {
                        Asset a = Assets.find(EAssetType.ITEM, id);
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
                UnturnedChat.Say(caller, m.Translate("usage_remove"), color: m.MessageColor);
                return;
            }
        }
    }
}
