using UnityEngine;
using System.Collections;
using TileLib;

[RequireComponent(typeof(SpriteRenderer))]
public class TileItemRenderer : MonoBehaviour {
	private	Transform		_transfom;
	private	SpriteRenderer	_renderer;
	private	TileItemLink	_link;
	private	float			_depth;
	private	bool			_animation;
	private	int				_frame;
	private	float			_speed;
	private	float			_last;
	private	bool			_reserved;

	void Awake() {
		_transfom = transform;
		_renderer = GetComponent<SpriteRenderer> ();
		_link = null;
		_animation = false;
		_frame = 0;
		_reserved = false;
	}

	void LateUpdate() {
		if (_animation) {
			if (Time.realtimeSinceStartup - _last > _speed) {
				_frame = (_frame + 1) % _link.itemArray.Length;
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
		_link = null;
	}

	public	void SetTileItemLInk(TileItemLink link, Transform parent, float depth = 0f) {
		_transfom.SetParent (parent);
		_depth = depth;

		if (_link != link) {
			_link = link;
			_animation = _link.itemArray.Length > 1;
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
			item = _link.itemArray [_frame];
		} else {
			item = _link.item;
		}

		if (item != null) {
			_transfom.localPosition = new Vector3 ((item.pivotX + _link.x) / 128f, (item.pivotY + _link.y) / 128f, _depth);
			var sprite = Resources.Load<Sprite> (item.assetPath);
			if (_renderer.sprite != sprite) {
				_renderer.sprite = sprite;
				_renderer.flipX = _link.flipH;
				_renderer.flipY = _link.flipV;
			}
		}
	}
}
