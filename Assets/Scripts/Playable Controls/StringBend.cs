using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class StringBend : MonoBehaviour
{
    CameraController cameraController;
    public Camera cam;
    private SpriteShapeController sprite;
    private Spline spline;
    GameObject crossBow;
    GameObject ball;
    public float downLimit = 1;
    public float upLimit = 3.5f;
    private Vector3 initialFingerPosition;
    private float initialCameraSize;
    private Vector3 initialBallPos;
    private Vector3 initialCameraPos;
    public float sizeSpeed = 0.8f; // Adjust as needed
    public float cameraAscendSpeed = 1f;
    private Quaternion initialCrossbowRotation;
    public bool permission = false;
    private Vector3 verticalMovement;
    private float angle;
    Vector2 velocity;
    private TrajectoryManager trajectoryManager;
    public float launchForce = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        //
        Application.targetFrameRate = 60;
        //
        cameraController = Camera.main.GetComponent<CameraController>();
        crossBow = transform.parent.gameObject;
        ball = cameraController.GetBall();
        sprite = GetComponent<SpriteShapeController>();
        spline = sprite.spline;
        initialCameraSize = Camera.main.orthographicSize;
        initialBallPos = ball.transform.position;
        initialCrossbowRotation = crossBow.transform.rotation;
        initialFingerPosition = new Vector3(2.6f, 3.5f, 0);
        initialCameraPos = Camera.main.transform.position;
        trajectoryManager = TrajectoryManager.Instance;
        cameraAscendSpeed = cameraController.GetCameraSpeed();
    }
    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && permission) // if the screen is touched
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
            Vector3 touchedPos = new Vector3(touch.position.x, touch.position.y, 10); // get the touch position from the screen touch to world point
            Vector3 splinePosition = transform.TransformPoint(spline.GetPosition(1).x, spline.GetPosition(1).y, 0); // get the position of the spline in the world space
            Vector3 currentFingerPosition = touchedPos / 200;
            verticalMovement = initialFingerPosition - currentFingerPosition;
            float newOrthoSize = 5.2f;
            if (currentFingerPosition.y <= upLimit)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initialFingerPosition = currentFingerPosition;
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    trajectoryManager.GetLine().enabled = true;
                    trajectoryManager.SimulateLaunch(ball.transform.position, velocity);
                }
                if (touch.phase == TouchPhase.Moved)// if the finger is moving
                {
                    float newSizeDelta = verticalMovement.y * sizeSpeed;
                    newOrthoSize = initialCameraSize + newSizeDelta;
                    newOrthoSize = Mathf.Clamp(newOrthoSize, 5.2f, 8.1f); // return a value between the miniimum and maximum value
                    Camera.main.orthographicSize = newOrthoSize; // change the size of the camera
                    velocity = verticalMovement * launchForce;
                    Camera.main.transform.position = initialCameraPos;
                    Vector3 newCameraPos = new Vector3(0, Camera.main.transform.position.y + (verticalMovement.y * cameraAscendSpeed), -10);
                    if (newCameraPos.y >= initialCameraPos.y)
                    {
                        Camera.main.transform.position = newCameraPos;
                    }
                    float bendValue = verticalMovement.y;
                    if (currentFingerPosition.y > downLimit && verticalMovement.y > 0)
                    {
                        spline.SetPosition(1, new Vector3(spline.GetPosition(1).x, -(bendValue / 2))); // bends the string in y axis
                        ball.transform.SetLocalPositionAndRotation(splinePosition, new Quaternion()); // attach the ball's position to spline's
                    }
                    else if (currentFingerPosition.y <= downLimit)
                    {
                        spline.SetPosition(1, new Vector3(spline.GetPosition(1).x, -0.7f)); // bends the string in y axis   
                        ball.transform.SetLocalPositionAndRotation(splinePosition, new Quaternion());
                    }
                    // Get the eighth position of the line renderer
                    Vector3 firstPosition = trajectoryManager.GetLine().GetPosition(0);
                    Vector3 secondPosition = trajectoryManager.GetLine().GetPosition(1);
                    // Calculate the direction from the crossbow to the target position
                    Vector3 directionToTarget = secondPosition - firstPosition;
                    // Calculate the angle between the direction and the crossbow's forward vector
                    angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

                    crossBow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                }

            }
        }
        else
        {
            spline.SetPosition(1, new Vector3(spline.GetPosition(1).x, 0)); // returns the position of the string to default
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0 && permission) // if the screen is touched
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
            Vector3 touchedPos = new Vector3(touch.position.x, touch.position.y, 10); // get the touch position from the screen touch to world point
            Vector3 currentFingerPosition = touchedPos / 200;
            if (touch.phase == TouchPhase.Ended && currentFingerPosition.y <= 3) // if the ball is thrown, shake the crossbow
            {
                ThrowBall();
                trajectoryManager.GetLine().enabled = false;
            }
        }
    }
    void ThrowBall()
    {
        cameraController.enabled = true;
        Rigidbody2D rigid = ball.GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Dynamic; // make the ball dynamic
        // Set the initial velocity to match the direction of the line
        rigid.velocity = velocity;
        permission = false; // untouchable
    }
    public void ResetVariables()
    {
        // Reset camera size and position
        Camera.main.orthographicSize = initialCameraSize;
        Camera.main.transform.position = initialCameraPos;
        sizeSpeed = 1f;
        cameraController.enabled = false;
        trajectoryManager.GetLine().positionCount = 0;
        // Reset ball position and spline
        ball.transform.position = initialBallPos;
        spline.SetPosition(1, Vector3.zero);
        Input.ResetInputAxes();
        // Reset crossbow rotation
        crossBow.transform.rotation = initialCrossbowRotation;
    }
    public void ChangeInitialValues(Vector3 ballPos, Vector3 crossbowPos, Quaternion crossbowRot, float cameraSize)
    {
        initialBallPos = ballPos;
        initialCrossbowRotation = crossbowRot;
        initialCameraSize = cameraSize;
    }
    public void GetNewBall(GameObject go)
    {
        ball = go;
    }
    public void ChangeLaunchForce(float force)
    {
        launchForce = force;
    }
    public void ChangePermission(bool allowance)
    {
        permission = allowance;
    }
    public void SetCameraPos(Vector3 pos)
    {
        initialCameraPos = pos;
    }
    public void SetCameraAscendSpeed(float value)
    {
        cameraAscendSpeed = value;
    }
    public void StartPermissionCoroutine()
    {
        StartCoroutine(GivePermission());
    }
    IEnumerator GivePermission()
    {
        yield return new WaitForSeconds(.3f);
        ChangePermission(true);
    }
    public void GetCamera(Camera camera)
    {
        cam = camera;
    }
}
