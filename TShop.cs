using Rocket.API;
using Rocket.Core.Plugins;
using System;
using Rocket.API.Collections;
using Logger = Rocket.Core.Logging.Logger;
using UnityEngine;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System.Linq;
using Rocket.Unturned.Player;
using Rocket.Unturned;

namespace TPlugins.TShop
{
    public class Main : RocketPlugin<TShopConfiguration>
    {
        public static Main Instance;
        public Color SuccessColor;
        public Color MessageColor;
        public Color ErrorColor;
        string search;

        protected override void Load()
        {
            Instance = this;
            EffectManager.onEffectTextCommitted += text;
            EffectManager.onEffectButtonClicked += button;
            if (Configuration.Instance.AllowOpenUIWithKey)
                PlayerInput.onPluginKeyTick += Key;
            U.Events.OnPlayerConnected += join;
            SuccessColor = UnturnedChat.GetColorFromName(Configuration.Instance.SuccessMessageColor, Palette.SERVER);
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.InfoMessageColor, Palette.SERVER);
            ErrorColor = UnturnedChat.GetColorFromName(Configuration.Instance.ErrorMessageColor, Palette.SERVER);
            Logger.Log("####################################", color: ConsoleColor.Yellow);
            Logger.Log("#   Thanks for downloading TShop   #", color: ConsoleColor.Yellow);
            Logger.Log("#    Plugin Created By TPlugins    #", color: ConsoleColor.Yellow);
            Logger.Log("#      Discord: TPlugins#6189      #", color: ConsoleColor.Yellow);
            Logger.Log("####################################", color: ConsoleColor.Yellow);
            Logger.Log("");
            Logger.Log("TShop is successfully loaded!", color: ConsoleColor.Green);
        }

        protected override void Unload()
        {
            EffectManager.onEffectTextCommitted -= text;
            EffectManager.onEffectButtonClicked -= button;
            if (Configuration.Instance.AllowOpenUIWithKey)
                PlayerInput.onPluginKeyTick -= Key;
            U.Events.OnPlayerConnected -= join;
            Logger.Log("TShop is successfully unloaded!", color: ConsoleColor.Green);
        }

        public void Key(Player player, uint simulation, byte key, bool state)
        {
            if (Configuration.Instance.AllowOpenUIWithKey)
            {
                int.TryParse(Configuration.Instance.Button, out int num);

                if (key == num && state)
                {
                    if (!Configuration.Instance.UIEnabled)
                        return;

                    TShopComponent cp = player.GetComponent<TShopComponent>();
                    if (!cp.UIOpened)
                    {
                        player.serversideSetPluginModal(true);
                        MainEffect(UnturnedPlayer.FromPlayer(player));
                        cp.UIOpened = true;
                    }
                }
            }
        }

        public void join(UnturnedPlayer player)
        {
            if (Configuration.Instance.UIEnabled && Configuration.Instance.OpenButtonEnabled)
                EffectManager.sendUIEffect(Configuration.Instance.OpenID, 700, player.CSteamID, true);
        }

        public void text(Player player, string buttonName, string text)
        {
            if (Configuration.Instance.UIEnabled)
            {
                if (buttonName == "tshop_search_if")
                {
                    search = "";
                    search = text;
                }
            }
        }

        public void MainEffect(UnturnedPlayer p)
        {
            TShopComponent cp = p.GetComponent<TShopComponent>();
            cp.amt = 1;
            ItemShop sh0 = null;
            ItemShop sh1 = null;
            ItemShop sh2 = null;
            ItemShop sh3 = null;
            ItemShop sh4 = null;
            ItemShop sh5 = null;
            ItemShop sh6 = null;
            ItemShop sh7 = null;

            if (cp.item0 < Configuration.Instance.ItemShop.Count)
                sh0 = Configuration.Instance.ItemShop[cp.item0];
            if (cp.item1 < Configuration.Instance.ItemShop.Count)
                sh1 = Configuration.Instance.ItemShop[cp.item1];
            if (cp.item2 < Configuration.Instance.ItemShop.Count)
                sh2 = Configuration.Instance.ItemShop[cp.item2];
            if (cp.item3 < Configuration.Instance.ItemShop.Count)
                sh3 = Configuration.Instance.ItemShop[cp.item3];
            if (cp.item4 < Configuration.Instance.ItemShop.Count)
                sh4 = Configuration.Instance.ItemShop[cp.item4];
            if (cp.item5 < Configuration.Instance.ItemShop.Count)
                sh5 = Configuration.Instance.ItemShop[cp.item5];
            if (cp.item6 < Configuration.Instance.ItemShop.Count)
                sh6 = Configuration.Instance.ItemShop[cp.item6];
            if (cp.item7 < Configuration.Instance.ItemShop.Count)
                sh7 = Configuration.Instance.ItemShop[cp.item7];

            string i0 = null;
            string i1 = null;
            string i2 = null;
            string i3 = null;
            string i4 = null;
            string i5 = null;
            string i6 = null;
            string i7 = null;

            if (sh0 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh0.Id);
                i0 = ((ItemAsset)a).itemName + " (ID:" + sh0.Id + ")";
            }
            else
                i0 = Translate("no_more_products");

            if (sh1 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh1.Id);
                i1 = ((ItemAsset)a).itemName + " (ID:" + sh1.Id + ")";
            }
            else
                i1 = Translate("no_more_products");

            if (sh2 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh2.Id);
                i2 = ((ItemAsset)a).itemName + " (ID:" + sh2.Id + ")";
            }
            else
                i2 = Translate("no_more_products");

            if (sh3 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh3.Id);
                i3 = ((ItemAsset)a).itemName + " (ID:" + sh3.Id + ")";
            }
            else
                i3 = Translate("no_more_products");

            if (sh4 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh4.Id);
                i4 = ((ItemAsset)a).itemName + " (ID:" + sh4.Id + ")";
            }
            else
                i4 = Translate("no_more_products");

            if (sh5 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh5.Id);
                i5 = ((ItemAsset)a).itemName + " (ID:" + sh5.Id + ")";
            }
            else
                i5 = Translate("no_more_products");

            if (sh6 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh6.Id);
                i6 = ((ItemAsset)a).itemName + " (ID:" + sh6.Id + ")";
            }
            else
                i6 = Translate("no_more_products");

            if (sh7 != null)
            {
                Asset a = Assets.find(EAssetType.ITEM, sh7.Id);
                i7 = ((ItemAsset)a).itemName + " (ID:" + sh7.Id + ")";
            }
            else
                i7 = Translate("no_more_products");

            EffectManager.askEffectClearByID(Configuration.Instance.OpenID, p.CSteamID);
            EffectManager.sendUIEffect(Configuration.Instance.Main1ID, 700, p.CSteamID, true, i0, i1, i2, i3);
            EffectManager.sendUIEffect(Configuration.Instance.Main2ID, 710, p.CSteamID, true, i4, i5, i6, i7);
        }

        public void ItemEffect(UnturnedPlayer p, ushort ID)
        {
            var i = Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == ID);

            if (i != null)
            {
                TShopComponent cp = p.GetComponent<TShopComponent>();
                Asset a = Assets.find(EAssetType.ITEM, ID);
                string cost = Translate("Buy_Price") + i.BuyCost + "|" + Translate("Sell_Price") + i.SellCost;
                cost = cost.Replace("|", "" + System.Environment.NewLine);

                string description = ((ItemAsset)a).itemDescription;

                string amt = cp.amt.ToString();
                cp.lookingid = i.Id;
                EffectManager.askEffectClearByID(Configuration.Instance.Main2ID, p.CSteamID);
                EffectManager.askEffectClearByID(Configuration.Instance.Main1ID, p.CSteamID);
                EffectManager.sendUIEffect(Configuration.Instance.BuySellID, 700, p.CSteamID, true, ((ItemAsset)a).itemName.ToString() + " (ID:" + ID + ")", cost, description, amt);
            }
            else
            {
                UnturnedChat.Say(p, Translate("item_isn't_added", Assets.find(EAssetType.ITEM, search).name, search), color: ErrorColor);
                return;
            }
        }

        public void button(Player player, string buttonName)
        {
            if (!Configuration.Instance.UIEnabled)
                return;

            UnturnedPlayer p = UnturnedPlayer.FromPlayer(player);
            TShopComponent cp = p.GetComponent<TShopComponent>();
            if (buttonName == "tshop_open")
            {
                cp.UIOpened = true;
                p.Player.serversideSetPluginModal(true);
                MainEffect(p);
            }
            if (buttonName == "tshop_b")
            {
                EffectManager.askEffectClearByID(Configuration.Instance.BuySellID, p.CSteamID);
                MainEffect(p);
            }
            if (buttonName == "tshop_exit")
            {
                cp.UIOpened = false;
                EffectManager.askEffectClearByID(Configuration.Instance.Main2ID, p.CSteamID);
                EffectManager.askEffectClearByID(Configuration.Instance.Main1ID, p.CSteamID);
                EffectManager.askEffectClearByID(Configuration.Instance.BuySellID, p.CSteamID);
                p.Player.serversideSetPluginModal(false);
                if (Configuration.Instance.OpenButtonEnabled)
                    EffectManager.sendUIEffect(Configuration.Instance.OpenID, 700, p.CSteamID, true);
            }

            #region Shop
            if (buttonName.ToLower() == "tshop_next")
            {
                if (cp.item0 < Configuration.Instance.ItemShop.Count)
                    cp.item0 = cp.item0 + 8;
                if (cp.item1 < Configuration.Instance.ItemShop.Count)
                    cp.item1 = cp.item1 + 8;
                if (cp.item2 < Configuration.Instance.ItemShop.Count)
                    cp.item2 = cp.item2 + 8;
                if (cp.item3 < Configuration.Instance.ItemShop.Count)
                    cp.item3 = cp.item3 + 8;
                if (cp.item4 < Configuration.Instance.ItemShop.Count)
                    cp.item4 = cp.item4 + 8;
                if (cp.item5 < Configuration.Instance.ItemShop.Count)
                    cp.item5 = cp.item5 + 8;
                if (cp.item6 < Configuration.Instance.ItemShop.Count)
                    cp.item6 = cp.item6 + 8;
                if (cp.item7 < Configuration.Instance.ItemShop.Count)
                    cp.item7 = cp.item7 + 8;

                MainEffect(p);
            }
            if (buttonName.ToLower() == "tshop_back")
            {
                if (cp.item0 >= 8)
                    cp.item0 = cp.item0 - 8;
                if (cp.item1 >= 9)
                    cp.item1 = cp.item1 - 8;
                if (cp.item2 >= 10)
                    cp.item2 = cp.item2 - 8;
                if (cp.item3 >= 11)
                    cp.item3 = cp.item3 - 8;
                if (cp.item4 >= 12)
                    cp.item4 = cp.item4 - 8;
                if (cp.item5 >= 13)
                    cp.item5 = cp.item5 - 8;
                if (cp.item6 >= 14)
                    cp.item6 = cp.item6 - 8;
                if (cp.item7 >= 15)
                    cp.item7 = cp.item7 - 8;

                MainEffect(p);
            }
            if (buttonName.ToLower() == "tshop_search")
            {
                if (search != null)
                {
                    if (Assets.find(EAssetType.ITEM, Convert.ToUInt16(search)) == null)
                    {
                        UnturnedChat.Say(p, Translate("item_isn't_found", search), color: ErrorColor);
                        return;
                    }

                    ushort id = Convert.ToUInt16(search);
                    ItemShop i = Configuration.Instance.ItemShop.FirstOrDefault(x => x.Id == id);

                    if (i == null)
                    {
                        Asset a = Assets.find(EAssetType.ITEM, id);
                        UnturnedChat.Say(p, Translate("item_isn't_added", ((ItemAsset)a).itemName, id), color: ErrorColor);
                        return;
                    }
                    else
                    {
                        ItemEffect(p, i.Id);
                    }
                }
                else
                {
                    UnturnedChat.Say(p, Translate("search_empty"));
                    return;
                }
            }
            if (buttonName.ToLower() == "tshop_item_0")
            {
                if (cp.item0 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item0];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_1")
            {
                if (cp.item1 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item1];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_2")
            {
                if (cp.item2 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item2];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_3")
            {
                if (cp.item3 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item3];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_4")
            {
                if (cp.item4 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item4];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_5")
            {
                if (cp.item5 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item5];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_6")
            {
                if (cp.item6 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item6];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_item_7")
            {
                if (cp.item7 < Configuration.Instance.ItemShop.Count)
                {
                    var i = Configuration.Instance.ItemShop[cp.item7];
                    ItemEffect(p, i.Id);
                }
            }
            if (buttonName.ToLower() == "tshop_buy")
            {
                if (cp.lookingid != 0 && cp.amt != 0)
                {
                    ChatManager.instance.askChat(p.CSteamID, (byte)EChatMode.LOCAL, string.Join(" ", "/shopbuy " + cp.lookingid + " " + cp.amt));
                }
            }
            if (buttonName.ToLower() == "tshop_sell")
            {
                if (cp.lookingid != 0 && cp.amt != 0)
                {
                    ChatManager.instance.askChat(p.CSteamID, (byte)EChatMode.LOCAL, string.Join(" ", "/shopsell " + cp.lookingid + " " + cp.amt));
                }
            }
            if (buttonName.ToLower() == "tshop_+")
            {
                cp.amt = cp.amt + 1;
                ItemEffect(p, cp.lookingid);
            }
            if (buttonName.ToLower() == "tshop_-")
            {
                if (cp.amt > 1)
                {
                    cp.amt = cp.amt - 1;
                    ItemEffect(p, cp.lookingid);
                }
            }
            #endregion
        }

        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                { "usage_buy", "[TShop] Usage: /shopbuy <ItemId> <Amount>" },
                { "usage_cost", "[TShop] Usage: /shopcost <ItemId>" },
                { "usage_sell", "[TShop] Usage: /shopsell <ItemId> <Amount>" },
                { "usage_list", "[TShop] Usage: /shoplist <Page>" },
                { "usage_add", "[TShop] Usage: /shopadd <ItemId> <Buyprice> <Sellprice>" },
                { "usage_chng", "[TShop] Usage: /shopchng <ItemId> <Buyprice> <Sellprice>" },
                { "usage_remove", "[TShop] Usage: /shopremove <ItemId>" },
                { "format_error", "[TShop] Unable to convert {0} to a number." },
                { "overflow_error", "[TShop] {0} is too big number.." },
                { "next_page", "[TShop] Next page: /shoplist {0} {1}." },
                { "unexistant_page", "[TShop] That page doesn't exist." },
                { "end_of_list", "[TShop] You have reached the end of the {0} shop list." },
                { "itemshop", "- {0} (ID: {1}, buy price: {2} and sell price: {3})" },
                { "not_enough_money", "[TShop] You not have enough money! You need {0} to buy!" },
                { "not_enough_items_to_sell", "[TShop] You not have enough items to sell. ({0}x {1})" },
                { "item_already_added", "[TShop] The {0} (ID: {1}) is already added to the shop." },
                { "item_isn't_added", "[TShop] The {0} (ID: {1}) item isn't added to the shop!" },
                { "item_isn't_found", "[TShop] You need to provide a valid item id. (ID: {0})" },
                { "successfully_buy", "[TShop] You successfully buy {0}x {1} (ID: {2}) for ${3}" },
                { "successfully_sell", "[TShop] You successfully sold the {0} item. your new balance is: {1}" },
                { "successfully_removed", "[TShop] You successfully removed the {0} (ID: {1}) item from the shop." },
                { "successfully_added", "[TShop] The {0} is successfully added to the shop! (ID: {1}, Buy price: {2}, Sell price: {3}" },
                { "successfully_changed", "[TShop] The {0} is successfully changed in the shop! (ID: {1}, New buy price: {2}, New sell price: {3}" },
                { "cost", "[TShop] The {0}'s ID: {1}, buy price: {2} and sell price: {3}" },
                { "shop_is_empty", "[TShop] The shop is empty" },
                { "no_more_products", "No more products" },
                { "WIP", "Work In Progess" },
                { "Buy_Price", "Buy price: " },
                { "Sell_Price", "Sell price: " }
            };
    }
}
