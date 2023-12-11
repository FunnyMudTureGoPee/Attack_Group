using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    public Material mat;
    public Color col = Color.red;
    public Vector3 pos1 { get; set; }
    public Vector3 pos2 { get; set; }
    private bool IsReady = false;
    private ArrayList pointList;
    private ArrayList breakpointList;
    private int index = 0;

    List<List<Vector3>> vector3s;
    // Use this for initialization
    public LineRenderer lineRenderer;

    public float ropeWidth = 0.1f;
    public float ropeResolution = 0.5f;


    void Start()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        mat.color = col;
        pointList = new ArrayList();
        breakpointList = new ArrayList();
    }

    //Update is called once per frame
    void Update()
    {
        if (!IsReady)
        {
            int numPoints = Mathf.CeilToInt(Vector3.Distance(pos1, pos2) * ropeResolution);
            lineRenderer.positionCount = numPoints;

            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (float)(numPoints - 1);
                Vector3 pointPosition = Vector3.Lerp(pos1, pos2, t);
                lineRenderer.SetPosition(i, pointPosition);

            }
            IsReady = true;
        }
    }
    // void OnPostRender()
    // {
    //     if(IsReady)
    //     {
    //         GL.PushMatrix();
    //         mat.SetPass(0);
    //         GL.LoadOrtho();
    //         for (int i = 0; i < pointList.Count-1; i++)
    //         {
    //             for (int j = 0; j < breakpointList.Count;j++ )
    //             {
    //                 if(i==(int)(breakpointList[j]))
    //                 {
    //                     i++;

    //                 }
    //             }
    //             Debug.Log(pointList[i].ToString());
    //             GL.Begin(GL.LINES);
    //             GL.Color(col);
    //             GL.Vertex3(((Vector3)pointList[i]).x , ((Vector3)pointList[i]).y , ((Vector3)pointList[i]).z);
    //             GL.Vertex3(((Vector3)pointList[i+1]).x , ((Vector3)pointList[i+1]).y , ((Vector3)pointList[i+1]).z);
    //             GL.End();
    //         }              
    //         GL.PopMatrix();
    //         Debug.Log("DDDD");
    //     }
    // }
}