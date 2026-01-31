using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Transition : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
