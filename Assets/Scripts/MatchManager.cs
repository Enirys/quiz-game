using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance;
    public List<GameObject> lines = new List<GameObject>();

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    public void DestroyLines()
    {
        if(lines.Count > 0)
        {
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }
        }
    }
}
