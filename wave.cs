using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject newLine;
    public LineRenderer waveLine;
    public float thetaX,thetaY,thetaZ;
    public int posi; //number of dot position
    public int isFinished, lineCountMax;
    public float coefficientX, coefficientY, coefficientZ;
    public float FHz;
    public Vector3[] dotVec; //dot of line's vector
    public int lineLength;
    public int dotCount;
    public const int dotMax = 4032;
    public GameObject s;
    public GameObject sphere;
    ValueSetting oscData = new ValueSetting();

    void Start()
    {
        newLine = new GameObject("Line");
        waveLine = newLine.AddComponent<LineRenderer>();
        thetaX = 0;
        thetaY = 0;
        thetaZ = 0;
        posi = 0;
        isFinished = 0;
        lineCountMax = 0;
        coefficientX = 0.0f;
        coefficientY = 0.0f;
        coefficientZ = 0.0f;
        FHz = 1.0f;
        lineLength = 252;
        dotCount = 1;
        dotVec = new Vector3[4033];
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //s = GameObject.FindWithTag("headsphere");

        //setting line of wave
        waveLine.SetWidth(0.05f, 0.05f);
        waveLine.material = new Material(Shader.Find("Sprites/Default"));
        waveLine.SetColors(Color.blue, Color.green);
        //setting line of wave

        sphere.transform.Translate(0, 0, 0);
        sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        sphere.GetComponent<Renderer>().material.color = Color.green;

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        
        //Debug.Log(dotVec.Length);
        dotVec[dotCount]
                = new Vector3(Mathf.Sin(thetaX) * 5,
                              Mathf.Sin(thetaY) * 5,
                              Mathf.Sin(thetaZ) * 5);
        //theta += 0.05f;
        thetaX += FHz * 0.05f * coefficientX;//default FHz = 1.0f;
        if (thetaX > 2 * Mathf.PI) thetaX -= 2 * Mathf.PI;
        thetaY += FHz * 0.05f * coefficientY;
        if (thetaY > 2 * Mathf.PI) thetaY -= 2 * Mathf.PI;
        thetaZ += FHz * 0.05f * coefficientZ;
        if (thetaZ > 2 * Mathf.PI) thetaZ -= 2 * Mathf.PI;
        if (dotCount < dotMax) dotCount++;
        else dotCount = 1;


        if (isFinished == 0)
        {
            //at first
            //write a new dot.
            waveLine.SetVertexCount(dotCount);
            waveLine.SetPosition(dotCount - 1, dotVec[dotCount-1]);
            if (dotCount == dotMax - 1) isFinished = 1;
        }



        //if enter to roop
        else if (isFinished == 1)
        {
            for (int i = lineLength; i >0 ; i--)
            {
                waveLine.SetVertexCount(lineLength-i+1);
                if ((dotCount - i) > 0) waveLine.SetPosition(lineLength-i, dotVec[dotCount - i]);
                else waveLine.SetPosition(lineLength-i, dotVec[dotCount - i + dotMax]);
            }
            if (lineLength == 0) waveLine.SetVertexCount(0);
        }
        sphere.transform.position = dotVec[dotCount-1];

        //oscData.sendOSCData(sphere.transform.position);
        oscData.sendOSCData(sphere.transform.position,coefficientX,coefficientY,coefficientZ,FHz);
        //if I need xyz's data to make chord, I use adobe function in comment 
        //calculating r, a and e is in this function.
    }



    public void sliderSetter(float x, float y, float z, float f, float l)
    {
        coefficientX = x/10;
        coefficientY = y/10;
        coefficientZ = z/10;
        FHz = f/440.0f;
        float fl = l / 10;
        fl = fl * 63;
        if(lineLength!=(int)fl)isFinished=1;
        lineLength = (int)fl;

      
    }


}
