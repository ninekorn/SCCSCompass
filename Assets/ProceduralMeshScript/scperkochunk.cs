using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshCollider))]

public class scperkochunk : MonoBehaviour
{
    public int width = 16;
    public int height = 1;
    public int depth = 16;
    public float planeSize = 0.25f;

    public byte[,,] map;
    protected Mesh mesh;
    protected List<Vector3> verts = new List<Vector3>();
    protected List<int> tris = new List<int>();
    protected List<Vector2> uv = new List<Vector2>();

    float seed;
    byte block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    int divider = 10;

    public float detailScale = 1;
    public float heightScale = 1;

    float persistence = 0;
    int numberOfOctaves = 0;
    float frequency = 0;
    float amplitude = 0;

    public float scale = 6.5f;
   
    void Start()
    {
        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
        chunkSize = 1f;

        seed = 3420;

        int radius = 5;
        Vector3 center = Vector3.zero;

        map = new byte[width, height, depth];

        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs(((float)(x * planeSize + transform.position.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs(((float)(y * planeSize + transform.position.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Mathf.Abs(((float)(z * planeSize + transform.position.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + transform.position.x;
                    float posY = y * planeSize + transform.position.y;
                    float posZ = z * planeSize + transform.position.z;

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(position, center);

                    Vector3 position1 = transform.position;

                    float distance1 = Vector3.Distance(position1, center);

                    map[x, y, z] = 1;
                }
            }
        }
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Regenerate();
        //GetComponent<chunko>().enabled = false;
    }

    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();
        uv.Clear();
        mesh.triangles = tris.ToArray();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    //block = map[x + width * (y + depth * z)]; //
                    block = map[x, y, z];

                    if (block == 0) continue;
                    {
                        DrawBrick(x, y, z);
                    }
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //GetComponent<chunku>().enabled = false;

        GetComponent<MeshFilter>().mesh = mesh;


    }
    
    public void DrawBrick(int x, int y, int z)
    {
        Vector3 start = new Vector3(x * planeSize, y * planeSize, z * planeSize);
        Vector3 offset1, offset2;

        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = Vector3.forward * planeSize;
            offset2 = Vector3.right * planeSize;
            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
        }

        /*//LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.back * planeSize;
            offset2 = Vector3.down * planeSize;
            DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.up * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start, offset1, offset2);
        }*/
    }

    public void DrawFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);


		tris.Add(index + 1);
		tris.Add(index + 2);
		tris.Add(index + 3);
		tris.Add(index + 2);
		tris.Add(index + 1);
		tris.Add(index + 0);

    }

    public bool IsTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            return map[x, y, z] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }

    public byte GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        return map[x, y, z];
        //return map[x + width * (y + depth * z)];
    }

    void checkBytePos()
    {
        float xPosition = (transform.position.x);
        float yPosition = (transform.position.y);
        float zPosition = (transform.position.z);

        int xPose;
        int yPose;
        int zPose;

        if (xPosition < 0)
        {
            xPose = (int)Mathf.Ceil(xPosition);
        }

        else
        {
            xPose = (int)Mathf.Floor(xPosition);
        }

        if (yPosition < 0)
        {
            yPose = (int)Mathf.Ceil(yPosition);
        }

        else
        {
            yPose = (int)Mathf.Floor(yPosition);
        }

        if (zPosition < 0)
        {
            zPose = (int)Mathf.Ceil(zPosition);
        }

        else
        {
            zPose = (int)Mathf.Floor(zPosition);
        }
        
        for (int x = xPose - width / 2; x < xPose + width / 2; x++)
        {
            for (int y = yPose - width / 2; y < yPose + width / 2; y++)
            {
                for (int z = zPose - width / 2; z < zPose + width / 2; z++)
                {
                    int xPos = (x);
                    int yPos = (y);
                    int zPos = (z);

                    //Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);

                    if (x < 0)
                    {
                        int yo = (int)Mathf.Ceil(-x / divider);
                        int yo1 = x + (yo * divider);
                        xPos = divider;
                        xPos += -yo;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(x / divider);
                        xPos = x - (yo * divider);
                    }
                    if (y < 0)
                    {
                        int yo = (int)Mathf.Ceil(-y / divider);
                        int yo1 = y + (yo * divider);
                        yPos = divider;
                        yPos += -yo1;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(y / divider);
                        yPos = y - (yo * divider);
                    }
                    if (z < 0)
                    {
                        int yo = (int)Mathf.Ceil(-z / divider);
                        int yo1 = z + (yo * divider);
                        zPos = divider;
                        zPos += -yo1;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(z / divider);
                        zPos = z - (yo * divider);
                    }
                }
            }
        }
    }

    /*void OnDrawGizmos()
    {
        if (mesh.vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x + transform.position.x, mesh.vertices[i].y + transform.position.y, mesh.vertices[i].z + transform.position.z), 0.01f);
        }
    }*/
}