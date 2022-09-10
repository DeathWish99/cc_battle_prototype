using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot(ElementName = "CasualtyRates")]
public class CasualtyRates
{
	[XmlElement(ElementName = "value")]
	public List<string> Value { get; set; }
	[XmlAttribute(AttributeName = "type")]
	public string Type { get; set; }
}

[XmlRoot(ElementName = "Unit")]
public class Unit
{
	[XmlElement(ElementName = "Team")]
	public string Team { get; set; }
	[XmlElement(ElementName = "Name")]
	public string Name { get; set; }
	[XmlElement(ElementName = "TroopCount")]
	public string TroopCount { get; set; }
	[XmlElement(ElementName = "CasualtyRates")]
	public CasualtyRates CasualtyRates { get; set; }
	[XmlElement(ElementName = "UnitType")]
	public string UnitType { get; set; }
	[XmlElement(ElementName = "MeleeRole")]
	public string MeleeRole { get; set; }
	[XmlElement(ElementName = "RangedArmour")]
	public string RangedArmour { get; set; }
	[XmlElement(ElementName = "Training")]
	public string Training { get; set; }
}

[XmlRoot(ElementName = "Units")]
public class UnitsXml
{
	[XmlElement(ElementName = "Unit")]
	public List<Unit> Unit { get; set; }
}

public class LoadUnitData : MonoBehaviour
{
	public static T ImportXml<T>(string path)
	{
		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (var stream = new FileStream(path, FileMode.Open))
			{
				return (T)serializer.Deserialize(stream);
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Exception importing xml file: " + e);
			return default;
		}
	}

    private void Awake()
    {
		UnitsXml xml = ImportXml<UnitsXml>(Path.Combine(Application.dataPath, "Scripts/UnitData/TestUnitData.xml"));

		Debug.Log(xml.Unit);
    }
}
