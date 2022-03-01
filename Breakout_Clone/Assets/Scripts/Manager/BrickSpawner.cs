using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Only Used in Singleplayer
public class BrickSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levels = null;

    [SerializeField]
    private bool DisableLoop;

    private int currentLevel;
    private GameManager manager;
    private int loops;
    private GameObject level;
    private Transform levelTrans;


    public bool loopDone { get; set; }

    private IEnumerator Start()
    {
        if (levels.Length == 0)
        {
            yield break;
        }

        manager = FindObjectOfType<GameManager>();

        while (true)
        {

            while (manager.rdySpawn == false)
            {
                yield return null;
            }

            level = (GameObject)Instantiate(levels[currentLevel], transform);
            levelTrans = level.transform;
            levelTrans.position = transform.position;

            while (0 < levelTrans.childCount)
             {
                yield return null;
             }

            Destroy(level);

            if (DisableLoop == true)
            {
                loops++;
                if (loops >= levels.Length)
                {
                    manager.rdySpawn = false;
                    loopDone = true;

                }
            }
                currentLevel = (int)Mathf.Repeat(currentLevel + 1f, levels.Length);
        }
    }

    public int restLoopCount() => loops = 0;

    public void stageClean()
    {
        for (int i = 0; i < levelTrans.childCount; i++)
        {
            GameObject childBrick = levelTrans.GetChild(i).gameObject;
            Destroy(childBrick);
        }
    }


}
