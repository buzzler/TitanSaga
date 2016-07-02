using UnityEngine;
using System.Collections;
using TileLib;

[RequireComponent(typeof(SpriteRenderer))]
public class TileItemRenderer : MonoBehaviour {
	private	Transform		_transfom;
	private	SpriteRenderer	_renderer;
	private	TileLibrary		_library;
	private	TileItemLink	_link;
	private	bool			_animation;
	private	int				_frame;
	private	string[]		_sequence;
	private	float			_speed;
	private	float			_last;
	private	bool			_reserved;

	void Awake() {
		_transfom = transform;
		_renderer = GetComponent<SpriteRenderer> ();
		_link = null;
		_animation = false;
		_frame = 0;
		_sequence = null;
		_reserved = false;
	}

	void LateUpdate() {
		if (_animation) {
			if (Time.realtimeSinceStartup - _last > _speed) {
				_frame = (_frame + 1) % _sequence.Length;
				_last = Time.realtimeSinceStartup;
				_reserved = true;
			}
		}

		if (_reserved) {
			ExeUpdateSprite ();
		}
	}

	void OnDestroy() {
		_transfom = null;
		_renderer = null;
		_library = null;
		_link = null;
		_sequence = null;
	}

	public	void SetTileItemLInk(TileLibrary library, TileItemLink link, Transform parent) {
		_transfom.SetParent (parent);
		_library = library;

		if (_link != link) {
			_link = link;
			_sequence = _link.GetSequence ();
			_animation = _sequence != null;
			_speed = 1f / _link.fps;
			_last = Time.realtimeSinceStartup - _speed;
		}
	}

	public	void UpdateSprite() {
		_reserved = true;
	}

	private	void ExeUpdateSprite() {
		TileItem item = null;
		if (_animation) {
			item = _library.FindItem (_sequence [_frame]) as TileItem;
		} else {
			item = _library.FindItem (_link.item) as TileItem;
		}

		if (item != null) {
			_transfom.localPosition = new Vector3 ((item.pivotX + _link.x) / 128f, (item.pivotY + _link.y) / 128f, 0f);
			var sprite = Resources.Load<Sprite> (item.assetPath);
			if (_renderer.sprite != sprite) {
				_renderer.sprite = sprite;
				_renderer.flipX = _link.flip;
			}
		}
	}
}
