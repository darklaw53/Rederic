using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrain : MonoBehaviour
{

    [SerializeField]
    GameObject player;
    [SerializeField]
    float timeOffSet;
    [SerializeField]
    Vector2 posOffSet;

    [SerializeField]
    float topLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    public Player rightFace;
    public bool camFollow;

    [SerializeField]
    float shake;
    [SerializeField]
    float shakeAmount;
    [SerializeField]
    float decreaseFactor;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    Transform deadCamPos;
    public bool deadCam;

    Vector3 startPos;
    Vector3 endPos;
    public Vector3 aimPos;

    public GameObject chivo1, chivo2, chivo3, chivo4, chivo5, chivo6;
    public Transform chivoHidePos, chivoShowPos;

    /*public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;*/

    private void Awake()
    {
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void Update()
    {
        if (shake > 0)
        {
            transform.localPosition = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0;
        }
    }

    public void CamShake(float shakiness)
    {
        shake = shakiness;
    }

    public void DeadCam()
    {
        camFollow = false;
        transform.position = deadCamPos.position;
        //Debug.Log("deadcam");
        deadCam = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        camFollow = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (camFollow)
        {
            if (rightFace != null)
            {
                if (rightFace.facingRight)
                {
                    startPos = transform.position;
                    endPos = player.transform.position;
                    endPos.x += posOffSet.x;
                    endPos.y += posOffSet.y;
                    endPos.z = -10;
                    transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);
                }
                else
                {
                    startPos = transform.position;
                    endPos = player.transform.position;
                    endPos.x -= posOffSet.x;
                    endPos.y += posOffSet.y;
                    endPos.z = -10;
                    transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);
                }


                //VV this is how you use smooth dampening VV
                //transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffSet);

                //VV this is how you lerp VV

                transform.position = new Vector3
                (
                    Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                    Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                    transform.position.z
                );
            }
        }
        else
        {
            CamOnThis();
        }
    }

    void CamOnThis()
    {
        if (deadCam)
        {
            startPos = transform.position;
            endPos = deadCamPos.transform.position;
            endPos.z = -10;
            transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);
        }
        else
        {
            startPos = transform.position;
            endPos = aimPos;
            endPos.z = -10;
            transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }

    public void Chivo1Get()
    {
        Debug.Log("chivo1get");
        if (!HoldmyShit.BountyCompleteR)
        {
            StartCoroutine(Chivo1());
        }
    }

    IEnumerator Chivo1()
    {
        yield return new WaitForSeconds(0);
        HoldmyShit.BountyCompleteR = true;

        //print("space key was pressed");
        for (int i = 0; i < 10; i++)
        {
            //print("space key was pressed");
            var b = (chivo1.transform.position.y - chivoShowPos.transform.position.y) / 10;
            chivo1.transform.position = new Vector2(chivo1.transform.position.x, chivo1.transform.position.y - b);
            yield return new WaitForSeconds(0.05f);
        }
        var c = (chivoHidePos.transform.position.y - chivo1.transform.position.y) / 10;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            //print("space key was pressed");
            chivo1.transform.position = new Vector2(chivo1.transform.position.x, chivo1.transform.position.y + c);
            yield return new WaitForSeconds(0.05f);
        }

        chivo1.transform.position = chivoHidePos.transform.position;
    }

    public void Chivo2Get()
    {
        Debug.Log("chivo2get");
        if (!HoldmyShit.JugglingR)
        {
            StartCoroutine(Chivo2());
        }      
    }

    IEnumerator Chivo2()
    {
        yield return new WaitForSeconds(0);
        HoldmyShit.JugglingR = true;

        print("space key was pressed");
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            var b = (chivo2.transform.position.y - chivoShowPos.transform.position.y) / 10;
            chivo2.transform.position = new Vector2(chivo2.transform.position.x, chivo2.transform.position.y - b);
            yield return new WaitForSeconds(0.05f);
        }
        var c = (chivoHidePos.transform.position.y - chivo2.transform.position.y) / 10;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            chivo2.transform.position = new Vector2(chivo2.transform.position.x, chivo2.transform.position.y + c);
            yield return new WaitForSeconds(0.05f);
        }

        chivo2.transform.position = chivoHidePos.transform.position;
    }

    public void Chivo3Get()
    {
        Debug.Log("chivo3get");
        if (!HoldmyShit.HoarderR)
        {
            StartCoroutine(Chivo3());
        }
    }

    IEnumerator Chivo3()
    {
        yield return new WaitForSeconds(0);
        HoldmyShit.HoarderR = true;

        print("space key was pressed");
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            var b = (chivo3.transform.position.y - chivoShowPos.transform.position.y) / 10;
            chivo3.transform.position = new Vector2(chivo3.transform.position.x, chivo3.transform.position.y - b);
            yield return new WaitForSeconds(0.05f);
        }
        var c = (chivoHidePos.transform.position.y - chivo3.transform.position.y) / 10;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            chivo3.transform.position = new Vector2(chivo3.transform.position.x, chivo3.transform.position.y + c);
            yield return new WaitForSeconds(0.05f);
        }

        chivo3.transform.position = chivoHidePos.transform.position;
    }

    public void Chivo4Get()
    {
        Debug.Log("chivo4get");
        if (!HoldmyShit.FrugalR)
        {
            StartCoroutine(Chivo4());
        }
    }

    IEnumerator Chivo4()
    {
        yield return new WaitForSeconds(0);
        HoldmyShit.FrugalR = true;

        print("space key was pressed");
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            var b = (chivo4.transform.position.y - chivoShowPos.transform.position.y) / 10;
            chivo4.transform.position = new Vector2(chivo4.transform.position.x, chivo4.transform.position.y - b);
            yield return new WaitForSeconds(0.05f);
        }
        var c = (chivoHidePos.transform.position.y - chivo4.transform.position.y) / 10;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            chivo4.transform.position = new Vector2(chivo4.transform.position.x, chivo4.transform.position.y + c);
            yield return new WaitForSeconds(0.05f);
        }

        chivo4.transform.position = chivoHidePos.transform.position;
    }

    public void Chivo5Get()
    {
        Debug.Log("chivo5get");
        if (!HoldmyShit.UnloadQuiverR)
        {
            StartCoroutine(Chivo5());
        }
    }

    IEnumerator Chivo5()
    {
        yield return new WaitForSeconds(0);
        HoldmyShit.UnloadQuiverR = true;

        print("space key was pressed");
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            var b = (chivo5.transform.position.y - chivoShowPos.transform.position.y) / 10;
            chivo5.transform.position = new Vector2(chivo5.transform.position.x, chivo5.transform.position.y - b);
            yield return new WaitForSeconds(0.05f);
        }
        var c = (chivoHidePos.transform.position.y - chivo5.transform.position.y) / 10;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            chivo5.transform.position = new Vector2(chivo5.transform.position.x, chivo5.transform.position.y + c);
            yield return new WaitForSeconds(0.05f);
        }

        chivo5.transform.position = chivoHidePos.transform.position;
    }

    public void Chivo6Get()
    {
        Debug.Log("chivo6get");
        if (!HoldmyShit.DoNoEvilR)
        {
            StartCoroutine(Chivo6());
        }
    }

    IEnumerator Chivo6()
    {
        yield return new WaitForSeconds(0);
        HoldmyShit.DoNoEvilR = true;

        print("space key was pressed");
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            var b = (chivo6.transform.position.y - chivoShowPos.transform.position.y) / 10;
            chivo6.transform.position = new Vector2(chivo6.transform.position.x, chivo6.transform.position.y - b);
            yield return new WaitForSeconds(0.05f);
        }
        var c = (chivoHidePos.transform.position.y - chivo6.transform.position.y) / 10;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            print("space key was pressed");
            chivo6.transform.position = new Vector2(chivo6.transform.position.x, chivo6.transform.position.y + c);
            yield return new WaitForSeconds(0.05f);
        }

        chivo6.transform.position = chivoHidePos.transform.position;
    }

    private void OnApplicationQuit()
    {
        SaveManager.instance.Save();
    }
}
