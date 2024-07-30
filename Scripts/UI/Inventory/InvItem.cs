using Godot;
using System;





namespace Manager.Inventory.Item{

    public partial class InvItem : Node
{
public enum ItemType{Weapon, Resource, Tool, Consumable};
public string ItemName;
public string ItemDescription;
public Texture ItemTexture;
public int MinItemPrice;
public int MaxItemPrice;
protected ItemType itemType;
}}