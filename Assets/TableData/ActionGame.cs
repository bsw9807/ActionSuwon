using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ActionGame : ScriptableObject
{
	public List<TableTip> TipMess; // Replace 'EntityType' to an actual type that is serializable.
	public List<TableItem> ItemData; // Replace 'EntityType' to an actual type that is serializable.
	public List<TableMonster> MonsterData; // Replace 'EntityType' to an actual type that is serializable.
}
