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
    public class CommandShopSell : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ShopSell";
        public string Help => "You can sell items to the shop";
        public string Syntax => "<ItemId> <Amount>";
        public List<string> Aliases => new List<string> { "shsell", "sell" };
        public List<string> Permissions => new List<string> { "TShop.shopsell" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            var m = Main.Instance;
            if (args.Length == 2 || args.Length == 1)
            {
                UnturnedPlayer p = (UnturnedPlayer)caller;
                byte amt = 1;
                ushort id = Convert.ToUInt16(args[0]);
                if (args.Length == 2)
                    amt = Convert.ToByte(args[1]);

                ItemShop Is = null;
                Is = m.Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == id);

                if (Is == null)
                {
                    Asset a = Assets.find(EAssetType.ITEM, id);
                    UnturnedChat.Say(caller, m.Translate("item_isn't_added", ((ItemAsset)a).itemName, id), color: m.ErrorColor);
                    return;
                }

                if (Assets.find(EAssetType.ITEM, id) == null)
                {
                    UnturnedChat.Say(caller, m.Translate("item_isn't_found", id), color: m.ErrorColor);
                    return;
                }

                //I'm tired to making a custom code so then I copied this code from Zaup Shop and I edited a little bit. from here
                List<InventorySearch> l = p.Inventory.search(id, true, true);
                if (l.Count == 0 || (l.Count < amt))
                {
                    Asset a = Assets.find(EAssetType.ITEM, id);
                    UnturnedChat.Say(caller, m.Translate("not_enough_items_to_sell", amt.ToString(), ((ItemAsset)a).itemName), color: m.ErrorColor);
                    return;
                }
                else
                {
                    var amt2 = amt;
                    decimal money = 0;
                    byte quality = 100;
                    decimal peritemprice = 0;
                    while (amt > 0)
                    {
                        if (p.Player.equipment.checkSelection(l[0].page, l[0].jar.x, l[0].jar.y))
                        {
                            p.Player.equipment.dequip();
                        }

                        if (l[0].jar.item.amount >= amt)
                        {
                            if (Main.Instance.Configuration.Instance.UsingQuality)
                                quality = l[0].jar.item.durability;
                            byte left = (byte)(l[0].jar.item.amount - amt);
                            l[0].jar.item.amount = left;
                            p.Inventory.sendUpdateAmount(l[0].page, l[0].jar.x, l[0].jar.y, left);
                            amt = 0;

                            if (left == 0)
                            {
                                p.Inventory.removeItem(l[0].page, p.Inventory.getIndex(l[0].page, l[0].jar.x, l[0].jar.y));
                                l.RemoveAt(0);
                            }
                        }
                        else
                        {
                            if (Main.Instance.Configuration.Instance.UsingQuality)
                                quality = l[0].jar.item.durability;
                            amt -= l[0].jar.item.amount;
                            p.Inventory.sendUpdateAmount(l[0].page, l[0].jar.x, l[0].jar.y, 0);
                            p.Inventory.removeItem(l[0].page, p.Inventory.getIndex(l[0].page, l[0].jar.x, l[0].jar.y));
                            l.RemoveAt(0);
                        }

                        peritemprice = decimal.Round(Is.SellCost * (quality / 100.0m), 2);
                        money = money + peritemprice;
                    }
                    //To here
                    Uconomy.Instance.Database.IncreaseBalance(p.Id, money);
                    Asset a = Assets.find(EAssetType.ITEM, id);
                    UnturnedChat.Say(caller, m.Translate("successfully_sell", ((ItemAsset)a).itemName, Uconomy.Instance.Database.GetBalance(p.Id).ToString()), color: m.SuccessColor);
                    return;
                }
            }
            else
            {
                UnturnedChat.Say(caller, m.Translate("usage_sell"), color: m.MessageColor);
                return;
            }
        }
    }
}
