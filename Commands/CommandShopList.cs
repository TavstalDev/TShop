using SDG.Unturned;
using System;
using System.Collections.Generic;
using Rocket.Unturned.Chat;
using Rocket.API;

namespace TPlugins.TShop
{
    public class CommandShopList : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopList";
        public string Help => "You can watch list of the added items";
        public string Syntax => "<Page>";
        public List<string> Aliases => new List<string> { "shlist" };
        public List<string> Permissions => new List<string> { "TShop.shoplist" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            var Config = m.Configuration.Instance;
            if (args.Length == 1)
            {

                if (Config.ItemShop.Count < 1)
                {
                    UnturnedChat.Say(caller, m.Translate("shop_is_empty"), color: m.ErrorColor);
                    return;
                }
                //I'm tired to making a custom code so then I copied this code from RocketJobs and I edited a little bit. from here
                int PageNumber = 1;
                try
                {
                    PageNumber = Convert.ToInt32(args[0]);
                }
                catch (FormatException)
                {
                    UnturnedChat.Say(caller, m.Translate("format_error", args[1]), color: m.ErrorColor);
                    return;
                }
                catch (OverflowException)
                {
                    UnturnedChat.Say(caller, m.Translate("overflow_error", args[1]), color: m.ErrorColor);
                    return;
                }
                int Min = (PageNumber * 4) - 4;
                int Max = (PageNumber * 4) - 1;
                int MaxPages = (Config.ItemShop.Count / 3) + 1;
                if (PageNumber < MaxPages)
                {
                    for (int i = Min; i <= Max; i++)
                    {
                        if (i == Max)
                        {
                            UnturnedChat.Say(caller, m.Translate("next_page", args[0].ToLower(), (PageNumber + 1)), color: m.MessageColor);
                            break;
                        }
                        else
                        {
                            UnturnedChat.Say(caller, m.Translate("itemshop", Assets.find(EAssetType.ITEM, Config.ItemShop[i - (PageNumber - 1)].Id).name, Config.ItemShop[i - (PageNumber - 1)].Id, Config.ItemShop[i - (PageNumber - 1)].BuyCost, Config.ItemShop[i - (PageNumber - 1)].SellCost), color: m.MessageColor);
                        }
                    }
                }
                else if (PageNumber == MaxPages)
                {
                    int Item = 0;
                    int ItemCount = Config.ItemShop.Count % 3;
                    for (int i = Min; i <= Max; i++)
                    {
                        if (Item > ItemCount - 1)
                        {
                            UnturnedChat.Say(caller, m.Translate("end_of_list", args[0].ToLower()), color: m.MessageColor);
                            break;
                        }
                        else
                        {
                            UnturnedChat.Say(caller, m.Translate("itemshop", Assets.find(EAssetType.ITEM, Config.ItemShop[i - (PageNumber - 1)].Id).name, Config.ItemShop[i - (PageNumber - 1)].Id, Config.ItemShop[i - (PageNumber - 1)].BuyCost, Config.ItemShop[i - (PageNumber - 1)].SellCost), color: m.MessageColor);
                        }
                        Item++;
                    }
                }
                else if (PageNumber > MaxPages)
                {
                    UnturnedChat.Say(caller, m.Translate("unexistant_page"), color: m.ErrorColor);
                }
                //To here
            }
            else
            {
                UnturnedChat.Say(caller, m.Translate("usage_list"), color: m.MessageColor);
                return;
            }
        }
    }
}
