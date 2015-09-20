// C#
// SplitMeshIntoTriangles.cs
using UnityEngine;
using System.Collections;

public class MeshToTriangles : MonoBehaviour
{
	public void SplitMesh (GameObject pfx, bool isDirectional, bool isExplosion, bool isAbove, bool isAtRight, Vector3 relativeVelocity, float explosionForce = 0, float explosionRadius = 0, bool useGravity = false)
	{
		MeshFilter MF = GetComponent<MeshFilter>();
		MeshRenderer MR = GetComponent<MeshRenderer>();
		Mesh M = MF.mesh;
		Vector3[] verts = M.vertices;
		Vector3[] normals = M.normals;
		Vector2[] uvs = M.uv;

		for (int submesh = 0; submesh < M.subMeshCount; submesh++)
		{
			int[] indices = M.GetTriangles(submesh);
			for (int i = 0; i < indices.Length; i += 3)
			{
				if (i % 5 == 0)
				{
					Vector3[] newVerts = new Vector3[3];
					Vector3[] newNormals = new Vector3[3];
					Vector2[] newUvs = new Vector2[3];
					for (int n = 0; n < 3; n++)
					{
						int index = indices[i + n];
						newVerts[n] = verts[index];
						newUvs[n] = uvs[index];
						newNormals[n] = normals[index];
					}
					Mesh mesh = new Mesh();
					mesh.vertices = newVerts;
					mesh.normals = newNormals;
					mesh.uv = newUvs;
					mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };
					
					GameObject GO = new GameObject("Triangle " + (i / 3));
					GO.transform.position = transform.position;
					GO.transform.rotation = transform.rotation;
					GO.transform.localScale = transform.localScale;
					GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];
					GO.AddComponent<MeshFilter>().mesh = mesh;
					GameObject gObj = Instantiate(pfx);
					gObj.transform.parent = GO.transform;
					gObj.transform.position = GO.transform.position;
					gObj.SetActive(true);
					
					Rigidbody rigidB = GO.AddComponent<Rigidbody>();
					rigidB.useGravity = false;

					//rigidB.AddForce(relativeVelocity * 40);

					// Horizontal speed interval
					float xSpeedMin = relativeVelocity.x * 2000;
					float xSpeedMax = relativeVelocity.x * 5000;
					float xSpeed 	= ((isAtRight)?1:-1) * Random.Range(xSpeedMin, xSpeedMax);

					// Verticla speed interval
					float ySpeedLim = relativeVelocity.x * 4000;
					float ySpeedLow = ySpeedLim/3;
					float ySpeedMin = (isAbove) ? -ySpeedLim : -ySpeedLow;
					float ySpeedMax = (isAbove) ? ySpeedLow : ySpeedLim;
					float ySpeed 	= Random.Range(ySpeedMin, ySpeedMax);

					rigidB.AddExplosionForce(relativeVelocity.x * 60, new Vector3(xSpeed, ySpeed, 0), 2000000);
					
					//				Vector3 globalVert = transform.TransformDirection(newVerts[0]);
					//				float distToZeroX = -globalVert.x;
					
					MeshLerper mLerper = GO.AddComponent<MeshLerper>();
					//Destroy(GO, distToZeroX * 1.05f /*5 + Random.Range(0.0f, 5.0f)*/);
				}
			}
		}
		MR.enabled = false;

		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}
}