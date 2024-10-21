using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	public List<scene> scenes = new List<scene>();

}
[Serializable]
public class scene
{

	public List<Item> itemlist = new List<Item>();
	public List<Spawn> spawnlist = new List<Spawn>();
	//[SerializeField] public Transform[] SpawnPoints;

	[Serializable]
	public class Item
	{
		public int id;
		public GameObject item;

	}
	[Serializable]
	public class Spawn
	{
		public Transform[] SpawnPoints;
	}

}
