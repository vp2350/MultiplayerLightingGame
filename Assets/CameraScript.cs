using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] [Range(0f, 1f)] float minMouseDisplacementRadius = .25f;
    [SerializeField] [Range(0f, 10f)] float displacementScale = 2f;
    public GameObject canvas; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = ConvertMousePosToScreenQuarters(Input.mousePosition);

        Vector3 target = new Vector3(character.transform.position.x,
                                     character.transform.position.y,
                                     transform.position.z);

        if (displacement.magnitude > minMouseDisplacementRadius)
        {
            target += displacement.normalized * Mathf.Lerp(0, displacement.magnitude * displacementScale,
                                                              displacement.magnitude - minMouseDisplacementRadius);

        }

        transform.position = target;
    }

    void LateUpdate()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));  // 90 degress on the X axis - change appropriately
        canvas.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public Vector3 ConvertMousePosToScreenQuarters(Vector3 pos)
    {
        Vector3 output = new Vector3();

        output.x = (pos.x * 2 / Screen.width) - 1;
        output.y = (pos.y * 2 / Screen.height) - 1;
        output.z = 0;

        return output;
    }
}
