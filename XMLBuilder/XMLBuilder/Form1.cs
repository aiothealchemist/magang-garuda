using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLBuilder
{
	public partial class Form1 : Form
	{
		System.Xml.XmlDocument xmlDoc;
		string buttonPath, ingamePath, explodePath, projectilePath;
		string xmlPath;

		void write(SortedDictionary<string, string> data, SortedDictionary<string ,string> pricing, SortedDictionary<string ,string> sprites, string head)
		{
			xmlDoc = new System.Xml.XmlDocument();
			xmlDoc.Load(xmlPath);

			System.Xml.XmlElement newObject = xmlDoc.CreateElement(head);
			foreach (var item in data)
			{
				System.Xml.XmlElement element = xmlDoc.CreateElement(item.Key);
				element.InnerText = item.Value;
				newObject.AppendChild(element);
			}
			//pricing
			System.Xml.XmlElement price = xmlDoc.CreateElement("pricing");
			foreach (var item in pricing)
			{
				System.Xml.XmlElement element = xmlDoc.CreateElement(item.Key);
				element.InnerText = item.Value;
				price.AppendChild(element);
			}
			newObject.AppendChild(price);

			//sprites
			System.Xml.XmlElement sprite = xmlDoc.CreateElement("sprites");
			foreach (var item in sprites)
			{
				System.Xml.XmlElement element = xmlDoc.CreateElement(item.Key);
				element.InnerText = item.Value;
				price.AppendChild(element);
			}
			newObject.AppendChild(sprite);

			xmlDoc.DocumentElement.AppendChild(newObject);
			xmlDoc.Save(xmlPath);
		}

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				buttonPath = openFileDialog1.FileName;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				ingamePath = openFileDialog1.FileName;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				explodePath = openFileDialog1.FileName;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				projectilePath = openFileDialog1.FileName;
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			string head = string.Empty;
			SortedDictionary<string, string> data = new SortedDictionary<string, string>();
			SortedDictionary<string, string> pricing = new SortedDictionary<string, string>();
			SortedDictionary<string, string> sprites = new SortedDictionary<string, string>();
			//normal
			data.Add("name", name.Text);
			data.Add("description", desc.Text);
			//pricing
			if (gem.Text != string.Empty) pricing.Add("gem", gem.Text);
			if (coin.Text != string.Empty) pricing.Add("coin", coin.Text);
			if (realMoney.Text != string.Empty) pricing.Add("realMoney", realMoney.Text);
			//sprites
			if (buttonPath != string.Empty) sprites.Add("button", buttonPath);
			if (ingamePath != string.Empty) sprites.Add("ingame", ingamePath);
			if (explodePath != string.Empty) sprites.Add("explode", explodePath);
			if (projectilePath != string.Empty) sprites.Add("projectile", projectilePath);

			switch (tabContainer.SelectedIndex)
			{
				case 0:	//zombie
					head = "Zombie";
					data.Add("sisi",sisiChoice.Text);
					data.Add("baseSpeed",baseSpeed.Text);
					data.Add("baseHealth", baseHealth.Text);
					data.Add("baseDamage", baseDamage.Text);
					data.Add("isGaet", isGaet.Checked.ToString());
					break;
				case 1:	//weapon
					head = "Weapon";
					data.Add("isDariLangit", isDariLangit.Checked.ToString());
					data.Add("isParabolic", isParabolic.Checked.ToString());
					data.Add("blastRadius",blastRadius.Text);
					data.Add("initSpeed", initSpeed.Text);
					data.Add("fireRate", fireRate.Text);
					data.Add("baseDamage", baseDamageWeapon.Text);
					break;
				case 2:	//vehicle
					head = "Vehicle";
					data.Add("baseSpeed",baseSpeedVehicle.Text);
					data.Add("baseHealth", baseHealthVehicle.Text);
					break;
				case 3:	//powerup
					head = "Powerup";
					data.Add("isMultiplier", isMultiplier.Checked.ToString());
					data.Add("speedFactor", speedFactor.Text);
					data.Add("healthFactor", healthFactor.Text);
					data.Add("coinFactor", coinFactor.Text);
					data.Add("damageFactor", damageFactor.Text);
					break;
				case 4:	//part
					head = "Part";
					data.Add("baseSpeed",baseSpeedPart.Text);
					data.Add("baseHealth", baseHealthPart.Text);
					break;
				default:
					head = isAchievement.Checked ? "Achievement" : "Gem";
					break;
			}
			write(data, pricing, sprites, head);
		}
	}
}
