using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public SkinManager sm;
    // Start is called before the first frame update

    public void NextScene()
    {
        MenuManager.instance.SkinSprite = sm.GetSkin(MenuManager.instance.SkinNameSelected);
        FindObjectOfType<AudioManager>().Play("botao");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);

    }

    public void teste()
    {
        Debug.Log(MenuManager.instance.SkinNameSelected);
       
    }
}
