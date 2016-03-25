using UnityEngine;
using System.Collections;

public class HorizontalLayoutor : MonoBehaviour {
	public	int cellWidth;
	public	int cellHeight;

	void Start() {
		Clear();
		Align();
	}

	public	void AddItem(RectTransform child) {
		child.SetParent(transform, false);
		child.localScale = Vector3.one;
	}

	public	void Align() {
		var t = this.transform as RectTransform;
		var num = t.childCount;
		var w = num * cellWidth;
		var ch = (int)((float)cellWidth / 2f - (float)w/2f);
		t.sizeDelta = new Vector2(w, 200);

		for (int i = 0 ; i < num ; i++) {
			var c = t.GetChild(i) as RectTransform;
			c.anchoredPosition = new Vector2(i * cellWidth + ch, 0);
		}
	}

	public	void Clear() {
		var t = this.transform as RectTransform;
		var num = t.childCount;
		for (int i = 0 ; i < num ; i++) {
			DestroyImmediate(t.GetChild(0).gameObject);
		}
	}
}
