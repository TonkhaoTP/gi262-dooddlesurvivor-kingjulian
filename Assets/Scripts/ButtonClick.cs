using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MapScene");
    }
}
