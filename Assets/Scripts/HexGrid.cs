using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class HexGrid : MonoBehaviour
{

	int width = 6;
	int height = 6;
	public string path = null;

	Color defaultColor = Color.white;
	Color Lightblue = new Color(0, 255, 255);

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

	HexCell[,] cells;

	Canvas gridCanvas;
	HexMesh hexMesh;

	void Awake()
	{
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();



		// for (int z = 0, i = 0; z < height; z++) {
		// 	for (int x = 0; x < width; x++) {
		// 		CreateCell(x, z, i++,"test");
		// 	}
		// }

		drawMap(path);
	}

	void Start()
	{
		hexMesh.Triangulate(cells);
	}

	// public void ColorCell (Vector3 position, Color color) {
	// 	position = transform.InverseTransformPoint(position);
	// 	HexCoordinates coordinates = HexCoordinates.FromPosition(position);
	// 	int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
	// 	HexCell cell = cells[index];
	// 	cell.color = color;
	// 	hexMesh.Triangulate(cells);
	// }
	//根据（x，y）绘制单元格

	public void CreateCell(int x, int z, string temp)
	{
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[x,z] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

		switch (temp)
		{
			case "1":
				cell.color = Color.blue;
				break;
			case "2":
				cell.color = Color.green;
				break;
			case "3":
				cell.color = Color.yellow;
				break;
			default:
				cell.color = Lightblue;
				break;
		}

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = x + "\n" + z;
		cell.temp = temp;
	}

	public void drawMap(string path)
	{
		FileStream stream = new FileStream(path, FileMode.Open);
		StreamReader sr = new StreamReader(stream);
		string line, temp;
		line = null;
		int width = 0, height = 0;
		while ((temp = sr.ReadLine()) != null)
		{
			width = temp.Length;
			height++;
			line += temp;
		}
		stream.Close();
		sr.Close();
		cells = new HexCell[width, height];

		for (int z = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{

				CreateCell(x, z, line.Substring(0, 1));
				line = line.Substring(1);
			}
		}
	}
}