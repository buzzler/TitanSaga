using UnityEngine;
using System.Collections.Generic;
using TileLib;

public class TileItemGroup : MonoBehaviour {
	private	Transform			_transform;
	private	TileLibrary			_library;
	private	ITileObject			_object;
	private	TileItemRenderer[]	_renderers;

	void OnDestroy() {
		_transform = null;
		_library = null;
		_object = null;
		_renderers = null;
	}

	public	void SetTileObject(TileLibrary library, ITileObject obj, Transform parent) {
		_transform = transform;
		_library = library;
		_transform.SetParent (parent);

		if (_object != obj) {
			_object = obj;
			ResetSprite ();
		}
	}

	public	void UpdateSprite(Vector3 position) {
		ResetSprite ();
		_transform.localPosition = position;
		for (int i = _renderers.Length - 1; i >= 0; i--) {
			_renderers [i].UpdateSprite ();
		}
	}

	private	void ResetSprite() {
		if (_object == null || _library == null) {
			return;
		}

		var links = _object.GetTileItemLink (_library);
		var count = links.Count;

		if (_renderers == null) {
			var list = new List<TileItemRenderer> ();
			for (int i = 0; i < count; i++) {
				list.Add (new GameObject (i.ToString ()).AddComponent<TileItemRenderer> ());
			}
			_renderers = list.ToArray ();
		} else if (_renderers.Length > count) {
			for (int i = _renderers.Length - 1; i >= count; i--) {
				DestroyImmediate (_renderers [i]);
			}
			System.Array.Resize<TileItemRenderer> (ref _renderers, count);
		} else if (_renderers.Length < count) {
			var start = _renderers.Length;
			System.Array.Resize<TileItemRenderer> (ref _renderers, count);
			for (int i = start; i < count; i++) {
				var tir = new GameObject (i.ToString ()).AddComponent<TileItemRenderer> ();
				_renderers [i] = tir;
			}
		}

		if (_renderers != null) {
			for (int i = 0; i < count; i++) {
				_renderers [i].SetTileItemLInk (_library, links [i], _transform);
			}
		}
	}
}
