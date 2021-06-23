
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public void DoslowMotion()
    {
        Time.timeScale = .05f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void ReturnNormal()
    {
        Time.timeScale = 1f;
    }
}
