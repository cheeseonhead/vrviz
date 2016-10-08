using UnityEngine;
using System.Collections;

public class Spectrum : MonoBehaviour {
	
	public GameObject prefab;
	public int numberOfObjects = 20;
	public int amplificationScale = 100;
	public float radius = 5f;
	public GameObject[] cubes;

	void Start() {
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
			Instantiate(prefab, pos, Quaternion.identity);
		}
		cubes = GameObject.FindGameObjectsWithTag ("cubes");
	}
	
	// Update is called once per frame
	void Update () {
		float[] spectrum = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		for (int i = 0; i < numberOfObjects; i++) {
			Vector3 previousScale = cubes [i].transform.localScale;
			previousScale.y = spectrum [i] * amplificationScale;
			cubes [i].transform.localScale = previousScale;
			cubes[i].transform.Rotate (Vector3.up * Time.deltaTime, Space.World);
		}
	}
}
