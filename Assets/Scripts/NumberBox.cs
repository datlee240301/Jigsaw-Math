using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBox : MonoBehaviour {
    public int index = 0; // ID của ô
    int x = 0;
    int y = 0;
    private Action<int, int> swapFunc = null;

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> swapFunc) {
        this.index = index; // Gán ID cho ô
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(i, j);
        this.swapFunc = swapFunc;
    }

    public void UpdatePos(int i, int j) {
        x = i;
        y = j;
        this.gameObject.transform.localPosition = new Vector2(i, j);
    }

    public bool IsEmpty() {
        return index == 16; // Giả sử 16 là ID của ô trống
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && swapFunc != null) {
            swapFunc(x, y);
        }
    }
}
