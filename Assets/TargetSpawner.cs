using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {
    public GameObject[] targetPrefs;

    public Vector3 newPosForNewTarget;
    public float maxXRange;
    public float maxYrange;
    public float zRange;

    public void SpawnNewTargetOnTrunkWithLevel(int level)
    {
        GameObject[] trunks = GameObject.FindGameObjectsWithTag("Trunk");
        foreach (GameObject trunk in trunks)
        {


        }

    }

    //Spawn Targets only without base/trunk
    public void SpawnNewTargetWithGivenLevel(int level)
    {
        Vector3 newPosForNewTarget;
        switch (level)
        {
            case 1:
            case 2:
            case 3:
                newPosForNewTarget = new Vector3(
                    Random.Range(-maxXRange, maxXRange),
                    Random.Range(-maxYrange, maxYrange),
                    zRange + zRange * level / 10
                    );
                Instantiate(targetPrefs[0], newPosForNewTarget, Quaternion.identity);
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                newPosForNewTarget = new Vector3(
                    Random.Range(-maxXRange, maxXRange),
                    Random.Range(-maxYrange, maxYrange),
                    zRange + zRange * level / 10
                    );
                Instantiate(targetPrefs[1], newPosForNewTarget, Quaternion.identity);
                break;

            case 8:
            case 9:
            case 10:
                newPosForNewTarget = new Vector3(
                    Random.Range(-maxXRange, maxXRange),
                    Random.Range(-maxYrange, maxYrange),
                    zRange + zRange * level / 10
                    );
                Instantiate(targetPrefs[2], newPosForNewTarget, Quaternion.identity);
                break;

        }
    }
}
