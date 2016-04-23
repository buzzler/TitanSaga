using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TileLib {

	public	class TileSimpleConst {
		public	const uint NONE = 0x00;
		public	const uint U = 0x01;
		public	const uint D = 0x02;
		public	const uint L = 0x04;
		public	const uint R = 0x08;
		public	const uint UL = 0x10;
		public	const uint UR = 0x20;
		public	const uint DL = 0x40;
		public	const uint DR = 0x80;

		public	const uint U_D = U|D;
		public	const uint U_L = U|L;
		public	const uint U_R = U|R;
		public	const uint D_L = D|L;
		public	const uint D_R = D|R;
		public	const uint L_R = L|R;
		public	const uint U_D_L = U|D|L;
		public	const uint U_D_R = U|D|R;
		public	const uint U_L_R = U|L|R;
		public	const uint D_L_R = D|L|R;
		public	const uint U_D_L_R = U|D|L|R;
	}

	public	class TileComplexConst {
		public	const uint NONE = 0x00;
		public	const uint U = 0x01;
		public	const uint D = 0x02;
		public	const uint L = 0x04;
		public	const uint R = 0x08;
		public	const uint UL = 0x10;
		public	const uint UR = 0x20;
		public	const uint DL = 0x40;
		public	const uint DR = 0x80;

//		public	const uint U_1C_EQ = UL;
//		public	const uint D_1C_EQ = DR;
//		public	const uint L_1C_EQ = DL;
//		public	const uint R_1C_EQ = UR;
//		public	const uint U_1S_AND = U;
//		public	const uint U_1S_XOR = D|DL|DR;
//		public	const uint D_1S_AND = D;
//		public	const uint D_1S_XOR = U|UL|UR;
//		public	const uint L_1S_AND = L;
//		public	const uint L_1S_XOR = R|UR|DR;
//		public	const uint R_1S_AND = R;
//		public	const uint R_1S_XOR = L|UL|DL;
//		public	const uint LU_1C1S_AND = U|DL;
//		public	const uint LU_1C1S_XOR = D|L|R|DR;
//		public	const uint DU_1C1S_AND = U|DR;
//		public	const uint DU_1C1S_XOR = D|L|R|DL;
//		public	const uint RD_1C1S_AND = D|UR;
//		public	const uint RD_1C1S_XOR = U|L|R|UL;
//		public	const uint UD_1C1S_AND = D|UL;
//		public	const uint UD_1C1S_XOR = U|L|R|UR;
//		public	const uint RL_1C1S_AND = L|UR;
//		public	const uint RL_1C1S_XOR = U|D|R|DR;
//		public	const uint DL_1C1S_AND = L|DR;
//		public	const uint DL_1C1S_XOR = U|D|R|UR;
//		public	const uint LR_1C1S_AND = R|DL;
//		public	const uint LR_1C1S_XOR = U|D|L|UL;
//		public	const uint UR_1C1S_AND = R|UL;
//		public	const uint UR_1C1S_XOR = U|D|L|DL;
	}

	public	enum TileType {
		Item,
		Simple,
		Complex,
		Direction
	}

	public	enum TileFace {
		Up = 0,
		Down = 180,
		Left = 270,
		Right = 90
	}

	[XmlRoot("Config")]
	public	class TileConfig {
		[XmlElement("Library")]
		public	TileLibrary	library;
		[XmlElement("Tutorial")]
		public	TileTutorial tutorial;

		public	void Hashing() {
			library.Hashing();
			tutorial.Hashing();
		}
	}

	public class TileLibrary {
		[XmlAttribute("path")]
		public	string			path;
		[XmlAttribute("default")]
		public	string			unit;
		[XmlElement("Category")]
		public	TileCategory[]	categories;
		[XmlElement("Simple")]
		public	TileSimple[]	simples;
		[XmlElement("Complex")]
		public	TileComplex[]	complexs;
		[XmlElement("Direction")]
		public	TileDirection[]	directions;

		private	Dictionary<string, TileCategory>	_dictionaryCategory;
		private	Dictionary<string, TileBase>		_dictionaryItem;

		public	TileLibrary() {
			categories			= new TileCategory[0];
			simples				= new TileSimple[0];
			complexs			= new TileComplex[0];
			directions			= new TileDirection[0];
			_dictionaryCategory	= new Dictionary<string, TileCategory>();
			_dictionaryItem		= new Dictionary<string, TileBase>();
		}

		public	void Hashing() {
			_dictionaryCategory.Clear();
			_dictionaryItem.Clear();

			for (int i = categories.Length-1 ; i >= 0 ; i--) {
				TileCategory category = categories[i];
				category.Hashing();
				_dictionaryCategory.Add(category.name, category);
				for (int j = category.items.Length-1 ; j >= 0 ; j--) {
					TileItem item = category.items[j];
					item.library = this;
					_dictionaryItem.Add(item.id, item);
				}
			}

			for (int i = simples.Length-1 ; i >= 0 ; i--) {
				TileSimple simple = simples[i];
				simple.library = this;
				_dictionaryItem.Add(simple.id, simple);
			}

			for (int i = complexs.Length-1 ; i >= 0 ; i--) {
				TileComplex complex = complexs[i];
				complex.library = this;
				_dictionaryItem.Add(complex.id, complex);
			}

			for (int i = directions.Length-1 ; i >= 0 ; i--) {
				TileDirection direction = directions[i];
				direction.library = this;
				_dictionaryItem.Add(direction.id, direction);
			}
		}

		public	TileCategory FindCategory(string name) {
			if (_dictionaryCategory.ContainsKey(name)) {
				return _dictionaryCategory[name];
			} else {
				return null;
			}
		}

		public	TileBase FindItem(string id) {
			if (_dictionaryItem.ContainsKey(id)) {
				return _dictionaryItem[id];
			} else {
				return null;
			}
		}

		public	TileItem unitItem {
			get {
				return FindItem(unit) as TileItem;
			}
		}
	}


	public	class TileCategory {
		[XmlAttribute("name")]
		public	string							name;
		[XmlElement("Item")]
		public	TileItem[]						items;
		private	Dictionary<string, TileItem>	_dictionary;

		public	TileCategory() {
			name		= string.Empty;
			items		= new TileItem[0];
			_dictionary	= new Dictionary<string, TileItem>();
		}

		public	void Hashing() {
			_dictionary.Clear();
			for (int i = items.Length-1 ; i>=0 ; i--) {
				TileItem item = items[i];
				_dictionary.Add(item.id, item);
			}
		}

		public	TileItem FindItem(string id) {
			if (_dictionary.ContainsKey(id)) {
				return _dictionary[name];
			} else {
				return null;
			}
		}
	}

	public	class TileBase {
		[XmlAttribute("id")]
		public	string	id;
		public	TileType type;
		public	TileLibrary library;


	}

	public	class TileItem : TileBase {
		[XmlAttribute("asset")]
		public	string	asset;
		[XmlAttribute("cube")]
		public	bool	cube;
		[XmlAttribute("pivotX")]
		public	float	pivotX;
		[XmlAttribute("pivotY")]
		public	float	pivotY;

		public	TileItem() {
			type = TileType.Item;
		}

		public	string assetPath {
			get {
				return Path.Combine(library.path, asset);
			}
		}
	}

	public	class TileSimple : TileBase {
		[XmlElement("None")]
		public	TileItemLink	none;
		[XmlElement("U")]
		public	TileItemLink	up;
		[XmlElement("D")]
		public	TileItemLink	down;
		[XmlElement("L")]
		public	TileItemLink	left;
		[XmlElement("R")]
		public	TileItemLink	right;
		[XmlElement("UD")]
		public	TileItemLink	updown;
		[XmlElement("UL")]
		public	TileItemLink	upleft;
		[XmlElement("UR")]
		public	TileItemLink	upright;
		[XmlElement("DL")]
		public	TileItemLink	downleft;
		[XmlElement("DR")]
		public	TileItemLink	downright;
		[XmlElement("LR")]
		public	TileItemLink	leftright;
		[XmlElement("UDL")]
		public	TileItemLink	updownleft;
		[XmlElement("UDR")]
		public	TileItemLink	updownright;
		[XmlElement("ULR")]
		public	TileItemLink	upleftright;
		[XmlElement("DLR")]
		public	TileItemLink	downleftright;
		[XmlElement("UDLR")]
		public	TileItemLink	updownleftright;

		public	TileSimple() {
			type = TileType.Simple;
		}
	}

	public	class TileComplex : TileBase {
		[XmlElement("None")]
		public	TileItemLink	none;
		[XmlElement("U_1C")]
		public	TileItemLink	up_1c;
		[XmlElement("D_1C")]
		public	TileItemLink	down_1c;
		[XmlElement("L_1C")]
		public	TileItemLink	left_1c;
		[XmlElement("R_1C")]
		public	TileItemLink	right_1c;
		[XmlElement("U_1S")]
		public	TileItemLink	up_1s;
		[XmlElement("D_1S")]
		public	TileItemLink	down_1s;
		[XmlElement("L_1S")]
		public	TileItemLink	left_1s;
		[XmlElement("R_1S")]
		public	TileItemLink	right_1s;
		[XmlElement("LU_1C1S")]
		public	TileItemLink	leftup_1c1s;
		[XmlElement("DU_1C1S")]
		public	TileItemLink	downup_1c1s;
		[XmlElement("RD_1C1S")]
		public	TileItemLink	rightdown_1c1s;
		[XmlElement("UD_1C1S")]
		public	TileItemLink	updown_1c1s;
		[XmlElement("RL_1C1S")]
		public	TileItemLink	rightleft_1c1s;
		[XmlElement("DL_1C1S")]
		public	TileItemLink	downleft_1c1s;
		[XmlElement("LR_1C1S")]
		public	TileItemLink	leftright_1c1s;
		[XmlElement("UR_1C1S")]
		public	TileItemLink	upright_1c1s;
		[XmlElement("DLU_2C1S")]
		public	TileItemLink	downleftup_2c1s;
		[XmlElement("URD_2C1S")]
		public	TileItemLink	uprightdown_2c1s;
		[XmlElement("DRL_2C1S")]
		public	TileItemLink	downrightleft_2c1s;
		[XmlElement("ULR_2C1S")]
		public	TileItemLink	upleftright_2c1s;
		[XmlElement("DL_2C")]
		public	TileItemLink	downleft_2c;
		[XmlElement("UR_2C")]
		public	TileItemLink	upright_2c;
		[XmlElement("UD_2C")]
		public	TileItemLink	updown_2c;
		[XmlElement("LR_2C")]
		public	TileItemLink	leftright_2c;
		[XmlElement("UL_2C")]
		public	TileItemLink	upleft_2c;
		[XmlElement("DR_2C")]
		public	TileItemLink	downright_2c;
		[XmlElement("UR_2S")]
		public	TileItemLink	upright_2s;
		[XmlElement("DR_2S")]
		public	TileItemLink	downright_2s;
		[XmlElement("DL_2S")]
		public	TileItemLink	downleft_2s;
		[XmlElement("UL_2S")]
		public	TileItemLink	upleft_2s;
		[XmlElement("UD_2S")]
		public	TileItemLink	updown_2s;
		[XmlElement("LR_2S")]
		public	TileItemLink	leftright_2s;
		[XmlElement("UDR_1C2S")]
		public	TileItemLink	updownright_1c2s;
		[XmlElement("RDL_1C2S")]
		public	TileItemLink	rightdownleft_1c2s;
		[XmlElement("DUL_1C2S")]
		public	TileItemLink	downupleft_1c2s;
		[XmlElement("LUR_1C2S")]
		public	TileItemLink	leftupright_1c2s;
		[XmlElement("DLR_3C")]
		public	TileItemLink	downleftright_3c;
		[XmlElement("UDR_3C")]
		public	TileItemLink	updownright_3c;
		[XmlElement("UDL_3C")]
		public	TileItemLink	updownleft_3c;
		[XmlElement("ULR_3C")]
		public	TileItemLink	upleftright_3c;
		[XmlElement("DLR_3S")]
		public	TileItemLink	downleftright_3s;
		[XmlElement("ULR_3S")]
		public	TileItemLink	upleftright_3s;
		[XmlElement("UDR_3S")]
		public	TileItemLink	updownright_3s;
		[XmlElement("UDL_3S")]
		public	TileItemLink	updownleft_3s;
		[XmlElement("UDLR_4C")]
		public	TileItemLink	updownleftright_4c;
		[XmlElement("UDLR_4S")]
		public	TileItemLink	updownleftright_4s;

		public	TileComplex() {
			type = TileType.Complex;
		}
	}

	public	class TileDirection : TileBase {
		[XmlElement("U")]
		public	TileItemLink	up;
		[XmlElement("D")]
		public	TileItemLink	down;
		[XmlElement("L")]
		public	TileItemLink	left;
		[XmlElement("R")]
		public	TileItemLink	right;

		public	TileDirection() {
			type = TileType.Direction;
		}
	}

	public	class TileItemLink {
		[XmlAttribute("item")]
		public	string			item;
		[XmlElement("Child")]
		public	TileItemLink[]	children;
	}

	public	class TileTutorial {
		[XmlElement("Map")]
		public	TileMap[]	maps;
		private	Dictionary<string, TileMap>	_dictionaryMap;

		public	TileTutorial() {
			maps = new TileMap[0];
			_dictionaryMap = new Dictionary<string, TileMap>();
		}

		public	void Hashing() {
			_dictionaryMap.Clear();
			for (int i = maps.Length-1 ; i >= 0 ; i--) {
				TileMap map = maps[i];
				map.Hashing();
				_dictionaryMap.Add(map.id, map);
			}
		}

		public	TileMap FindItem(string id) {
			if (_dictionaryMap.ContainsKey(id)) {
				return _dictionaryMap[id];
			} else {
				return null;
			}
		}
	}

	public	class TileMap {
		[XmlAttribute("id")]
		public	string	id;
		[XmlElement("Terrain")]
		public	TileTerrain[]	terrains;
		[XmlAttribute("width")]
		public	int width;
		[XmlAttribute("height")]
		public	int height;
		[XmlAttribute("depth")]
		public	int depth;
		private List<List<List<TileTerrain>>> _tlist;

		public	TileMap() {
			this.id = string.Empty;
			this.terrains = new TileTerrain[0];
			this.width = 1;
			this.height = 1;
			this.depth = 1;
		}

		public	TileMap Clone() {
			var c = new TileMap();
			c.id = this.id;
			c.width = this.width;
			c.height = this.height;
			c.depth = this.depth;

			var len = this.terrains.Length;
			c.terrains = new TileTerrain[len];
			System.Array.Copy(this.terrains, c.terrains, len);

			return c;
		}

		public	void Hashing() {
			var wlist = new List<List<List<TileTerrain>>>(width);
			for (int i = 0 ; i < width ; i++) {
				var hlist = new List<List<TileTerrain>>(height);
				wlist.Add(hlist);
				for (int j = 0 ; j < height ; j++) {
					var dlist = new List<TileTerrain>(depth);
					hlist.Add(dlist);
					for (int k = 0 ; k < depth ; k++) {
						dlist.Add(null);
					}
				}
			}

			_tlist = wlist;

			for (int i = terrains.Length-1 ; i >= 0 ; i--) {
				var terrain = terrains[i];
				_tlist[terrain.x][terrain.y][terrain.z] = terrain;
				terrain.SetMap (this);
			}
		}

		public	bool AddTerrain(TileTerrain terrain) {
			if (_tlist [terrain.x] [terrain.y] [terrain.z] == null) {
				_tlist [terrain.x] [terrain.y] [terrain.z] = terrain;
				terrain.SetMap (this);
				// TODO : add terrain from terrains array Generic
				System.Array.Resize<TileTerrain> (ref terrains, terrains.Length + 1);
				terrains [terrains.Length - 1] = terrain;
				return true;
			} else {
				Debug.LogWarningFormat ("Conflict x:{0}, y:{1}, z:{2}", terrain.x, terrain.y, terrain.z);
				return false;
			}
		}

		public	void RemoveTerrain(TileTerrain terrain) {
			if (_tlist [terrain.x] [terrain.y] [terrain.z] == terrain) {
				_tlist [terrain.x] [terrain.y] [terrain.z] = null;
				terrain.SetMap (null);
				// TODO : remove terrain from terrains array Generic
				terrains = System.Array.FindAll<TileTerrain>(terrains, (TileTerrain a)=>{
					return a != terrain;
				});
			}
		}

		public	TileTerrain GetTerrain(int x, int y, int z) {
			if (x < 0 || x >= width || y < 0 || y >= height || z < 0 || z >= depth) {
				return null;
			} else {
				return _tlist[x][y][z];
			}
		}
	}

	public	class TileTerrain {
		[XmlAttribute("item")]
		public	string		item;
		[XmlAttribute("x")]
		public	float		xf;
		[XmlAttribute("y")]
		public	float		yf;
		[XmlAttribute("z")]
		public	float		zf;
		[XmlAttribute("face")]
		public	TileFace	face;
		private	TileMap		_map;

		public	TileTerrain() {
			this.item = string.Empty;
			this.face = TileFace.Up;
			this._map = null;
			SetPosition (0f, 0f, 0f);
		}

		public	TileTerrain(string item, float x, float y, float z) {
			this.item = item;
			this.face = TileFace.Up;
			this._map = null;
			SetPosition (x, y, z);
		}

		public	void SetMap(TileMap map) {
			_map = map;
		}

		public	TileMap GetMap() {
			return _map;
		}

		public	void SetPosition(float x, float y, float z) {
			this.xf = x;
			this.yf = y;
			this.zf = z;
		}

		public	void SetPosition(Vector3 position) {
			SetPosition (position.x, position.y, position.z);
		}

		public	int x {
			get {
				return (int)xf;
			}
		}

		public	int y {
			get {
				return (int)yf;
			}
		}

		public	int z {
			get {
				return (int)zf;
			}
		}

		public	TileTerrain u {
			get {
				if (_map != null) {
					return _map.GetTerrain (x, y, z - 1);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain d {
			get {
				if (_map != null) {
					return _map.GetTerrain (x, y, z + 1);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain l {
			get {
				if (_map != null) {
					return _map.GetTerrain (x - 1, y, z);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain r {
			get {
				if (_map != null) {
					return _map.GetTerrain (x + 1, y, z);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain ul {
			get {
				if (_map != null) {
					return _map.GetTerrain (x - 1, y, z - 1);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain ur {
			get {
				if (_map != null) {
					return _map.GetTerrain (x + 1, y, z - 1);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain dl {
			get {
				if (_map != null) {
					return _map.GetTerrain (x - 1, y, z + 1);
				} else {
					return null;
				}
			}
		}

		public	TileTerrain dr {
			get {
				if (_map != null) {
					return _map.GetTerrain (x + 1, y, z + 1);
				} else {
					return null;
				}
			}
		}

		public	List<TileItem> GetTileItem(TileLibrary library) {
			TileBase tb = library.FindItem(item);

			List<TileItem> list = new List<TileItem>(8);
			switch(tb.type) {
			case TileType.Item:
				list.Add(tb as TileItem);
				break;
			case TileType.Simple:
				GetTileItemForSimple(library, tb as TileSimple, list);
				break;
			case TileType.Complex:
				GetTileItemForComplex(library, tb as TileComplex, list);
				break;
			case TileType.Direction:
				GetTileItemForDirection(library, tb as TileDirection, list);
				break;
			}

			return list;
		}

		private	void GetTileItemForSimple(TileLibrary library, TileSimple simple, List<TileItem> list) {
			var up = u;
			var down = d;
			var left = l;
			var right = r;
			uint result = 0;
			string tbid = simple.id;

			if (up != null && tbid == library.FindItem(up.item).id) {
				result |= TileSimpleConst.U;
			}
			if (down != null && tbid == library.FindItem(down.item).id) {
				result |= TileSimpleConst.D;
			}
			if (left != null && tbid == library.FindItem(left.item).id) {
				result |= TileSimpleConst.L;
			}
			if (right != null && tbid == library.FindItem(right.item).id) {
				result |= TileSimpleConst.R;
			}

			switch (result) {
			case TileSimpleConst.U:
				GetAllTileItem(library, simple.up, list);				break;
			case TileSimpleConst.D:
				GetAllTileItem(library, simple.down, list);				break;
			case TileSimpleConst.L:
				GetAllTileItem(library, simple.left, list);				break;
			case TileSimpleConst.R:
				GetAllTileItem(library, simple.right, list);			break;
			case TileSimpleConst.U_D:
				GetAllTileItem(library, simple.updown, list);			break;
			case TileSimpleConst.U_L:
				GetAllTileItem(library, simple.upleft, list);			break;
			case TileSimpleConst.U_R:
				GetAllTileItem(library, simple.upright, list);			break;
			case TileSimpleConst.D_L:
				GetAllTileItem(library, simple.downleft, list);			break;
			case TileSimpleConst.D_R:
				GetAllTileItem(library, simple.downright, list);		break;
			case TileSimpleConst.L_R:
				GetAllTileItem(library, simple.leftright, list);		break;
			case TileSimpleConst.U_D_L:
				GetAllTileItem(library, simple.updownleft, list);		break;
			case TileSimpleConst.U_D_R:
				GetAllTileItem(library, simple.updownright, list);		break;
			case TileSimpleConst.U_L_R:
				GetAllTileItem(library, simple.upleftright, list);		break;
			case TileSimpleConst.D_L_R:
				GetAllTileItem(library, simple.downleftright, list);	break;
			case TileSimpleConst.U_D_L_R:
				GetAllTileItem(library, simple.updownleftright, list);	break;
			default:
				GetAllTileItem(library, simple.none, list);				break;
			}
		}

		private	void GetTileItemForComplex(TileLibrary library, TileComplex complex, List<TileItem> list) {
			var up = u;
			var down = d;
			var left = l;
			var right = r;
			var north = ul;
			var south = dr;
			var east = ur;
			var west = dl;
			uint result = 0;
			string tbid = complex.id;

			if (up != null && tbid != library.FindItem(up.item).id) {
				result |= TileComplexConst.U;
			}
			if (down != null && tbid != library.FindItem(down.item).id) {
				result |= TileComplexConst.D;
			}
			if (left != null && tbid != library.FindItem(left.item).id) {
				result |= TileComplexConst.L;
			}
			if (right != null && tbid != library.FindItem(right.item).id) {
				result |= TileComplexConst.R;
			}
			if (north != null && tbid != library.FindItem (north.item).id) {
				result |= TileComplexConst.UL;
			}
			if (south != null && tbid != library.FindItem (south.item).id) {
				result |= TileComplexConst.DR;
			}
			if (east != null && tbid != library.FindItem (east.item).id) {
				result |= TileComplexConst.UR;
			}
			if (west != null && tbid != library.FindItem (west.item).id) {
				result |= TileComplexConst.DL;
			}

			switch (result) {
			case TileComplexConst.NONE:
				GetAllTileItem (library, complex.none, list);
				return;
			case TileComplexConst.UL:
				GetAllTileItem (library, complex.up_1c, list);
				return;
			case TileComplexConst.DR:
				GetAllTileItem (library, complex.down_1c, list);
				return;
			case TileComplexConst.DL:
				GetAllTileItem (library, complex.left_1c, list);
				return;
			case TileComplexConst.UR:
				GetAllTileItem (library, complex.right_1c, list);
				return;
			case TileComplexConst.U:
			case TileComplexConst.U|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.UL|TileComplexConst.UR:	
				GetAllTileItem (library, complex.up_1s, list);
				return;
			case TileComplexConst.D:
			case TileComplexConst.D|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.down_1s, list);
				return;
			case TileComplexConst.L:
			case TileComplexConst.L|TileComplexConst.UL:
			case TileComplexConst.L|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL:
				GetAllTileItem (library, complex.left_1s, list);
				return;
			case TileComplexConst.R:
			case TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.right_1s, list);
				return;
			case TileComplexConst.U|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.UL|TileComplexConst.UR:
				GetAllTileItem (library, complex.leftup_1c1s, list);
				return;
			case TileComplexConst.U|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.DR|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.DR|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.UR:
				GetAllTileItem (library, complex.downup_1c1s, list);
				return;
			case TileComplexConst.D|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.rightdown_1c1s, list);
				return;
			case TileComplexConst.D|TileComplexConst.UL:
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updown_1c1s, list);
				return;
			case TileComplexConst.L|TileComplexConst.UR:
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.UL:
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.UL|TileComplexConst.DL:
				GetAllTileItem (library, complex.rightleft_1c1s, list);
				return;
			case TileComplexConst.L|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UL:
			case TileComplexConst.L|TileComplexConst.DR|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.DL:
				GetAllTileItem (library, complex.downleft_1c1s, list);
				return;
			case TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.leftright_1c1s, list);
				return;
			case TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.upright_1c1s, list);
				return;
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.DR|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.DR|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.DL|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.UR:
				GetAllTileItem (library, complex.downleftup_2c1s, list);
				return;
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.uprightdown_2c1s, list);
				return;
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR|TileComplexConst.UL:
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.DL:
				GetAllTileItem (library, complex.downrightleft_2c1s, list);
				return;
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.upleftright_2c1s, list);
				return;
			case TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.downleft_2c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR:
				GetAllTileItem (library, complex.upright_2c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updown_2c, list);
				return;
			case TileComplexConst.DL|TileComplexConst.UR:
				GetAllTileItem (library, complex.leftright_2c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.DL:
				GetAllTileItem (library, complex.upleft_2c, list);
				return;
			case TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.downright_2c, list);
				return;
			case TileComplexConst.U|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.upright_2s, list);
				return;
			case TileComplexConst.D|TileComplexConst.R:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.downright_2s, list);
				return;
			case TileComplexConst.D|TileComplexConst.L:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.UL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.UL|TileComplexConst.DR:
				GetAllTileItem (library, complex.downleft_2s, list);
				return;
			case TileComplexConst.U|TileComplexConst.L:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.UL|TileComplexConst.UR:
				GetAllTileItem (library, complex.upleft_2s, list);
				return;
			case TileComplexConst.U|TileComplexConst.D:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updown_2s, list);
				return;
			case TileComplexConst.L|TileComplexConst.R:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.leftright_2s, list);
				return;
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownright_1c2s, list);
				return;
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.UL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.rightdownleft_1c2s, list);
				return;
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.DR|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.UR:
				GetAllTileItem (library, complex.downupleft_1c2s, list);
				return;
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.UL|TileComplexConst.DR|TileComplexConst.UR:
				GetAllTileItem (library, complex.leftupright_1c2s, list);
				return;
			case TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.downleftright_3c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownright_3c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownleft_3c, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
				GetAllTileItem (library, complex.upleftright_3c, list);
				return;
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.downleftright_3s, list);
				return;
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.upleftright_3s, list);
				return;
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownright_3s, list);
				return;
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownleft_3s, list);
				return;
			case TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownleftright_4c, list);
				return;
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
			case TileComplexConst.U|TileComplexConst.D|TileComplexConst.L|TileComplexConst.R|TileComplexConst.UL|TileComplexConst.UR|TileComplexConst.DL|TileComplexConst.DR:
				GetAllTileItem (library, complex.updownleftright_4s, list);
				return;
			}
			GetAllTileItem (library, complex.none, list);
		}

		private	void GetTileItemForDirection(TileLibrary library, TileDirection direction, List<TileItem> list) {
			switch (face) {
			case TileFace.Up:
				GetAllTileItem(library, direction.up, list);	break;
			case TileFace.Down:
				GetAllTileItem(library, direction.down, list);	break;
			case TileFace.Left:
				GetAllTileItem(library, direction.left, list);	break;
			case TileFace.Right:
				GetAllTileItem(library, direction.right, list);	break;
			}
		}

		private	void GetAllTileItem(TileLibrary library, TileItemLink link, List<TileItem> list) {
			if (!string.IsNullOrEmpty(link.item)) {
				list.Add(library.FindItem(link.item) as TileItem);
			}
			int total = link.children != null ? link.children.Length : 0;
			for (int i = 0 ; i < total ; i++) {
				GetAllTileItem(library, link.children[i], list);
			}
		}
	}
}
