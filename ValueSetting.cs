using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityOSC;

public class ValueSetting : MonoBehaviour
{

	private Slider ValueA, ValueB, ValueC, ValueF,ValueL;
    public Text text;
    public wave w;
    public GameObject m;

    // Start is called before the first frame update
    void Start()
    {
		GameObject A = GameObject.FindWithTag("SliderA");
		GameObject B = GameObject.FindWithTag("SliderB");
        GameObject C = GameObject.FindWithTag("SliderC");
        GameObject F = GameObject.FindWithTag("SliderF");
        GameObject L = GameObject.FindWithTag("SliderL");
        ValueA = A.GetComponent<Slider>();
		ValueB = B.GetComponent<Slider>();
        ValueC = C.GetComponent<Slider>();
        ValueF = F.GetComponent<Slider>();
        ValueL = L.GetComponent<Slider>();
        ValueA.value = 10;
		ValueB.value = 20;
        ValueC.value = 10;
        ValueF.value = 440;
        ValueL.value = 80;
        //Slider setup

        m = GameObject.FindWithTag("MainCamera");
        w = m.GetComponent<wave>();

        GameObject D = GameObject.FindWithTag("equationText");
		text = D.GetComponent<Text>();
		text.text = "To display each sliders value. Wate a seconds";
        //Text setup

        //to use unityOSC
        OSCHandler.Instance.Init();

	}

    // Update is called once per frame
    void Update()
    {
		int bigA = (int)ValueA.value / 10;
		int smallA = (int)ValueA.value % 10;
		int bigB = (int)ValueB.value / 10;
		int smallB = (int)ValueB.value % 10;
		int bigC = (int)ValueC.value / 10;
		int smallC = (int)ValueC.value % 10;
        int F = (int)ValueF.value;
        int bigL = (int)ValueL.value / 10;
        int smallL = (int)ValueL.value % 10;

        text.text = "Ratios are X=" + bigA + "." + smallA +
                              " Y=" + bigB + "." + smallB +
                              " Z=" + bigC + ". " + smallC +
                              " F = "+F+
                              " and L="+bigL+"."+smallL+"PI.";
        w.sliderSetter(ValueA.value, ValueB.value, ValueC.value, ValueF.value, ValueL.value);



    }

    public void sendOSCData(Vector3 position,float x, float y, float z, float f)
    //public void sendOSCData(Vector3 position)
    {
        //OSC data sent every update
        //if send one data.
        //OSCHandler.Instance.SendMessageToClient("chordController", "/soundX", ValueA.value);

        //to calculate for sending data
        double r, a, e;
        double sqrtOfX, sqrtOfY, sqrtOfZ;
        sqrtOfX = position.x * position.x;
        sqrtOfY = position.y * position.y;
        sqrtOfZ = position.z * position.z;

        r = Math.Sqrt(sqrtOfX+sqrtOfY+sqrtOfZ);
        a = Math.Atan2(position.z , position.x) * Mathf.Rad2Deg - 90;
        if (a < 0) a += 360;
        e = Math.Atan2(position.y , Math.Sqrt(sqrtOfX + sqrtOfZ)) * Mathf.Rad2Deg;
        //Debug.Log("a is " + a + " and e is " + e + ".");
        //to calculate for sending data

        //if send datas box.
        List<object> values = new List<object>();
        //values.AddRange(new object[] { (float)r, (float)a, (float)e });

        values.AddRange(new object[] { x*f*440 , y*f*440 , z*f*440 , (float)a , (float)e ,(float)r });
        
        OSCHandler.Instance.SendMessageToClient("chordController", "/soundDataBox", values);

        /*
         * if I need 3 thetas data to make chord, I should fix 3 points of this protram
         * 1. argument of this function(hikisuu).
         * 2. packaging data box(this/@98).
         * 3. function at wave.cs/@137.
         * and little fix Puredata program.
        */
    }

    public void ValueChecking()
	{
        
        //Debug.Log(w.dotCount);
        //w.sliderSetter(ValueA.value, ValueB.value, ValueC.value, ValueF.value, ValueL.value);
        //Debug.Log(ValueA.value + " &" + ValueB.value + " &" + ValueC.value);
        //Debug.Log("is valuechecking called?");
    }
}
