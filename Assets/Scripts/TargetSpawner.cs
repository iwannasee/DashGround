using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {
    public GameObject[] targetPrefs;
	public Transform trunks;
    public Vector3 newPosForNewTarget;
    public float maxXRange;
    public float maxYrange;
    public float zRange;

    public void SpawnNewTargetOnTrunkWithLevel(int level)
    {
       	Transform trunk = trunks.GetChild(level);
       	//Assume the spawn point will be the first child in hierachy of trunk
       	Transform spawnPoint = trunk.GetChild(0);
		GameObject targetToSpawn;
		switch (level)
        {
        	//Normal target board if is level 1,2 or 3
            case 1:
            case 2:
            case 3:
				targetToSpawn = Instantiate(targetPrefs[0], spawnPoint.position, Quaternion.identity);
				targetToSpawn.transform.parent = spawnPoint;
				break;
			//Heart-shaped target board if level 4, 5, 6, 7
			case 4:
            case 5:
            case 6:
            case 7:
				targetToSpawn = Instantiate(targetPrefs[1], spawnPoint.position, Quaternion.identity);
				targetToSpawn.transform.parent = spawnPoint;
				break;
			//Star-shaped target board if level 8,9, 10
			case 8:
            case 9:
            case 10:
				targetToSpawn= Instantiate(targetPrefs[2], spawnPoint.position, Quaternion.identity);
				targetToSpawn.transform.parent = spawnPoint;
				break;
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
