
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    public static CameraFlip Instance;
    [SerializeField] private float speed;
    [SerializeField]private float currentPosX;
    private Vector3 velocity;
    private GameObject batasCamera;
    private CinemachineConfiner2D confiner;
    private CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        
    }
    private void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        CameraDefault();
        confiner.m_BoundingShape2D = batasCamera.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            velocity = new Vector3(-6,0,0);
        }
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX,transform.position.y,transform.position.z),
        ref velocity, speed);
    }
    public void FlipCam()
    {
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(-8.82f, 0, 0);
    }
    public void DefaultCam()
    {
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(8.82f, 0, 0);
    }
    public void CameraDefault()
    {
        batasCamera = GameObject.Find("BatasCamera");
        confiner.m_BoundingShape2D = batasCamera.GetComponent<Collider2D>();
    }
    public void CameraWave1()
    {
        batasCamera = GameObject.Find("BatasCameraWave1");
        
        confiner.m_BoundingShape2D = batasCamera.GetComponent<Collider2D>();
    }
    public void CameraWave2()
    {
        batasCamera = GameObject.Find("BatasCameraWave2");

        confiner.m_BoundingShape2D = batasCamera.GetComponent<Collider2D>();
    }
}
