using UnityEngine;

public class CrossbowSpawn : MonoBehaviour
{
    public HoopManager hoopManager;
    CrossbowSpawn crossbowSpawn;
    Animator ringAnim;
    Animation anim;
    public GameObject spawnPoint;
    private GameObject originalCrossbow;
    private GameObject originalBall;
    private Ground ground;
    CameraController controller;
    GameObject crossbow;
    GameObject circle;
    GameObject ball;
    Vector3 targetPos;
    Vector3 ballSpawnPos;
    private bool isScored = false;
    public float ascendSpeed;
    public float launchForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        controller = Camera.main.GetComponent<CameraController>();
        ground = controller.GetGround();
        crossbowSpawn = GetComponent<CrossbowSpawn>();
        ringAnim = GetComponent<Animator>();
        originalCrossbow = controller.GetCrossbow();
        originalBall = controller.GetBall();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawnPoint = Instantiate(spawnPoint, transform.parent.position, Quaternion.identity);
        controller.CreateCrossbow(transform.parent.GetChild(1).position, Quaternion.identity, launchForce);
        targetPos = transform.parent.GetChild(2).position;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ringAnim.SetTrigger("Ring");
        crossbow = controller.GetCrossbow();
        ballSpawnPos = crossbow.transform.GetChild(2).position;
        controller.CreateBall(ballSpawnPos, Quaternion.identity);
        ball = controller.GetBall();
        crossbow.transform.GetChild(1).GetComponent<StringBend>().GetNewBall(ball);
        ball.GetComponent<BallAscend>().GetCrossbow(crossbow);
        ground.GetNewBall(ball);
        isScored = true;
        originalBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        originalBall.SetActive(false);
        originalCrossbow.SetActive(false);
        controller.SetScoreValue(isScored);
        ground.SetStringBend(crossbow.transform.GetChild(1).GetComponent<StringBend>());
        Vector3 newBallPos = new Vector3(targetPos.x, targetPos.y);
        crossbow.transform.GetChild(1).GetComponent<StringBend>().GetNewBall(ball);
        crossbow.transform.GetChild(1).GetComponent<StringBend>().ChangeInitialValues(newBallPos,
            targetPos, crossbow.transform.rotation, 5.2f);
        hoopManager.SetSpawnValues();
    }
    public void RingEnd()
    {
        if (hoopManager.GetHoopsCount() > 0)
        {
            circle = spawnPoint.transform.GetChild(0).gameObject;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            circle.GetComponent<Animator>().SetTrigger("CircleExpand");
            circle.GetComponent<CircleExpanse>().CircleSpawn(crossbowSpawn);
        }
        else
        {
            Camera.main.GetComponent<LevelEnd>().LevelEndUI();
        }
        transform.parent.gameObject.SetActive(false);
    }
    public void MoveObjects()
    {
        CrossbowAscendAnimation();
        controller.ShrinkCameraAnimation();
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        ball.GetComponent<BallAscend>().BallAscendAnimation(targetPos);
        controller.enabled = true;
        circle.GetComponent<CircleExpanse>().CloseCircleAnimation();
    }
    public void CrossbowAscendAnimation()
    {
        anim = crossbow.GetComponent<Animation>();
        AnimationCurve curveY;
        AnimationCurve curveX;
        // create a new AnimationClip
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        // create a curveX to move the GameObject and assign to the clip
        Keyframe[] keysX;
        keysX = new Keyframe[2];
        keysX[0] = new Keyframe(0.0f, crossbow.transform.position.x);
        keysX[1] = new Keyframe(0.4f, targetPos.x);
        curveX = new AnimationCurve(keysX);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        // create a curveY to move the GameObject and assign to the clip
        Keyframe[] keysY;
        keysY = new Keyframe[2];
        keysY[0] = new Keyframe(0.0f, crossbow.transform.position.y);
        keysY[1] = new Keyframe(0.4f, targetPos.y);
        curveY = new AnimationCurve(keysY);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        // now animate the GameObject
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }
    public void SetOriginalCrossbow(GameObject go)
    {
        originalCrossbow = go;
    }
    public void SetOriginalBall(GameObject go)
    {
        originalBall = go;
    }
    public float GetActiveAscendSpeed()
    {
        return ascendSpeed;
    }
}
