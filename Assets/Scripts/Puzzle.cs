using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour {
    public NumberBox boxPrefabs;
    public NumberBox[,] boxes;
    public Sprite[] sprites1;
    public Sprite[] sprites2;

    public static int gridSize; // Kích thước lưới (4 hoặc 5)
    private Sprite[] selectedSprites; // Tập sprite được chọn

    private void Start() {
        PlayerPrefs.SetInt(StringManager.layoutId,1);
        // Kiểm tra layout dựa trên PlayerPrefs
        int layoutId = PlayerPrefs.GetInt(StringManager.layoutId);

        if (layoutId == 0) {
            gridSize = 4;
            selectedSprites = sprites1;
        } else if (layoutId == 1) {
            gridSize = 5;
            selectedSprites = sprites2;
        }

        // Khởi tạo kích thước mảng 2D dựa trên gridSize
        boxes = new NumberBox[gridSize, gridSize];

        Init();
    }

    void Init() {
        List<Vector2> availablePositions = new List<Vector2>();

        // Khởi tạo danh sách các vị trí có sẵn
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                availablePositions.Add(new Vector2(x, y));
            }
        }

        // Khởi tạo các box với vị trí ngẫu nhiên
        int n = 0;
        while (availablePositions.Count > 0) {
            // Chọn ngẫu nhiên một vị trí
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector2 pos = availablePositions[randomIndex];

            // Xóa vị trí này khỏi danh sách
            availablePositions.RemoveAt(randomIndex);

            // Tạo box tại vị trí ngẫu nhiên
            NumberBox box = Instantiate(boxPrefabs, pos, Quaternion.identity);
            box.Init((int)pos.x, (int)pos.y, n + 1, selectedSprites[n], ClickToSwap);
            boxes[(int)pos.x, (int)pos.y] = box;

            n++;
        }
    }

    void ClickToSwap(int x, int y) {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        if (dx == 0 && dy == 0) return; // Không có di chuyển hợp lệ

        var from = boxes[x, y];
        var target = boxes[x + dx, y + dy];
        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);

        // Sau khi swap, kiểm tra xem đã chiến thắng chưa
        CheckWinCondition();
    }

    int getDx(int x, int y) {
        if (x < gridSize - 1 && boxes[x + 1, y].IsEmpty()) {
            return 1;
        }
        if (x > 0 && boxes[x - 1, y].IsEmpty()) {
            return -1;
        }
        return 0;
    }

    int getDy(int x, int y) {
        if (y < gridSize - 1 && boxes[x, y + 1].IsEmpty()) {
            return 1;
        }
        if (y > 0 && boxes[x, y - 1].IsEmpty()) {
            return -1;
        }
        return 0;
    }

    // Hàm kiểm tra điều kiện chiến thắng
    void CheckWinCondition() {
        int expectedIndex = 1; // Bắt đầu từ 1
        for (int y = gridSize - 1; y >= 0; y--) {
            for (int x = 0; x < gridSize; x++) {
                if (boxes[x, y].index != expectedIndex) {
                    return; // Nếu không đúng thứ tự, thoát hàm
                }
                expectedIndex++;
            }
        }
        Debug.Log("Win"); // Nếu tất cả đúng thứ tự
    }
    private void Update() {
        CheckWinCondition();
    }
}

