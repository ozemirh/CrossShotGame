using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class TrajectoryManager : MonoBehaviour
{
    #region SingletonPattern
    private static TrajectoryManager _instance;
    public static TrajectoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The AudioManager is Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    Scene currentScene;
    Scene _simScene;
    PhysicsScene2D _physicsScene;
    PhysicsScene2D currentPhysicsScene;
    GameObject ball;
    public GameObject ballPrefab;
    LineRenderer lineRenderer;
    public int trajectoryCount = 60;
    CameraController controller;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene2D();
        CreateSceneParameters _param = new CreateSceneParameters(LocalPhysicsMode.Physics2D); //define the parameters of a new scene, this lets us have our own separate physics 
        _simScene = SceneManager.CreateScene("Simulation", _param); // create a new scene and implement the parameters we just created
        _physicsScene = _simScene.GetPhysicsScene2D(); // assign the physics of the scene so we can simulate on our own time. 
        CreateSimObjects(); // send over simulated objects (see method below for details)
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = trajectoryCount; // set amount of points our drawn line will have
    }
    // Update is called once per frame
    void Update()
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }
    public LineRenderer GetLine()
    {
        return lineRenderer;
    }
    private void CreateSimObjects()  //all objects start in regulare scene, and get sent over on start. this way colliders are dynamic and we can grab refrence to simulated player in first scene.
    {
        GameObject[] _walls = GameObject.FindGameObjectsWithTag("Walls");
        GameObject[] _obstacles = GameObject.FindGameObjectsWithTag("Obstacle");// check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
        foreach (GameObject GO in _walls)   //duplicate all collidables and move them to the simulation
        {
            var newGO = Instantiate(GO, GO.transform.position, GO.transform.rotation);
            SceneManager.MoveGameObjectToScene(newGO, _simScene);
        }
        foreach (GameObject GO in _obstacles)   //duplicate all collidables and move them to the simulation
        {
            var newGO = Instantiate(GO, GO.transform.position, GO.transform.rotation);
            SceneManager.MoveGameObjectToScene(newGO, _simScene);
        }
    }
    public void SimulateLaunch(Vector3 currentPosition, Vector3 force)   //call this every frame while player is grabed;
    {
        if (currentPhysicsScene.IsValid() && _physicsScene.IsValid())
        {
            if (ball == null)
            {
                ball = Instantiate(ballPrefab, currentPosition, ballPrefab.transform.rotation);
                ball.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                SceneManager.MoveGameObjectToScene(ball, _simScene);
            }
            ball.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = trajectoryCount;
            for (int i = 0; i < trajectoryCount; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                Vector3 newBallPos = ball.transform.position;
                lineRenderer.SetPosition(i, new Vector3(newBallPos.x, newBallPos.y, -1f));
            }
        }
        Destroy(ball);
    }
}