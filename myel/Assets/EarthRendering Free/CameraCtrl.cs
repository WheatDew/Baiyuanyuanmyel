using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour
{
	public Transform light = null;
    public Transform cloud = null;
	public MeshRenderer earthRenderer = null;
	public MeshRenderer atmosphereRenderer = null;

	float MIN_DIST = 200;
	float MAX_DIST = 5000;

	public float dist = 400;
	public float day = 1;
    public float dayCloud = 0.5f;
	private float longitude = 0;
	private float latitude = 0;

	private float setLon = 0;
	private float setLat = 0;

	[Range(-180f,180)] public float displayLon = 0;
	[Range(-90,90)] public float displayLat = 0;
	Quaternion cameraRotation;
	Vector2 targetOffCenter = Vector2.zero;
	Vector2 offCenter = Vector2.zero;

	// Use this for initialization
	void Start()
	{
		displayLat = -36;
		displayLon = 30;
		SetPosition();
		longitude = setLon;
		latitude = setLat;
		//cameraRotation = Quaternion.LookRotation(-transform.position.normalized, Vector3.up);
	}

	// Update is called once per frame
	void Update()
	{
        //float wheelDelta = Input.GetAxis("Mouse ScrollWheel");
        //if (wheelDelta > 0)
        //{
        //	dist *= 0.87f;
        //}
        //else if (wheelDelta < 0)
        //{
        //	dist *= 1.15f;
        //}
        //if (dist < MIN_DIST)
        //{
        //	dist = MIN_DIST;
        //}
        //else if (dist > MAX_DIST)
        //{
        //	dist = MAX_DIST;
        //}
        //float xMove = Input.GetAxis("Mouse X");
        //float yMove = Input.GetAxis("Mouse Y");

        //float targetRadius = 100;

        //if (Input.GetButton("Fire1"))
        //{
        //	if (xMove != 0 || yMove != 0)
        //	{
        //		float rotateSensitivity = Mathf.Min(2f, (float)((dist - targetRadius) / targetRadius) * 1.2f);
        //		cameraRotation *= (Quaternion.AngleAxis(rotateSensitivity * xMove, Vector3.up) *
        //							Quaternion.AngleAxis(rotateSensitivity * yMove, Vector3.left));
        //	}
        //}
        //else if (Input.GetButton("Fire2"))
        //{
        //	//Quaternion lightRotation = light.rotation;
        //	//lightRotation *= Quaternion.AngleAxis(-xMove * 2, Vector3.up);
        //	//light.rotation = lightRotation;
        //}
        //else if (Input.GetButton("Fire3"))
        //{
        //	const float MOUSE_TRANSLATE_SENSITIVITY = 10;
        //	targetOffCenter.x -= xMove * MOUSE_TRANSLATE_SENSITIVITY;
        //	targetOffCenter.y -= yMove * MOUSE_TRANSLATE_SENSITIVITY;

        //	float translateMultiply = 0.5625f * Screen.width / Screen.height * Mathf.Tan(GetComponent<Camera>().fieldOfView / 2) * dist / Screen.height / 2.5f;
        //	offCenter.x = targetOffCenter.x * translateMultiply;
        //	offCenter.y = targetOffCenter.y * translateMultiply;
        //}

        //transform.rotation = cameraRotation;

        //transform.position = cameraRotation * (-Vector3.forward * dist);
        //transform.position += new Vector3(transform.right.x * offCenter.x + transform.up.x * offCenter.y,
        //									transform.right.y * offCenter.x + transform.up.y * offCenter.y,
        //									transform.right.z * offCenter.x + transform.up.z * offCenter.y);

        //Vector3 lightDir = Quaternion.Inverse(light.rotation) * Vector3.forward;
        //earthRenderer.material.SetVector("_LightDir", lightDir);
        //atmosphereRenderer.material.SetVector("_LightDir", lightDir);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit result;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result);

            float theta,phi = 0;
            float arccos = Mathf.Acos(result.point.z / Mathf.Sqrt(Mathf.Pow(result.point.x, 2) + Mathf.Pow(result.point.z, 2)));
            float sin = result.point.x / Mathf.Sqrt(Mathf.Pow(result.point.x, 2) + Mathf.Pow(result.point.z, 2));

            phi = Mathf.Acos(result.point.y / Mathf.Sqrt(Mathf.Pow(result.point.x, 2) 
                + Mathf.Pow(result.point.y, 2) + Mathf.Pow(result.point.z, 2)));

            if (sin > 0)
            {
                theta = arccos;
            }
            else
            {
                theta = 2 * Mathf.PI - arccos;
            }

            print(string.Format("{0} {1} {4} {2} {3}", theta, longitude, phi, latitude,theta+longitude));
        }



		longitude = Mathf.Lerp(longitude, setLon, Time.deltaTime);
		latitude = Mathf.Lerp(latitude, setLat, Time.deltaTime);

		Quaternion lightRotation = light.rotation;
		lightRotation *= Quaternion.AngleAxis(-day * 2, Vector3.up);
		light.rotation = lightRotation;

        Quaternion cloudRotation = cloud.rotation;
        cloudRotation *= Quaternion.AngleAxis(-dayCloud * 2, Vector3.up);
        cloud.rotation = cloudRotation;

        Camera.main.transform.position = new Vector3(200 * Mathf.Sin(latitude) * Mathf.Cos(longitude), 200 * Mathf.Cos(latitude), 200 * Mathf.Sin(longitude) * Mathf.Sin(latitude));
		Camera.main.transform.transform.LookAt(Vector3.zero);
	}

	public void SetPosition()
    {
		setLon = (displayLon+180f)/180f*Mathf.PI;
		setLat = (displayLat+90f)/180f*Mathf.PI;
    }

}
