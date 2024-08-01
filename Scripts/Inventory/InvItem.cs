using Godot;
using System;





namespace Manager.Inventory.Item{
public partial class InvItem : Node
{
public enum ItemTypeEnum{Weapon, Resource, Tool, Consumable};
public enum ItemFactionEnum{Lunare, Cultists, General};


public string ItemName;
public string ItemDescription;
public TextureRect ItemTextureNode;
public Texture ItemTexture;
public int ItemPrice;
public ItemTypeEnum ItemType;
public ItemFactionEnum ItemFaction;
}}