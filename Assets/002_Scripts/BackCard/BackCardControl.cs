using UnityEngine;
using System.Collections;

public class BackCardControl : TargetControl
{

    public Animator cubeAnim;
    public Animator textAnim;
    public Animator circleAnim;
    public GameObject Circle;
    public Animator[] modelsAnim;
    private int currentModel;

    void Start () {
	
	}
	
	void Update () {
        
    }

    public void ShowCircle() {
        Circle.SetActive(true);
    }

    public void BtnClick(int tag) {
        if (circleAnim.GetInteger("Btn") != tag && (cubeAnim.GetCurrentAnimatorStateInfo(0).IsName("CubeStatic") || cubeAnim.GetCurrentAnimatorStateInfo(0).IsName("CubeStaticOpened")) ) {
            StartCoroutine(RealBtnClick(tag));

            cubeAnim.SetTrigger("Open");

            if (cubeAnim.GetCurrentAnimatorStateInfo(0).IsName("CubeStatic"))
            {
                modelsAnim[tag-1].SetBool("Show", true);
            }
            else {
                modelsAnim[currentModel].SetBool("Show", false);
                StartCoroutine(ShowModel(tag-1));
                
            }
            currentModel = tag - 1;
        }
    }

    IEnumerator ShowModel(int tag) {
        while (!cubeAnim.GetCurrentAnimatorStateInfo(0).IsName("CubeOpen")) {
            yield return null;
        }
        modelsAnim[tag].SetBool("Show", true);
    }

    IEnumerator RealBtnClick(int tag) {
        circleAnim.SetInteger("Btn", 0);
        yield return null;
        circleAnim.SetInteger("Btn", tag);
    }

    public override void Reset()
    {
        base.Reset();
        Circle.SetActive(false);
    }

    public override void Init()
    {
        base.Init();

    }
}
