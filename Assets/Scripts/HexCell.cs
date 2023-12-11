using UnityEngine;

public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

	public Color color;
	public string temp;
	public string getCoordinate(){
		return coordinates.X +","+coordinates.Z;
	}
}