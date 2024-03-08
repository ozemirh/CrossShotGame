using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAscend : MonoBehaviour
{
    Animation anim;
    public GameObject crossbow;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        crossbow = Camera.main.GetComponent<CameraController>().GetCrossbow();
        StartCoroutine(AllowTouch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

    }
    public void GetCrossbow(GameObject gameObject)
    {
        crossbow = gameObject;
    }
    public void BallAscendAnimation(Vector3 targetPos)
    {
        AnimationCurve curveY;
        AnimationCurve curveX;
        targetPosition = targetPos;
        // create a new AnimationClip
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        // create a curveX to move the GameObject and assign to the clip
        Keyframe[] keysX;
        keysX = new Keyframe[2];
        keysX[0] = new Keyframe(0.0f, transform.position.x);
        keysX[1] = new Keyframe(0.4f, targetPos.x);
        curveX = new AnimationCurve(keysX);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        // create a curveY to move the GameObject and assign to the clip
        Keyframe[] keysY;
        keysY = new Keyframe[2];
        keysY[0] = new Keyframe(0.0f, transform.position.y);
        keysY[1] = new Keyframe(0.4f, targetPos.y);
        curveY = new AnimationCurve(keysY);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        // now animate the GameObject
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }
    IEnumerator AllowTouch()
    {
        yield return new WaitForSeconds(1f);
        crossbow.transform.GetChild(1).GetComponent<StringBend>().ChangePermission(true);
        crossbow.transform.GetChild(1).GetComponent<StringBend>().SetCameraPos(Camera.main.transform.position);
        Camera.main.GetComponent<CameraController>().enabled = false;
    }
}
