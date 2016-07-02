using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TileLib;

public class TerrainItem : MonoBehaviour {
	public	RawImage	image;
	public	Text		text;
	public	Button		button;

	private	TileBase	tilebase;

	public	void SetItem(TileItem item, TileBase tilebase) {
		this.image.texture = Resources.Load<Texture>(item.assetPath);
		this.text.text = Path.GetFileNameWithoutExtension(item.asset);
		this.tilebase = tilebase;
	}

	void OnEnable() {
		button.onClick.AddListener(OnClickButton);
	}

	void OnDisable() {
		button.onClick.RemoveListener(OnClickButton);
	}

	private	void OnClickButton() {
		SendMessageUpwards("OnClickItem", tilebase, SendMessageOptions.RequireReceiver);
	}
}
