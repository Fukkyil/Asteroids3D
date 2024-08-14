using Godot;
using System;

public partial class InvItem : Node
{
public enum ItemTypeEnum{Weapon, Ore, Biological, Power, Disposable, Consumable};
public enum ItemFactionEnum{Lunare, Cultists, Professionals, Insects, Scrappers, General};


public string ItemName;
public string ItemDescription;
public TextureRect ItemTextureNode;
public Texture2D ItemTexture;
public int ItemPrice;
public ItemTypeEnum ItemType;
public ItemFactionEnum ItemFaction;
}